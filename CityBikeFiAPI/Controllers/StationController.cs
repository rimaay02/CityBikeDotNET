using CityBikeAPI.Data;
using CityBikeAPI.Models;
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
            return Ok(station);
        }

        [HttpPost]
        public async Task<IActionResult> AddStation([FromBody] Station station)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await db.AddAsync(station);
            await db.SaveChangesAsync();
            return Ok(station.ID);
        }
    }

}

