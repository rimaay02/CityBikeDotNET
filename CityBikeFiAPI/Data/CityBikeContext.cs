using CityBikeAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CityBikeAPI.Data
{
    public class CityBikeContext: DbContext
    {
        public DbSet<Station> Stations { get; set; }

        public DbSet<Journey> Journeys { get; set; }

        public CityBikeContext(DbContextOptions options) : base(options) { }
    }
}
