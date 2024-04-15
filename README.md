
**Important Note:** 
This program is currently contain some bugs affecting its operation.  
Initially designed to handle a configuration of 20 seats across four rows, adjustments can be made for larger capacities by changing the variable planeRows = 33. 
# Maximizing Airline Revenue: A Systematic Approach
The application developed efficiently manages the seating and revenue optimization for a 200-seat airliner, addressing the unique needs of families and children. This document outlines the methodologies and strategies employed by the program, which are encapsulated within several well-defined C# classes.

## Program Structure Overview
The program utilizes several classes designed to encapsulate the functionality needed to manage passengers, group them appropriately, calculate revenues, and assign seating in a manner that maximizes revenue:

- **`Passenger`**: Manages individual passenger details, including revenue generation and seating requirements.
- **`Family`**: Groups passengers by family, managing total and average revenue, and calculates collective seating needs.
- **`BoardingGroup`**: A wrapper for either individual passengers or families to manage them as single entities during the boarding process.
- **`BoardingGroupComparator`**: A comparator that prioritizes boarding groups based on revenue per seat and minimal seating needs.

## Revenue Maximization Steps
### 1. **Class Definitions and Data Handling**
- **Passenger and Family Creation**: Passengers are either instantiated directly or parsed from input, with attributes defining their adult/child status, revenue, and seating needs. Families aggregate passengers, summarizing their combined revenue and seating metrics.
- **Boarding Group Formation**: Individuals and families are wrapped into boarding groups which can be managed uniformly.

### 2. **Sorting and Prioritization**
- **Custom Sorting Strategy**: Boarding groups are sorted using a priority queue that utilizes a custom comparator focusing on maximizing revenue and optimizing seat usage.

### 3. **Dynamic Seating Strategy**
- **Efficient Seat Allocation**: The algorithm places passengers or families based on current availability, optimizing the use of space and aiming to seat higher revenue groups first.
- **Family Handling**: The seating logic includes provisions to keep families together and efficiently use available space, especially considering the needs of children.

### 4. **Edge Case Management**
- **Adjustments for Overflows**: If standard rows are filled, the algorithm adapts by considering additional seats or rows, ensuring all passengers are accommodated.

### 5. **Output and Confirmation**
- **Seating Map Visualization**: Displays the final seating arrangement, offering a visual confirmation of the strategic placement of passengers and families.

## Detailed Explanation of the `idealRevenue` Method
The `idealRevenue` method determines the optimal seating arrangement to maximize revenue based on passenger and boarding group characteristics. Here is a step-by-step breakdown of how the method works:

### Step 1: Initialize Variables
A two-dimensional array (`seatMap`) represents the plane's seating, and various counters track the current row, seat, and the total number of seated passengers.

### Step 2: Boarding Group Allocation
A `PriorityQueue` manages boarding groups with different priorities. The method iterates through the queue, placing each group in the available seats, adjusting for overflow as necessary.

### Step 3: Row and Seat Management
If the current seat index exceeds the row's capacity, it resets, and seating continues on the next row. Groups that can't be seated are held back and tried again later.

### Step 4: Check and Adjustment
If no new passengers were seated during a round, the seating process advances to the next row. Unseated groups are re-enqueued.

### Revenue Calculation
Total revenue is computed by summing up the revenue from each passenger seated in the `seatMap`.

### Output
The seating layout and total revenue are printed after all passengers are seated. Additionally, the execution time and power usage are displayed to provide insights into the performance and efficiency of the seating process.

# Docker Container Management for `seatoptimizer`

## 1. Building the Docker Image

To build the Docker image, open a terminal or command prompt, navigate to the directory containing your Dockerfile, and run the following command:

```bash
docker build --build-arg BUILDPLATFORM=linux/amd64 --build-arg TARGETARCH=linux-x64 -t SeatAllocationOptimizer .
```

## 2. Running the Docker Container

After successfully building the image, start a container with this image using the following command:

```bash
docker run -d -p 8080:8080 --name SeatAllocationOptimizer SeatAllocationOptimizer
```

## 3. Verifying the Application

Check that your application is running with:

```bash
docker logs SeatAllocationOptimizer
```

## 4. Stopping the Container

To stop the container:

```bash
docker stop SeatAllocationOptimizer
```

## 5. Removing the Container

To remove the container when you're done:

```bash
docker rm SeatAllocationOptimizer
```
