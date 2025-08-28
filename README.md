# Booking API

A simple booking system API built with .NET 9 and clean architecture principles. The system allows you to search for available homes within specific date ranges using an efficient IntervalTree implementation.

## Quick Start

### Running the Application

1. **Start the API**:
   ```bash
   cd BookingApi.WebApi
   dotnet run
   ```

2. **Access the API**:
   - API: `https://localhost:7230/api/available-homes`
   - Swagger UI: `https://localhost:7230/swagger`

### Basic Usage

**Filter by date range:**
```bash
GET https://localhost:7230/api/available-homes?startDate=2025-07-15&endDate=2025-07-17
```

The API returns homes that are available for **all** days in the requested range.

## Testing

Python integration tests are included that verify the API works correctly with different date ranges.

### Prerequisites
- Python 3.7+ with `requests` library
- Running API (see above)

### Running Tests

1. **Install dependencies**:
   ```bash
   cd tests
   pip install -r requirements.txt
   ```

2. **Run all tests**:
   ```bash
   python test_available_homes.py
   ```

### What the Tests Cover

The tests validate 14 different scenarios using our sample data:

- **Single day searches** (July 15, July 16, July 4, July 18)
- **Multi-day ranges** (July 15-17, July 20-22, July 12-13)
- **Edge cases** (entire month, month boundaries, no matches)
- **Error handling** (invalid dates, malformed requests)

Each test checks that the right homes are returned and validates response structure and performance.

## Architecture

The project follows clean architecture with four distinct layers:

```
API Layer (BookingApi.WebApi)
    ↓ depends on
Application Layer (BookingApi.Application)
    ↓ depends on
Domain Layer (BookingApi.Domain)
    ↑ implemented by
Infrastructure Layer (BookingApi.Infrastructure)
```

### Key Components

- **Domain**: Contains the `Home` entity with basic availability checking
- **Application**: Houses the filtering logic and DTOs
- **Infrastructure**: Provides in-memory data storage and dependency injection
- **API**: Exposes HTTP endpoints with proper error handling

## How the Filtering Works

### The Problem
With potentially thousands of homes and hundreds of available dates each, a naive approach would check every date for every home on each search. This becomes slow quickly.

### Solution: IntervalTree

An IntervalTree is a specialized data structure designed to efficiently find overlapping intervals. Think of it as a smart way to organize date ranges so you can quickly answer questions like "which homes are available for July 15-17?"

#### How It Works

1. **Data Transformation**: Instead of storing individual dates, we group consecutive days into intervals:
   - House 1: [July 15, July 16, July 17, July 18, July 19] → Interval(July 15-19)
   - House 3: [July 12], [July 15], [July 18] → Three separate intervals
   - House 8: [July 1-31] → One large interval covering the entire month

2. **Tree Structure**: These intervals are organized in a binary tree where:
   - Each node contains an interval and knows the maximum end date in its subtree
   - This allows the algorithm to skip entire branches that can't possibly contain matches
   - Searching becomes logarithmic instead of linear

3. **Smart Searching**: When you search for July 16-18:
   - The tree only visits nodes that might contain overlapping intervals
   - It uses the "maximum end date" information to prune branches early
   - Instead of checking all 1000+ homes, it might only examine 8-10 tree nodes

#### Performance in Practice

- **Traditional approach**: Check every home's every date = O(homes × dates)
- **IntervalTree approach**: Navigate tree structure = O(log intervals + matches)
- **Real impact**: What took 30,000 operations now takes ~10 operations

The tree is built once on the first filtered request and cached in memory. All subsequent searches benefit from this optimized structure, making date range queries extremely fast even with large datasets.

The system automatically handles this optimization behind the scenes. You just make normal API calls and get fast results.

## Sample Data

The system includes 10 sample homes with various availability patterns:

- **House 1**: July 15-19 (5 consecutive days)
- **House 2**: July 10-16 (7 consecutive days)  
- **House 3**: Scattered individual days
- **House 8**: Entire month of July (great for testing long ranges)
- **House 9**: Crosses month boundary (July 28 - August 3)

This gives you realistic data to test different scenarios.

## Project Structure

```
BookingApi/
├── BookingApi.Domain/           # Business entities
├── BookingApi.Application/      # Use cases and DTOs
│   ├── DataStructures/         # IntervalTree
│   ├── DTOs/                   # Response objects
│   └── Services/               # Business logic
├── BookingApi.Infrastructure/   # Data, external services
└── BookingApi.WebApi/          # HTTP endpoints
```
