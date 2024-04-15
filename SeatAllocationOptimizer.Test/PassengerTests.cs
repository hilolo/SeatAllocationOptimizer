using NUnit.Framework;

namespace AirlineSeatingTests
{
    [TestFixture]
public class PassengerTests
{
    [SetUp]
    public void Setup()
    {
        // Optional setup before each test, if needed.
    }

    [Test]
    public void Constructor_WithExplicitParameters_CorrectlyInitializesProperties()
    {
        // Arrange
        var isAdult = true;
        var seats = 2;
        var family = "Mehdi";
        var key = "1";

        // Act
        var passenger = new Passenger(isAdult, seats, family, key);

        // Assert
        Assert.AreEqual(isAdult, passenger.IsAdult);
        Assert.AreEqual(seats, passenger.Seats);
        Assert.AreEqual(family, passenger.Family);
        Assert.AreEqual(key, passenger.Key);
        Assert.AreEqual(500, passenger.Revenue);  // 2 seats * $250 each for adults
    }

    [Test]
    public void Constructor_WithInputString_CorrectlyParsesAndInitializesProperties()
    {
        // Arrange
        var inputString = "1,Adult,35,FOO1,Yes";

        // Act
        var passenger = new Passenger(inputString);

        // Assert
        Assert.IsTrue(passenger.IsAdult);
        Assert.AreEqual(2, passenger.Seats);
        Assert.AreEqual("FOO1", passenger.Family);
        Assert.AreEqual("1", passenger.Key);
        Assert.AreEqual(500, passenger.Revenue);  // 2 seats * $250 each for adults
    }

    [Test]
    public void IsInFamily_WithFamilyIdentifier_ReturnsTrue()
    {
        // Arrange
        var passenger = new Passenger(true, 1, "Mehdi", "003");

        // Act
        var result = passenger.IsInFamily();

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void IsInFamily_WithoutFamilyIdentifier_ReturnsFalse()
    {
        // Arrange
        var passenger = new Passenger(true, 1, "-", "004");

        // Act
        var result = passenger.IsInFamily();

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void GetPerSeatRevenue_ReturnsCorrectRevenuePerSeat()
    {
        // Arrange
        var passenger = new Passenger(true, 2, "Mehdi", "005");

        // Act
        var revenuePerSeat = passenger.GetPerSeatRevenue();

        // Assert
        Assert.AreEqual(250, revenuePerSeat); // $500 total / 2 seats
    }

    [TestCase(300)]
    public void SetAdultPrice_ChangesPriceCorrectly(int newPrice)
    {
        // Arrange
        Passenger.SetAdultPrice(newPrice);

        // Act
        var price = Passenger.GetAdultPrice();

        // Assert
        Assert.AreEqual(newPrice, price);
    }

    [TestCase(200)]
    public void SetChildPrice_ChangesPriceCorrectly(int newPrice)
    {
        // Arrange
        Passenger.SetChildPrice(newPrice);

        // Act
        var price = Passenger.GetChildPrice();

        // Assert
        Assert.AreEqual(newPrice, price);
    }

    [TearDown]
    public void TearDown()
    {
        // Reset prices back to default after each test, if price setting is used in tests.
        Passenger.SetAdultPrice(250);
        Passenger.SetChildPrice(150);
    }
}
}
