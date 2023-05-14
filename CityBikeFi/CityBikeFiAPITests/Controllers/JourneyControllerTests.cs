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
using Microsoft.AspNetCore.Http;
using CityBikeFiAPI.Models;
using Microsoft.Extensions.Options;

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
            _context?.Journey.AddRange(new List<JourneyEntity>
            {
              new JourneyEntity {
                  Departure = DateTime.Now,
                  Return = DateTime.Now.AddDays(1),
                  Departure_station_id = 1,
                  Departure_station_name = "Station 1",
                  Return_station_id = 2,
                  Return_station_name = "Station 2",
                  Covered_distance = 10,
                  Duration = 120 },
              new JourneyEntity {
                  Departure = DateTime.Now.AddDays(2),
                  Return = DateTime.Now.AddDays(3),
                  Departure_station_id = 2,
                  Departure_station_name = "Station 2",
                  Return_station_id = 3,
                  Return_station_name = "Station 3",
                  Covered_distance = 20,
                  Duration = 240 },
              new JourneyEntity {
                  Departure = DateTime.Now.AddDays(4),
                  Return = DateTime.Now.AddDays(5),
                  Departure_station_id = 3,
                  Departure_station_name = "Station 3",
                  Return_station_id = 4,
                  Return_station_name = "Station 4",
                  Covered_distance = 30,
                  Duration = 360 },
            });
            _context.SaveChanges();
            using (var context = _context)
            {
                var controller = new JourneyController(context);

                // Act
                var result = await controller.GetAllJourneys();

                // Assert
                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(OkObjectResult));

                var okResult = result as OkObjectResult;
                var journeys = okResult.Value as List<JourneyView>;

                Assert.AreEqual(3, journeys.Count);
            }
        }

      
    }

}