using CityBikeAPI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CityBikeAPI.Data;
using CityBikeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using static System.Collections.Specialized.BitVector32;
using CityBikeFiAPI.Models;

namespace CityBikeAPI.Controllers.Tests
{
    [TestClass()]
    public class StationControllerTests
    {
        private CityBikeContext? _context;

        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<CityBikeContext>()
                .UseInMemoryDatabase(databaseName: "CityBikeTestDB")
                .Options;

            _context = new CityBikeContext(options);
            _context.Database.EnsureCreated();
        }

        [TestMethod]
        public async Task GetAllStations_ReturnsOkResultWithStationsList()
        {
            // Add test data
            _context?.Stations.AddRange(new List<Station>
            {
                new Station {

                    FID = 1,
                    ID = 1,
                    Nimi = "Station 1",
                    Namn = "Station 1",
                    Name = "Station 1",
                    Osoite = "Address 1",
                    Adress = "Address 1",
                    Kaupunki = "City 1",
                    Stad = "City 1",
                    Operaattor = "Operator 1",
                    Kapasiteet = 15,
                    X = 60.23456M,
                    Y = 24.23456M

                },
                new Station {

                    FID = 2,
                    ID = 2,
                    Nimi = "Station 2",
                    Namn = "Station 2",
                    Name = "Station 2",
                    Osoite = "Address 2",
                    Adress = "Address 2",
                    Kaupunki = "City 2",
                    Stad = "City 2",
                    Operaattor = "Operator 2",
                    Kapasiteet = 15,
                    X = 60.23456M,
                    Y = 24.23456M

                },
            });
            _context.SaveChanges();
            var controller = new StationController(_context);
            var result = await controller.GetAllStations();
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var stations = okResult.Value as List<Station>;
            Assert.IsNotNull(stations);
            Assert.AreEqual(2, stations.Count);
            Assert.IsTrue(stations.Any(s => s.ID == 1 && s.Name == "Station 1"));
            Assert.IsTrue(stations.Any(s => s.ID == 2 && s.Name == "Station 2"));
        }

        [TestMethod()]
        public async Task GetStationTestAsync()
        {
            // Arrange
            var station = new Station
            {
                ID = 1,
                Name = "Station 1",
                Adress = "Address 1"
            };

            var journey1 = new JourneyEntity
            {
                Departure_station_id = 1,
                Return_station_id = 2
            };

            var journey2 = new JourneyEntity
            {
                Departure_station_id = 3,
                Return_station_id = 1
            };

            var options = new DbContextOptionsBuilder<CityBikeContext>()
                .UseInMemoryDatabase(databaseName: "GetStation_ReturnsStationInfo")
                .Options;

            using (var context = new CityBikeContext(options))
            {
                await context.AddRangeAsync(station, journey1, journey2);
                await context.SaveChangesAsync();
            }

            var controller = new StationController(new CityBikeContext(options));

            // Act
            var result = await controller.GetStation(station.ID);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));


            var okResult = (OkObjectResult)result;
            var stationInfo = (StationInfo)okResult.Value;

            Assert.AreEqual(station.Name, stationInfo.Name);
            Assert.AreEqual(station.Adress, stationInfo.Address);
            Assert.AreEqual(1, stationInfo.TotalJourneysStarting);
            Assert.AreEqual(1, stationInfo.TotalJourneysEnding);

        }
            
    }
}
