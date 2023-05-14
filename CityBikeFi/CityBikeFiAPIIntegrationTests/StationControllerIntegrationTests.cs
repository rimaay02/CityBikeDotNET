using CityBikeAPI.Controllers;
using CityBikeAPI.Data;
using CityBikeAPI.Models;
using CityBikeFiAPI.Models;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;

namespace CityBikeAPI.Controllers.Tests
{
    public class StationControllerIntegrationTests
    {
        private readonly string _connectionString = "Server=localhost;port=3306; Database=CityBikeTestDbCON; Uid=root; Pwd=1234";
        private DbContextOptions<CityBikeContext> _options;
        private CityBikeContext? _context;

        public StationControllerIntegrationTests()
        {           
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 25));

            _options = new DbContextOptionsBuilder<CityBikeContext>()
                .UseMySql(_connectionString, serverVersion)
                .Options;

            _context = new CityBikeContext(_options);
            _context.Database.EnsureCreated();
            _context.Journey.RemoveRange(_context.Journey);
            _context.Stations.RemoveRange(_context.Stations);
            _context.SaveChanges();
            SeedDatabaseAsync().Wait(); // wait for the seed data to be populated

        }
        private async Task SeedDatabaseAsync()
        {
            var stations = new List<Station>
            {
                new Station
                {
                    FID = 1,
                    ID = 101,
                    Nimi = "Station 1",
                    Namn = "Station 1",
                    Name = "Station 1",
                    Osoite = "Address 1",
                    Adress = "Address 1",
                    Kaupunki = "City 1",
                    Stad = "City 1",
                    Operaattor = "Operator 1",
                    Kapasiteet = 10,
                    X = 60.1654M,
                    Y = 24.9384M
                },
                new Station
                {
                    FID = 2,
                    ID = 102,
                    Nimi = "Station 2",
                    Namn = "Station 2",
                    Name = "Station 2",
                    Osoite = "Address 2",
                    Adress = "Address 2",
                    Kaupunki = "City 2",
                    Stad = "City 2",
                    Operaattor = "Operator 2",
                    Kapasiteet = 20,
                    X = 60.1699M,
                    Y = 24.9525M
                },
                new Station
                {
                    FID = 3,
                    ID = 103,
                    Nimi = "Station 3",
                    Namn = "Station 3",
                    Name = "Station 3",
                    Osoite = "Address 3",
                    Adress = "Address 3",
                    Kaupunki = "City 3",
                    Stad = "City 3",
                    Operaattor = "Operator 3",
                    Kapasiteet = 30,
                    X = 60.1645M,
                    Y = 24.9461M
                },
                new Station
                {
                    FID = 4,
                    ID = 104,
                    Nimi = "Station 4",
                    Namn = "Station 4",
                    Name = "Station 4",
                    Osoite = "Address 4",
                    Adress = "Address 4",
                    Kaupunki = "City 4",
                    Stad = "City 4",
                    Operaattor = "Operator 4",
                    Kapasiteet = 40,
                    X = 60.1711M,
                    Y = 24.9315M
                },
                new Station
                {
                    FID = 5,
                    ID = 105,
                    Nimi = "Station 5",
                    Namn = "Station 5",
                    Name = "Station 5",
                    Osoite = "Address 5",
                    Adress = "Address 5",
                    Kaupunki = "City 5",
                    Stad = "City 5",
                    Operaattor = "Operator 5",
                    Kapasiteet = 50,
                    X = 60.1632M,
                    Y = 24.9402M
                }
            };
            List<JourneyEntity> journeys = new List<JourneyEntity>
            {
                new JourneyEntity
                {
                    Departure = new DateTime(2023, 5, 12, 10, 0, 0),
                    Return = new DateTime(2023, 5, 12, 10, 30, 0),
                    Departure_station_id = 1,
                    Departure_station_name = "Station 1",
                    Return_station_id = 2,
                    Return_station_name = "Station 2",
                    Covered_distance = 2000,
                    Duration = 30
                },
                new JourneyEntity
                {
                    Departure = new DateTime(2023, 5, 12, 11, 0, 0),
                    Return = new DateTime(2023, 5, 12, 11, 15, 0),
                    Departure_station_id = 3,
                    Departure_station_name = "Station 3",
                    Return_station_id = 5,
                    Return_station_name = "Station 5",
                    Covered_distance = 1500,
                    Duration = 15
                },
                new JourneyEntity
                {
                    Departure = new DateTime(2023, 5, 12, 12, 0, 0),
                    Return = new DateTime(2023, 5, 12, 12, 45, 0),
                    Departure_station_id = 4,
                    Departure_station_name = "Station 4",
                    Return_station_id = 1,
                    Return_station_name = "Station 1",
                    Covered_distance = 3000,
                    Duration = 45
                },
                new JourneyEntity
                {
                    Departure = new DateTime(2023, 5, 12, 13, 0, 0),
                    Return = new DateTime(2023, 5, 12, 13, 10, 0),
                    Departure_station_id = 5,
                    Departure_station_name = "Station 5",
                    Return_station_id = 3,
                    Return_station_name = "Station 3",
                    Covered_distance = 1000,
                    Duration = 10
                },
                new JourneyEntity
                {
                    Departure = new DateTime(2023, 5, 12, 14, 0, 0),
                    Return = new DateTime(2023, 5, 12, 14, 20, 0),
                    Departure_station_id = 2,
                    Departure_station_name = "Station 2", 
                    Return_station_id = 4, 
                    Return_station_name = "Station 4", 
                    Covered_distance = 2500,
                    Duration = 20
                }
            };

            _context.Stations.AddRange(stations);
            _context.Journey.AddRange(journeys);
            _context.SaveChanges();

        }
        [Fact]
        public async Task GetAllStationsTest()
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                var controller = new StationController(_context);
                var result = await controller.GetAllStations();
                var okResult = result as OkObjectResult;
                Assert.NotNull(okResult);
                Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
                var stations = okResult.Value as List<Station>;
                Assert.NotNull(stations);
                Assert.Equal(5, stations.Count);

            }
        }
        [Fact]
        public async Task GetStationTest()
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                var serverVersion = new MySqlServerVersion(new Version(8, 0, 25));
                var db = new DbContextOptionsBuilder<CityBikeContext>()
                .UseMySql(_connectionString, serverVersion);
                var controller = new StationController(new CityBikeContext(_options));
                var result = await controller.GetStation(1);
                var okResult = result as OkObjectResult;
                var stationInfo = Assert.IsType<StationInfo>(okResult.Value);

                Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
                Assert.NotNull(stationInfo);
                Assert.Equal("Station 1", stationInfo?.Name);
                stationInfo.TotalJourneysStarting = await _context.Journey.CountAsync(j => j.Departure_station_id == 1);

                Assert.Equal(1, stationInfo?.TotalJourneysEnding);
            }
        }
        [Fact]
        public async Task AddNewStationAsyncTest()
        {
            var dbContext = new CityBikeContext(_options);
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
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal(expectedStationId, actualStationId);
        }

    }
}

