using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CityBikeAPI.Models
{
    public class Station
    {
        [Key]
        public int FID { get; set; }
        public int ID { get; set; }
        public string? Nimi { get; set; }
        public string? Namn { get; set; }
        public string? Name { get; set; }
        public string? Osoite { get; set; }
        public string? Adress { get; set; }
        public string? Kaupunki { get; set; }
        public string? Stad { get; set; }
        public string? Operaattor { get; set; }
        public int Kapasiteet { get; set; }
        public decimal X { get; set; }
        public decimal Y { get; set; }
    }
}
