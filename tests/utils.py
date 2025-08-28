"""
Utility functions for API testing
"""

import requests
import time
from datetime import datetime
from config import BASE_URL, API_ENDPOINT, VERIFY_SSL, TIMEOUT

def make_api_request(start_date=None, end_date=None):
    url = f"{BASE_URL}{API_ENDPOINT}"
    params = {}
    
    if start_date and end_date:
        params = {
            "startDate": start_date,
            "endDate": end_date
        }
    
    start_time = time.time()
    response = requests.get(url, params=params, verify=VERIFY_SSL, timeout=TIMEOUT)
    end_time = time.time()
    
    response_time = end_time - start_time
    
    return response, response_time

def validate_response_structure(response_data):
    errors = []
    
    if not isinstance(response_data, dict):
        errors.append("Response is not a JSON object")
        return errors
    
    if "status" not in response_data:
        errors.append("Missing 'status' field")
    elif response_data["status"] != "OK":
        errors.append(f"Status is not 'OK': {response_data['status']}")
    
    if "homes" not in response_data:
        errors.append("Missing 'homes' field")
        return errors
    
    if not isinstance(response_data["homes"], list):
        errors.append("'homes' field is not an array")
        return errors
    
    for i, home in enumerate(response_data["homes"]):
        if not isinstance(home, dict):
            errors.append(f"Home {i} is not an object")
            continue
            
        required_fields = ["homeId", "homeName", "availableSlots"]
        for field in required_fields:
            if field not in home:
                errors.append(f"Home {i} missing '{field}' field")
        
        if "availableSlots" in home and not isinstance(home["availableSlots"], list):
            errors.append(f"Home {i} 'availableSlots' is not an array")
    
    return errors

def extract_home_ids(response_data):
    if "homes" not in response_data:
        return []
    
    return [home.get("homeId") for home in response_data["homes"] if "homeId" in home]

def print_test_result(test_name, passed, message="", response_time=None):
    status = "✓ PASS" if passed else "✗ FAIL"
    timestamp = datetime.now().strftime("%H:%M:%S")
    
    result = f"[{timestamp}] {status} - {test_name}"
    
    if response_time:
        result += f" ({response_time:.3f}s)"
    
    if message:
        result += f" - {message}"
    
    print(result)

def print_summary(total_tests, passed_tests, failed_tests):
    print("\n" + "="*60)
    print("TEST EXECUTION SUMMARY")
    print("="*60)
    print(f"Total Tests: {total_tests}")
    print(f"Passed: {passed_tests}")
    print(f"Failed: {failed_tests}")
    print(f"Success Rate: {(passed_tests/total_tests)*100:.1f}%")
    
    if failed_tests == 0:
        print("\nALL TESTS PASSED!")
    else:
        print(f"\n {failed_tests} TESTS FAILED")

def compare_lists(expected, actual, item_name="item"):
    expected_set = set(expected)
    actual_set = set(actual)
    
    missing = expected_set - actual_set
    unexpected = actual_set - expected_set
    
    if not missing and not unexpected:
        return True, "Lists match exactly"
    
    errors = []
    if missing:
        errors.append(f"Missing {item_name}s: {sorted(missing)}")
    if unexpected:
        errors.append(f"Unexpected {item_name}s: {sorted(unexpected)}")
    
    return False, "; ".join(errors)