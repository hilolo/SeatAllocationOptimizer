// Defines a class to represent a group of passengers or a family as a boarding entity.
public class BoardingGroup
{
    // Properties to hold references to Family and Passenger objects.
    public Family Family { get; set; }
    public Passenger Passenger { get; set; }
    public bool IsFamily { get; private set; }  // True if the group is a family.

    public double AvgRevenue { get; private set; }
    public int Seats { get; private set; }

    // Constructor for a group of individual passenger.
    public BoardingGroup(Passenger passenger)
    {
        IsFamily = false;
        Passenger = passenger;
        AvgRevenue = passenger.GetPerSeatRevenue();  // Calculates revenue per seat.
        Seats = passenger.Seats;
    }

    // Constructor for a family group.
    public BoardingGroup(Family family)
    {
        IsFamily = true;
        Family = family;
        AvgRevenue = family.AvgRevenue;
        Seats = family.TotalSeats;
    }

    // Method to get minimum seats required for the boarding group.
    public int GetMinSeats()
    {
        return IsFamily ? Family.GetMinSize() : Passenger.Seats;
    }

    // Returns a string representation of the boarding group.
    public override string ToString()
    {
        return IsFamily ? $"Family {Family}" : $"Passenger {Passenger}";
    }
}
