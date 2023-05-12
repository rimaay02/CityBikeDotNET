using CityBikeAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CityBikeAPI.Data
{
    public class CityBikeContext: DbContext
    {
        public DbSet<Station> Stations { get; set; }

        public DbSet<JourneyEntity> Journey { get; set; }

        public CityBikeContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JourneyEntity>()
                        .HasOne<Station>()
                        .WithMany()
                        .HasForeignKey(j => j.Departure_station_id);

            modelBuilder.Entity<JourneyEntity>()
                        .HasOne<Station>()
                        .WithMany()
                        .HasForeignKey(j => j.Return_station_id);
           
            modelBuilder.Entity<JourneyEntity>()
                .HasKey(j => new { j.Departure_station_id, j.Return_station_id });

        }
    }
}
