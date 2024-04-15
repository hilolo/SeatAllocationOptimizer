using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System;

namespace AirlineSeatingTests
{
    [TestFixture]
    public class DataLoaderTests
    {
        [Test]
        public void TestLoadData()
        {
            // Arrange
            string filePath = "Mock/input.txt";
            List<Passenger> expectedPassengers = new List<Passenger>
            {
                new Passenger("1,Adult,35,FOO1,Yes"),
                new Passenger("2,Adult,32,FOO1,No"),
            };

            // Act
            using (StreamReader reader = new StreamReader(filePath))
            {
                List<Passenger> passengers = new List<Passenger>();
                string data;
                while ((data = reader.ReadLine()) != null)
                {
                    passengers.Add(new Passenger(data));
                }

                // Assert
                Assert.AreEqual(expectedPassengers.Count, passengers.Count);
                for (int i = 0; i < passengers.Count; i++)
                {
                    Assert.AreEqual(expectedPassengers[i].ToString(), passengers[i].ToString());
                }
            }
        }
    }
}
