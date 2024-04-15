using NUnit.Framework;
using System.Linq;

namespace AirlineSeatingTests
{

    [TestFixture]
    public class FamilyTests
    {
        private Family family;
        private Passenger adult1, adult2, child1;

        [SetUp]
        public void Setup()
        {
            // Initialize the Family with a unique key
            family = new Family("Fam001");

            // Create some passengers
            adult1 = new Passenger(true, 2, "Fam001", "A1");
            adult2 = new Passenger(true, 1, "Fam001", "A2");
            child1 = new Passenger(false, 1, "Fam001", "C1");
        }

        [Test]
        public void AddPassenger_AddsAdultsAndChildren_UpdatesListsAndTotalsCorrectly()
        {
            // Act
            family.AddPassenger(adult1);
            family.AddPassenger(child1);

            // Assert
            Assert.Contains(adult1, family.Adults);
            Assert.Contains(child1, family.Children);
            Assert.AreEqual(3, family.TotalSeats);
            Assert.AreEqual(Passenger.GetAdultPrice() * adult1.Seats + Passenger.GetChildPrice() * child1.Seats, family.TotalRevenue);
            Assert.AreEqual((double)family.TotalRevenue / family.TotalSeats, family.AvgRevenue);
        }

        [Test]
        public void HasChildren_WhenChildrenPresent_ReturnsTrue()
        {
            // Act
            family.AddPassenger(child1);

            // Assert
            Assert.IsTrue(family.HasChildren());
        }

        [Test]
        public void HasChildren_WhenNoChildren_ReturnsFalse()
        {
            // Act
            family.AddPassenger(adult1);

            // Assert
            Assert.IsFalse(family.HasChildren());
        }

        [Test]
        public void GetMinSize_WithOneAdultAndChildren_ReturnsSeatsPlusChildrenCount()
        {
            // Arrange
            family.AddPassenger(adult1);
            family.AddPassenger(child1);

            // Act
            int minSize = family.GetMinSize();

            // Assert
            Assert.AreEqual(adult1.Seats + 1, minSize); // adult1.Seats (2) + 1 child
        }

        [Test]
        public void GetMinSize_WithMultipleAdultsAndChildren_ReturnsSmallestAdultPlusOne()
        {
            // Arrange
            family.AddPassenger(adult1);
            family.AddPassenger(adult2);
            family.AddPassenger(child1);

            // Act
            int minSize = family.GetMinSize();

            // Assert
            Assert.AreEqual(adult2.Seats + 1, minSize); // adult2.Seats (1) + 1 child
        }

        [Test]
        public void GetMinSize_WithMultipleAdultsNoChildren_ReturnsSeatsOfSmallestAdult()
        {
            // Arrange
            family.AddPassenger(adult1);
            family.AddPassenger(adult2);

            // Act
            int minSize = family.GetMinSize();

            // Assert
        }

        [TearDown]
        public void TearDown()
        {
            // Reset prices if changed during testing
            Passenger.SetAdultPrice(250);
            Passenger.SetChildPrice(150);
        }
    }
}
