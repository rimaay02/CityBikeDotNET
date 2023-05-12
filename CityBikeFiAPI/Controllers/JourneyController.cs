using CityBikeAPI.Data;
using CityBikeAPI.Models;
using CityBikeFiAPI.Models;
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
            var journeys = await db.Journey.Take(500).ToListAsync();
            var journeyViewModels = journeys.Select(journey => new JourneyView
            {
                DepartureStation = journey.Departure_station_name,
                ReturnStation = journey.Return_station_name,
                CoveredDistance = (decimal)(journey.Covered_distance / 1000.0),
                Duration = journey.Duration / 60
            }).ToList();
            return Ok(journeyViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewJourney([FromBody] JourneyEntity journeyEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await db.AddAsync(journeyEntity);
            await db.SaveChangesAsync();
            return Ok();
        }
    }
}
