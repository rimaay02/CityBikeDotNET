namespace CityBikeFiAPI.Models
{
    public class JourneyView
    {
        public string DepartureStation { get; set; }

        public string ReturnStation { get; set; }
        public decimal CoveredDistance { get; set; }
        public int Duration { get; set; }
    }
}
