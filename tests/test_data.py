"""
Test data based on InMemoryHomeRepository.cs
Carefully extracted from the actual seed data
"""

HOMES_DATA = {
    "H001": {
        "name": "House 1",
        "dates": ["2025-07-15", "2025-07-16", "2025-07-17", "2025-07-18", "2025-07-19"]
    },
    "H002": {
        "name": "House 2", 
        "dates": ["2025-07-10", "2025-07-11", "2025-07-12", "2025-07-13", "2025-07-14", "2025-07-15", "2025-07-16"]
    },
    "H003": {
        "name": "House 3",
        "dates": ["2025-07-12", "2025-07-15", "2025-07-18", "2025-07-22", "2025-07-25"]
    },
    "H004": {
        "name": "House 4",
        "dates": ["2025-07-08", "2025-07-09", "2025-07-10", "2025-07-20", "2025-07-21", "2025-07-22", "2025-07-23", "2025-07-30", "2025-07-31"]
    },
    "H005": {
        "name": "House 5",
        "dates": ["2025-07-14", "2025-07-15", "2025-07-16", "2025-07-17", "2025-07-24", "2025-07-25", "2025-07-26"]
    },
    "H006": {
        "name": "House 6",
        "dates": ["2025-07-05", "2025-07-06", "2025-07-12", "2025-07-13", "2025-07-19", "2025-07-20", "2025-07-26", "2025-07-27"]
    },
    "H007": {
        "name": "House 7",
        "dates": ["2025-07-16"]
    },
    "H008": {
        "name": "House 8",
        "dates": [f"2025-07-{str(day).zfill(2)}" for day in range(1, 32)]  # July 1-31
    },
    "H009": {
        "name": "House 9",
        "dates": ["2025-07-28", "2025-07-29", "2025-07-30", "2025-07-31", "2025-08-01", "2025-08-02", "2025-08-03"]
    },
    "H010": {
        "name": "House 10",
        "dates": ["2025-07-07", "2025-07-08", "2025-07-11", "2025-07-12", "2025-07-13", "2025-07-16", "2025-07-19", "2025-07-20", "2025-07-21"]
    }
}

TEST_CASES = [
    {
        "name": "Single day - July 15",
        "start_date": "2025-07-15",
        "end_date": "2025-07-15",
        "expected_home_ids": ["H001", "H002", "H003", "H005", "H008"],
        "expected_count": 5,
        "description": "Houses available on July 15 only"
    },
    {
        "name": "Single day - July 16", 
        "start_date": "2025-07-16",
        "end_date": "2025-07-16",
        "expected_home_ids": ["H001", "H002", "H005", "H007", "H008", "H010"],
        "expected_count": 6,
        "description": "Houses available on July 16 only"
    },
    {
        "name": "Weekend - July 12-13",
        "start_date": "2025-07-12",
        "end_date": "2025-07-13",
        "expected_home_ids": ["H002", "H006", "H008", "H010"],
        "expected_count": 4,
        "description": "Houses available for entire weekend July 12-13"
    },
    {
        "name": "Three day range - July 15-17",
        "start_date": "2025-07-15",
        "end_date": "2025-07-17",
        "expected_home_ids": ["H001", "H005", "H008"],
        "expected_count": 3,
        "description": "Houses available for all three days July 15-17 (H002 excluded - no July 17)"
    },
    {
        "name": "Entire month - July 1-31",
        "start_date": "2025-07-01", 
        "end_date": "2025-07-31",
        "expected_home_ids": ["H008"],
        "expected_count": 1,
        "description": "Only House 8 is available for entire month"
    },
    {
        "name": "Month boundary - July 30 to August 1",
        "start_date": "2025-07-30",
        "end_date": "2025-08-01",
        "expected_home_ids": ["H009"],
        "expected_count": 1,
        "description": "Only House 9 covers July 30 - August 1 range"
    },
    {
        "name": "Single day - July 4",
        "start_date": "2025-07-04",
        "end_date": "2025-07-04",
        "expected_home_ids": ["H008"],
        "expected_count": 1,
        "description": "Only House 8 available on July 4"
    },
    {
        "name": "Three day range - July 20-22",
        "start_date": "2025-07-20",
        "end_date": "2025-07-22",
        "expected_home_ids": ["H004", "H008"],
        "expected_count": 2,
        "description": "Houses available for all three days July 20-22"
    },
    {
        "name": "Early weekend - July 5-6",
        "start_date": "2025-07-05", 
        "end_date": "2025-07-06",
        "expected_home_ids": ["H006", "H008"],
        "expected_count": 2,
        "description": "Houses available for July 5-6 weekend"
    },
    {
        "name": "Five day range - July 14-18",
        "start_date": "2025-07-14",
        "end_date": "2025-07-18",
        "expected_home_ids": ["H008"],
        "expected_count": 1,
        "description": "Only House 8 covers this full range"
    },
    {
        "name": "Long range - July 10-25",
        "start_date": "2025-07-10",
        "end_date": "2025-07-25",
        "expected_home_ids": ["H008"],
        "expected_count": 1,
        "description": "Only House 8 covers this long range"
    },
    {
        "name": "Single day gap test - July 18",
        "start_date": "2025-07-18",
        "end_date": "2025-07-18", 
        "expected_home_ids": ["H001", "H003", "H008"],
        "expected_count": 3,
        "description": "Houses available on July 18"
    },
    {
        "name": "End of month - July 30-31",
        "start_date": "2025-07-30",
        "end_date": "2025-07-31",
        "expected_home_ids": ["H004", "H008", "H009"],
        "expected_count": 3,
        "description": "Houses available for July 30-31"
    },
    {
        "name": "No matches - September 1",
        "start_date": "2025-09-01",
        "end_date": "2025-09-01",
        "expected_home_ids": [],
        "expected_count": 0,
        "description": "No houses available on September 1"
    }
]

def validate_test_case(test_case):
    if test_case["start_date"] is None:
        return True
        
    expected_homes = set(test_case["expected_home_ids"])
    actual_homes = set()
    
    for home_id in HOMES_DATA.keys():
        if is_home_available_for_range(home_id, test_case["start_date"], test_case["end_date"]):
            actual_homes.add(home_id)
    
    return expected_homes == actual_homes