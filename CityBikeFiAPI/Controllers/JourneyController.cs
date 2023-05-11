using CityBikeAPI.Data;
using CityBikeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CityBikeAPI.Controllers
{
    [Route("/api/[controller]")]
    public class JourneyController : InjectedController
    {
        public JourneyController(CityBikeContext context) : base(context) { }
       
        [HttpGet]
        public async Task<IActionResult> GetAllJourneys()
        {
            var journeys = await db.Journeys.ToListAsync();
            return Ok(journeys);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetJourney(int id)
        {
            var journey = await db.Journeys.FindAsync(id);
            if (journey == default(Journey))
            {
                return NotFound();
            }
            return Ok(journey);
        }
        
    }
}
