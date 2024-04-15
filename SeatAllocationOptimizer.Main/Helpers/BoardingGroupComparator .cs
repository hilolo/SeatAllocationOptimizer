// Imports necessary libraries for comparing objects.
using System.Collections.Generic;

// Defines a class that can compare two BoardingGroup objects.
public class BoardingGroupComparator : IComparer<BoardingGroup>
{
    // The method used to compare two BoardingGroup objects.
    public int Compare(BoardingGroup x, BoardingGroup y)
    {
        // Compares two BoardingGroups based on their average revenue.
        if (x.AvgRevenue < y.AvgRevenue)
        {
            return 1;
        }
        else if (x.AvgRevenue > y.AvgRevenue)
        {
            return -1;
        }
        else
        {
            // If revenues are the same, compare based on the number of seats.
            if (x.Seats < y.Seats)
                return 1;
            else if (x.Seats > y.Seats)
                return -1;
            else
                return 0;  // Same revenue and seats count.
        }
    }
}
