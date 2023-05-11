using Microsoft.VisualStudio.TestTools.UnitTesting;
using CityBikeAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityBikeAPI.Data;
using Microsoft.EntityFrameworkCore;
using CityBikeAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityBikeAPI.Controllers.Tests
{
    [TestClass()]
    public class JourneyControllerTests
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
        [TestMethod()]
        public async Task GetAllJourneysTestAsync()
        {
            _context?.Journeys.AddRange(new List<Journey>
            {
              new Journey {
                  Id = 1,
                  Departure = DateTime.Now,
                  Return_station = DateTime.Now.AddDays(1),
                  Departure_station_id = 1,
                  Departure_station_name = "Station 1",
                  Return_station_id = 2,
                  Return_station_name = "Station 2",
                  Covered_distance = 10,
                  Duration = 120 },
              new Journey {
                  Id = 2,
                  Departure = DateTime.Now.AddDays(2),
                  Return_station = DateTime.Now.AddDays(3),
                  Departure_station_id = 2,
                  Departure_station_name = "Station 2",
                  Return_station_id = 3,
                  Return_station_name = "Station 3",
                  Covered_distance = 20,
                  Duration = 240 },
              new Journey {
                  Id = 3,
                  Departure = DateTime.Now.AddDays(4),
                  Return_station = DateTime.Now.AddDays(5),
                  Departure_station_id = 3,
                  Departure_station_name = "Station 3",
                  Return_station_id = 4,
                  Return_station_name = "Station 4",
                  Covered_distance = 30,
                  Duration = 360 },
            });
            _context.SaveChanges();
            var controller = new JourneyController(_context);
            var result = await controller.GetAllJourneys();
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            var journeys = okResult.Value as List<Journey>;
            Assert.IsNotNull(journeys);
            Assert.AreEqual(3, journeys.Count);
            Assert.IsTrue(journeys.Any(j => j.Id == 3 && j.Departure_station_name == "Station 3"));



        }

        [TestMethod()]
        public async Task GetJourneyTestAsync()
        {
            var existingJourney = new Journey
            {
                Id = 4,
                Departure = DateTime.Now.AddDays(4),
                Return_station = DateTime.Now.AddDays(5),
                Departure_station_id = 4,
                Departure_station_name = "Station 4",
                Return_station_id = 5,
                Return_station_name = "Station 5",
                Covered_distance = 30,
                Duration = 360            
            };
            var options = new DbContextOptionsBuilder<CityBikeContext>()
                .UseInMemoryDatabase(databaseName: "GetJourney")
                .Options;
            using (var context = new CityBikeContext(options))
            {
                await context.Journeys.AddAsync(existingJourney);
                await context.SaveChangesAsync();

                var controller = new JourneyController(context);
                var result = await controller.GetJourney(existingJourney.Id);
                Assert.IsInstanceOfType(result, typeof(OkObjectResult));
                var okResult = (OkObjectResult)result;
                Assert.AreEqual(existingJourney, okResult.Value);
            }

        }
    }
}