namespace AirlineSeatingTests
{
    [TestFixture]
    public class SeatAllocationTests
    {
        [Test]
        public void TestSeatAllocation()
        {
            // Arrange
            List<Passenger> passengers = new List<Passenger>
        {
           new Passenger("1,Adult,35,FOO1,No"),
            new Passenger("2,Adult,32,FOO1,No")
        };

            var boardingGroups = MainClass.dataSetupHelper(passengers);

            // Act
            var seatMap = MainClass.idealRevenue(boardingGroups);

            // Assert
        }
    }
}
