using CityBikeAPI.Data;
using CityBikeAPI.Models;
using CityBikeFiAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CityBikeAPI.Controllers
{
    [Route("/api/[controller]")]
    public class StationController : InjectedController
    {
        public StationController(CityBikeContext context) : base(context) { }
        
        [HttpGet]
        public async Task<IActionResult> GetAllStations()
        {
            var stations = await db.Stations.ToListAsync();
            return Ok(stations);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStation(int id)
        {
            var station = await db.Stations.FindAsync(id);
            if (station == default(Station))
            {
                return NotFound();
            }
            var stationInfo = new StationInfo
            {
                Name = station.Name,
                Address = station.Adress,
                TotalJourneysStarting = await db.Journey.CountAsync(j => j.Departure_station_id == id),
                TotalJourneysEnding = await db.Journey.CountAsync(j => j.Return_station_id == id),
            };

            return Ok(stationInfo);
        }

       
    }

}

