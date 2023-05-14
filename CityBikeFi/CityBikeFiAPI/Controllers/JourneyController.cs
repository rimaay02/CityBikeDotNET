using CityBikeAPI.Data;
using CityBikeAPI.Models;
using CityBikeFiAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CityBikeAPI.Controllers
{
    [Route("/api/[controller]")]
    public class JourneyController : InjectedController
    {
        public JourneyController(CityBikeContext context) : base(context) { }

        [EnableCors]
        [HttpGet]
        public async Task<IActionResult> GetAllJourneys()
        {
            var journeys = await db.Journey.Take(30).ToListAsync();
            var journeyViewModels = journeys.Select(journey => new JourneyView
            {
                DepartureStation = journey.Departure_station_name,
                ReturnStation = journey.Return_station_name,
                CoveredDistance = (decimal)(journey.Covered_distance / 1000.0),
                Duration = journey.Duration / 60
            }).ToList();
            return Ok(journeyViewModels);
        }
             
    }
}
