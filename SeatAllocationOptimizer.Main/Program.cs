using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

// Defines a class for the main operations related to passenger and seat management.
public class MainClass
{
    // Constants for plane dimensions and seat calculations
    public static int planeWidth = 6; // Width of the plane in terms of seats per row
    public static int planeRows = 3; // Number of rows in the plane
    public static int bonusSeats = 2; // Additional seats available
    public static int totalSeats = (planeRows * planeWidth) + bonusSeats; // Total number of seats

    // Main entry point for the program
    public static void Main(string[] args)
    {
        // Measure program execution time
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        List<Passenger> passengers = new List<Passenger>(); // List to hold passengers

        // Reading passenger data from a file and creating passenger objects
        using (StreamReader reader = new StreamReader("Data/input.txt"))
        {
            string data;
            while ((data = reader.ReadLine()) != null)
            {
#if DEBUG
                Console.WriteLine("Processing passenger - " + data);
#endif
                passengers.Add(new Passenger(data));
#if DEBUG
                Console.WriteLine("Passenger created - " + data);
#endif
            }
        }

        // Setting up boarding groups and printing the seat map with maximum revenue
        PriorityQueue<BoardingGroup, BoardingGroup> boardingGroups = dataSetupHelper(passengers);
        printMap(idealRevenue(boardingGroups));

        // Stop measuring time
        stopwatch.Stop();


        // Print memory usage
        PrintMemoryUsage(stopwatch);

    }

    // Function to print memory usage
    public static void PrintMemoryUsage(Stopwatch stopwatch)
    {
        long memoryUsed = GC.GetTotalMemory(false);
        Console.WriteLine($"Memory used: {memoryUsed / 1024} KB");
        Console.WriteLine($"Time taken: {stopwatch.Elapsed}");
    }

    // Prints the seating arrangement in the console
    public static void printMap(Passenger[,] seatMap)
    {
        int index = 0;
        
        Console.Write("Seat Allocation : \n");
        for (int i = 0; i < seatMap.GetLength(0); i++)
        {
            for (int j = 0; j < seatMap.GetLength(1); j++)
            {
                index++;
                Console.Write("|" + (seatMap[i, j] == null ? "     " : seatMap[i, j].ToString()));
                if (index == totalSeats)
                    break;
            }
            Console.Write("|\n");
        }
        Console.WriteLine($"Total Revenue: ${calculateRevenue(seatMap):F2}");
    }

    // Calculate and return total revenue from the seat map
    public static double calculateRevenue(Passenger[,] seatMap)
    {
        double totalRevenue = 0;
        foreach (Passenger passenger in seatMap)
        {
            if (passenger != null)
            {
                totalRevenue += (passenger.Seats == 2) ? (passenger.Revenue / 2) : passenger.Revenue ;
            }
        }
        return totalRevenue;
    }

    // Helper method to set up boarding groups from passenger list
    public static PriorityQueue<BoardingGroup, BoardingGroup> dataSetupHelper(List<Passenger> passengers)
    {
#if DEBUG
        Console.WriteLine("Setting up boarding groups");
#endif
        PriorityQueue<BoardingGroup, BoardingGroup> queue = new PriorityQueue<BoardingGroup, BoardingGroup>(new BoardingGroupComparator());
        Dictionary<string, Family> familyMap = new Dictionary<string, Family>();

        // Categorizing passengers into families and individual boarding groups
        foreach (var passenger in passengers)
        {
            if (passenger.Family == "-")
            {
                queue.Enqueue(new BoardingGroup(passenger), new BoardingGroup(passenger));
#if DEBUG
                Console.WriteLine(passenger + " added to boarding group");
#endif
            }
            else
            {
                if (!familyMap.ContainsKey(passenger.Family))
                {
                    familyMap[passenger.Family] = new Family(passenger.Family);
                }

                familyMap[passenger.Family].AddPassenger(passenger);
#if DEBUG
                Console.WriteLine(passenger + " added to family");
#endif
            }
        }

        // Enqueue families into the boarding group
        foreach (var family in familyMap.Values)
        {
            queue.Enqueue(new BoardingGroup(family), new BoardingGroup(family));
#if DEBUG
            Console.WriteLine("Family " + family + " added to boarding group");
#endif
        }

        return queue;
    }

