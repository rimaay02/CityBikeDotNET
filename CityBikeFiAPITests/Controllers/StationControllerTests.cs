using CityBikeAPI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CityBikeAPI.Data;
using CityBikeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using static System.Collections.Specialized.BitVector32;

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
            var existingStation = new Station
            {
                FID = 3,
                ID = 3,
                Nimi = "Station 3",
                Namn = "Station 3",
                Name = "Station 3",
                Osoite = "Address 3",
                Adress = "Address 3",
                Kaupunki = "City 3",
                Stad = "City 3",
                Operaattor = "Operator 3",
                Kapasiteet = 15,
                X = 60.23456M,
                Y = 24.23456M
            };
            var options = new DbContextOptionsBuilder<CityBikeContext>()
                .UseInMemoryDatabase(databaseName: "GetStation")
                .Options;
            using (var context = new CityBikeContext(options))
            {
                await context.Stations.AddAsync(existingStation);
                await context.SaveChangesAsync();

                var controller = new StationController(context);
                var result = await controller.GetStation(existingStation.ID);
                Assert.IsInstanceOfType(result, typeof(OkObjectResult));
                var okResult = (OkObjectResult)result;
                Assert.AreEqual(existingStation, okResult.Value);
            }
        }

        [TestMethod()]
        public async Task AddStationTestAsync()
        {
            var options = new DbContextOptionsBuilder<CityBikeContext>()
      .UseInMemoryDatabase(databaseName: "NewStation")
      .Options;
            var dbContext = new CityBikeContext(options);
            var controller = new StationController(dbContext);
            var newStation = new Station()
            {
                FID = 88,
                ID = 89,
                Nimi = "Station 88",
                Namn = "Station 88",
                Name = "Station 88",
                Osoite = "Address 88",
                Adress = "Address 88",
                Kaupunki = "City 88",
                Stad = "City 88",
                Operaattor = "Operator 88",
                Kapasiteet = 15,
                X = 60.23456M,
                Y = 24.23456M
            };
            var expectedStationId = 89;

            // Act
            var result = await controller.AddStation(newStation) as OkObjectResult;
            var actualStationId = (int)result.Value;

            // Assert
            Assert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
            Assert.AreEqual(expectedStationId, actualStationId);
        }
            
    }
}
