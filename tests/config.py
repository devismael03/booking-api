BASE_URL = "https://localhost:7230"
API_ENDPOINT = "/api/available-homes"

VERIFY_SSL = False 
TIMEOUT = 30 
RETRY_COUNT = 3 

EXPECTED_RESPONSE_KEYS = ["status", "homes"]
EXPECTED_HOME_KEYS = ["homeId", "homeName", "availableSlots"]