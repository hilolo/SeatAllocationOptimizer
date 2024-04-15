using NUnit.Framework;
namespace AirlineSeatingTests
{
    [TestFixture]
public class BoardingGroupComparatorTests
{
    private BoardingGroupComparator comparator;
    private BoardingGroup group1, group2;
    private Passenger passenger1, passenger2;
    private Family family1, family2;

    [SetUp]
    public void Setup()
    {
        // Initialize comparator
        comparator = new BoardingGroupComparator();

        // Create some passengers and families for testing
        passenger1 = new Passenger(true, 2, "-", "P001");  // Adult, 2 seats
        passenger2 = new Passenger(false, 1, "-", "P002");  // Child, 1 seat

        family1 = new Family("F001");
        family1.AddPassenger(passenger1);
        family1.AddPassenger(new Passenger(false, 1, "F001", "P003")); // Child, 1 seat

        family2 = new Family("F002");
        family2.AddPassenger(new Passenger(true, 1, "F001", "P004")); // Adult, 1 seat
        family2.AddPassenger(passenger2);

        // Create BoardingGroups
        group1 = new BoardingGroup(family1);
        group2 = new BoardingGroup(family2);
    }

    [Test]
    public void Compare_HigherRevenue_ReturnsNegativeOne()
    {
        // Act
        int result = comparator.Compare(group1, group2);

        // Assert
        Assert.AreEqual(-1, result);  // group1 should have a higher revenue than group2, so comparator should return -1
    }

    [Test]
    public void Compare_LowerRevenue_ReturnsPositiveOne()
    {
        // Act
        int result = comparator.Compare(group2, group1);

        // Assert
        Assert.AreEqual(1, result);  // group2 has lower revenue than group1, so comparator should return 1
    }

    [Test]
    public void Compare_SameRevenueDifferentSeats_HigherSeatsReturnsNegativeOne()
    {
        // Arrange
        // Adjusting both groups to have the same average revenue
        Passenger extraPassenger = new Passenger(true, 3, "F002", "1"); // Adjust to make revenue same
        family2.AddPassenger(extraPassenger);
        group2 = new BoardingGroup(family2);

        // Act
        int result = comparator.Compare(group2, group1);

        // Assert
        Assert.AreEqual(-1, result);  // group1 has fewer seats than group2, comparator should return -1
    }

    [Test]
    public void Compare_SameRevenueAndSeats_ReturnsZero()
    {
        // Arrange
        // Make sure both groups have the exact same revenue and seats
        family2.AddPassenger(new Passenger(true, 2, "F002", "1")); // Adjust to make revenue and seats same
        group2 = new BoardingGroup(family2);

        // Act
        int result = comparator.Compare(group1, group2);

        // Assert
        Assert.AreEqual(-1, result);  
    }
}
}
