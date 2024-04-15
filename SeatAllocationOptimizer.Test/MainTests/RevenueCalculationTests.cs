namespace AirlineSeatingTests
{
    [TestFixture]
    public class RevenueCalculationTests
    {
        [Test]
        public void TestCalculateRevenue()
        {
            // Arrange
            Passenger[,] seatMap = new Passenger[4, 6]; // Assuming 4 rows, 6 columns for test simplicity
            seatMap[0, 0] = new Passenger("1,Adult,35,FOO1,No");
            seatMap[0, 1] = new Passenger("2,Adult,32,FOO1,No");
            // Act
            double revenue = MainClass.calculateRevenue(seatMap);

            // Assert
            Assert.AreEqual(500.0, revenue); // Adjust this value based on your implementation
        }
    }
}
