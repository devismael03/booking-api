#!/usr/bin/env python3

import sys
import time
import requests
from test_data import TEST_CASES, validate_test_case
from utils import (
    make_api_request, validate_response_structure, extract_home_ids,
    print_test_result, print_summary, compare_lists
)


def run_test_case(test_case):
    test_name = test_case["name"]
    
    try:
        response, response_time = make_api_request(
            test_case["start_date"], 
            test_case["end_date"]
        )
        
        if response.status_code != 200:
            print_test_result(test_name, False, f"HTTP {response.status_code}", response_time)
            return False
        
        try:
            data = response.json()
        except ValueError as e:
            print_test_result(test_name, False, f"Invalid JSON: {str(e)}", response_time)
            return False
        
        structure_errors = validate_response_structure(data)
        if structure_errors:
            print_test_result(test_name, False, f"Structure errors: {'; '.join(structure_errors)}", response_time)
            return False
        
        actual_home_ids = extract_home_ids(data)
        actual_count = len(actual_home_ids)
        
        expected_count = test_case["expected_count"]
        if actual_count != expected_count:
            print_test_result(test_name, False, f"Expected {expected_count} homes, got {actual_count}", response_time)
            return False
        
        if test_case["start_date"] is not None:
            expected_home_ids = test_case["expected_home_ids"]
            lists_match, diff_message = compare_lists(expected_home_ids, actual_home_ids, "home")
            
            if not lists_match:
                print_test_result(test_name, False, diff_message, response_time)
                return False
        
        success_msg = test_case["description"]
        print_test_result(test_name, True, success_msg, response_time)
        return True
        
    except requests.exceptions.RequestException as e:
        print_test_result(test_name, False, f"Request error: {str(e)}")
        return False
    except Exception as e:
        print_test_result(test_name, False, f"Unexpected error: {str(e)}")
        return False

def test_error_scenarios():
    error_tests = [
        {
            "name": "Invalid start date format",
            "start_date": "invalid-date",
            "end_date": "2025-07-15",
            "expected_status": 400
        },
        {
            "name": "Invalid end date format", 
            "start_date": "2025-07-15",
            "end_date": "invalid-date",
            "expected_status": 400
        },
        {
            "name": "Start date after end date",
            "start_date": "2025-07-20",
            "end_date": "2025-07-15", 
            "expected_status": 400
        },
        {
            "name": "Only start date provided",
            "start_date": "2025-07-15",
            "end_date": None,
            "expected_status": 400
        }
    ]
    
    passed_error_tests = 0
    
    for error_test in error_tests:
        try:
            url = f"https://localhost:7230/api/available-homes"
            params = {}
            
            if error_test["start_date"]:
                params["startDate"] = error_test["start_date"]
            if error_test["end_date"]:
                params["endDate"] = error_test["end_date"]
            
            response = requests.get(url, params=params, verify=False, timeout=30)
            
            expected_status = error_test["expected_status"]
            if response.status_code == expected_status:
                print_test_result(error_test["name"], True, f"Correctly returned HTTP {expected_status}")
                passed_error_tests += 1
            else:
                print_test_result(error_test["name"], False, f"Expected HTTP {expected_status}, got {response.status_code}")
                
        except Exception as e:
            print_test_result(error_test["name"], False, f"Error: {str(e)}")
    
    return passed_error_tests, len(error_tests)

def main():
    print("BOOKING API INTEGRATION TESTS")
    print("="*60)
    print(f"Testing endpoint: https://localhost:7230/api/available-homes")
    print(f"Total test cases: {len(TEST_CASES)}")
    print("="*60)
    
    print("\nRunning test cases...")
    print("-" * 60)
    
    passed_tests = 0
    failed_tests = 0
    
    for i, test_case in enumerate(TEST_CASES):        
        if run_test_case(test_case):
            passed_tests += 1
        else:
            failed_tests += 1
    
    print("\nTesting error scenarios...")
    print("-" * 60)
    
    passed_error, total_error = test_error_scenarios()
    passed_tests += passed_error
    failed_tests += (total_error - passed_error)
    
    total_tests = passed_tests + failed_tests
    print_summary(total_tests, passed_tests, failed_tests)
    
    sys.exit(0 if failed_tests == 0 else 1)

if __name__ == "__main__":
    main()