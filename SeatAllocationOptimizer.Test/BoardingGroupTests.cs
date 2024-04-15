using NUnit.Framework;

namespace AirlineSeatingTests
{
    [TestFixture]
public class BoardingGroupTests
{
    private Passenger passenger;
    private Family family;
    private BoardingGroup boardingGroupPassenger;
    private BoardingGroup boardingGroupFamily;

    [SetUp]
    public void Setup()
    {
        // Initialize a Passenger
        passenger = new Passenger(true, 1, "Smith", "P001");

        // Initialize a Family and add passengers
        family = new Family("F001");
        family.AddPassenger(new Passenger(true, 2, "Smith", "F001-A1"));
        family.AddPassenger(new Passenger(false, 1, "Smith", "F001-C1"));

        // Create Boarding Groups
        boardingGroupPassenger = new BoardingGroup(passenger);
        boardingGroupFamily = new BoardingGroup(family);
    }

    [Test]
    public void Constructor_IndividualPassenger_CorrectlyInitializesProperties()
    {
        // Assert
        Assert.IsFalse(boardingGroupPassenger.IsFamily);
        Assert.AreEqual(passenger, boardingGroupPassenger.Passenger);
        Assert.AreEqual(passenger.GetPerSeatRevenue(), boardingGroupPassenger.AvgRevenue);
        Assert.AreEqual(passenger.Seats, boardingGroupPassenger.Seats);
    }

    [Test]
    public void Constructor_FamilyGroup_CorrectlyInitializesProperties()
    {
        // Assert
        Assert.IsTrue(boardingGroupFamily.IsFamily);
        Assert.AreEqual(family, boardingGroupFamily.Family);
        Assert.AreEqual(family.AvgRevenue, boardingGroupFamily.AvgRevenue);
        Assert.AreEqual(family.TotalSeats, boardingGroupFamily.Seats);
    }

    [Test]
    public void GetMinSeats_IndividualPassenger_ReturnsPassengerSeats()
    {
        // Act
        var minSeats = boardingGroupPassenger.GetMinSeats();

        // Assert
        Assert.AreEqual(passenger.Seats, minSeats);
    }

    [Test]
    public void GetMinSeats_FamilyGroup_ReturnsFamilyMinSize()
    {
        // Act
        var minSeats = boardingGroupFamily.GetMinSeats();

        // Assert
        Assert.AreEqual(family.GetMinSize(), minSeats);
    }

    [Test]
    public void ToString_IndividualPassenger_ReturnsCorrectFormat()
    {
        // Act
        var result = boardingGroupPassenger.ToString();

        // Assert
        Assert.AreEqual($"Passenger {passenger}", result);
    }

    [Test]
    public void ToString_FamilyGroup_ReturnsCorrectFormat()
    {
        // Act
        var result = boardingGroupFamily.ToString();

        // Assert
        Assert.AreEqual($"Family {family}", result);
    }
}
}