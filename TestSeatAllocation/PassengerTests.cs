using NUnit.Framework;

[TestFixture]
public class PassengerTests
{
    [Test]
    public void Passenger_Creation_AdultPassengerCorrectRevenue()
    {
        int expectedRevenue = 500; // Assuming 2 seats at $250 each
        var passenger = new Passenger(true, 2, "-", "001");

        Assert.AreEqual(expectedRevenue, passenger.Revenue, "Revenue should be correctly calculated for adult passengers.");
    }

    [Test]
    public void Passenger_Creation_ChildPassengerCorrectRevenue()
    {
        int expectedRevenue = 150; // Assuming 1 seat at $150
        var passenger = new Passenger(false, 1, "-", "002");

        Assert.AreEqual(expectedRevenue, passenger.Revenue, "Revenue should be correctly calculated for child passengers.");
    }

    [Test]
    public void Passenger_IsInFamily_FamilyAssigned()
    {
        var passenger = new Passenger(true, 1, "Smith", "003");
        Assert.IsTrue(passenger.IsInFamily(), "Passenger should be in a family when a family name is assigned.");
    }

    [Test]
    public void Passenger_IsInFamily_NoFamilyAssigned()
    {
        var passenger = new Passenger(true, 1, "-", "004");
        Assert.IsFalse(passenger.IsInFamily(), "Passenger should not be in a family when '-' is assigned as the family name.");
    }
}
