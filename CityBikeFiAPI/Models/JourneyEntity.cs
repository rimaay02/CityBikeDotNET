using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CityBikeAPI.Models
{
    public class JourneyEntity
    {
        public DateTime Departure { get; set; }
        public DateTime Return { get; set; }
        public int Departure_station_id { get; set; }
        public string? Departure_station_name { get; set; }
        public int Return_station_id { get; set; }
        public string? Return_station_name { get; set; }
        public int Covered_distance { get; set; }
        public int Duration { get; set; }
}
}