    // Determines the ideal seating arrangement for maximum revenue
    public static Passenger[,] idealRevenue(PriorityQueue<BoardingGroup, BoardingGroup> boardingGroups)
    {
        int curRow = 0, curRowSeat = 0, seated = 0, maxSeatsPerRow = planeWidth;
        Passenger[,] seatMap = new Passenger[planeRows + 1, maxSeatsPerRow];

        // Allocating seats for each boarding group optimally
        while (seated < totalSeats )
        {
            List<BoardingGroup> holder = new List<BoardingGroup>();
            int seat_check = seated;

            while (boardingGroups.Count > 0)
            {
                BoardingGroup bg = boardingGroups.Dequeue();
#if DEBUG
                Console.WriteLine("Seeing if boarding group " + bg + " will fit at seat " + curRow + curRowSeat);
#endif

                if (bg.Seats > totalSeats - seated)
                {
#if DEBUG
                    Console.WriteLine("Boarding group " + bg + " is too large with only " + (totalSeats - seated) + " seats remaining; removing");
#endif
                }
                else
                {
#if DEBUG
                    Console.WriteLine("Attempting to check boarding group " + bg);
#endif
                    if (bg.GetMinSeats() + curRowSeat < maxSeatsPerRow)
                    {
                        seatMap = processBoardingGroup(seatMap, bg, curRow, curRowSeat, maxSeatsPerRow);
                        curRowSeat += bg.Seats;
                        if (curRowSeat >= maxSeatsPerRow)
                        {
                            curRow++; // Move to the next row
                            curRowSeat -= maxSeatsPerRow; // Adjust the seat index for the next row
                        }
                        seated += bg.Seats; // Update the count of seated passengers
                    }
                    else
                    {
                        holder.Add(bg); // Hold the boarding group for the next round of seating
                    }
                }

                // If we've exceeded the number of rows, adjust the maximum seats per row for overflow seating
                if (curRow > planeRows)
                {
                    maxSeatsPerRow = bonusSeats;
                }
            }

            // If no new passengers were seated in this round, move to the next row and reset the seat index
            if (seated == seat_check)
            {
                curRow++;
                curRowSeat = 0;
            }

            // Re-enqueue boarding groups that were held back in this round
            foreach (var bg in holder)
            {
                boardingGroups.Enqueue(bg, bg);
            }

            // If no boarding groups were held back for the next round of seating,
            // exit the loop to conclude the seating arrangement process.
            // otherwise stuck in loop
            if (holder.Count == 0)
            {
                break;
            }

        }

        return seatMap; // Return the final seating arrangement
    }

    // Processes each boarding group to fit into the seating map
    public static Passenger[,] processBoardingGroup(Passenger[,] seatMap, BoardingGroup bg, int curRow, int curRowSeat, int maxSeatsPerRow)
    {
#if DEBUG
        Console.WriteLine("Attempting to process boarding group " + bg);
#endif
        if (bg.IsFamily)
        {
            List<Passenger> adults = bg.Family.Adults;
            List<Passenger> children = bg.Family.Children;

            // Assign seats to adults first
            if (adults[0].Seats <= maxSeatsPerRow - curRowSeat)
            {
                for (int i = 0; i < adults[0].Seats; i++)
                {
#if DEBUG
                    Console.WriteLine("Assigning seat for " + adults[0]);
#endif
                    seatMap[curRow, curRowSeat] = adults[0];
                    curRowSeat++;
                }
                adults.RemoveAt(0);
            }
            else
            {
#if DEBUG
                Console.WriteLine("Assigning seat for " + adults[1]);
#endif
                seatMap[curRow, curRowSeat] = adults[1];
                curRowSeat++;
                adults.RemoveAt(1);
            }

            // Now assign seats to children
            int index = 0;
            while (index < children.Count && curRowSeat < maxSeatsPerRow)
            {
#if DEBUG
                Console.WriteLine("Assigning seat for " + children[index]);
#endif
                seatMap[curRow, curRowSeat] = children[index];
                index++;
                curRowSeat++;
            }

            // If we reach the end of a row, move to the next row
            if (curRowSeat >= maxSeatsPerRow)
            {
                curRow++;
                curRowSeat -= maxSeatsPerRow;
            }

            // If there are still adults left, assign their seats
            foreach (var adult in adults)
            {
                for (int i = 0; i < adult.Seats; i++)
                {
#if DEBUG
                    Console.WriteLine("Assigning seat for " + adult);
#endif
                    seatMap[curRow, curRowSeat] = adult;
                    curRowSeat++;
                }
            }

            // Check and assign any remaining children's seats
            while (index < children.Count && curRowSeat < maxSeatsPerRow)
            {
#if DEBUG
                Console.WriteLine("Assigning seat for " + children[index]);
#endif
                seatMap[curRow, curRowSeat] = children[index];
                index++;
                curRowSeat++;
            }
        }
        else
        {
            // For individual passengers or groups without families
            for (int i = 0; i < bg.Seats; i++)
            {
                seatMap[curRow, curRowSeat] = bg.Passenger;
                curRowSeat++;
            }
        }
#if DEBUG
        //printMap(seatMap); // Display the current seating map only in debug mode
#endif
        return seatMap;
    }
}

