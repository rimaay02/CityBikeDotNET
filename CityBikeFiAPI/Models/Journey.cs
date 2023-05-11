namespace CityBikeAPI.Models
{
    public class Journey
    {
        public int Id { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Return_station { get; set; }
        public int Departure_station_id { get; set; }
        public string? Departure_station_name { get; set; }
        public int Return_station_id { get; set; }
        public string? Return_station_name { get; set; }
        public int Covered_distance { get; set; }
        public int Duration { get; set; }
}
}
