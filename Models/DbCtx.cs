using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using Npgsql;
using NpgsqlTypes;

namespace VehicleNetBackend {
    public class DbCtx : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<LocationRecord> LocationRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("postgis");
        }
    }

    [Table("vehicles")]
    public class Vehicle
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("secrettoken")]
        public string SecretToken { get; set; }

        [InverseProperty("vehicleid")]
        public List<LocationRecord> LocationRecords { get; set; }
    }

    [Table("location_records")]
    public class LocationRecord
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("location")]
        public Point Location { get; set; }

        [Column("vehicleid")]
        public long VehicleId { get; set; }

        [ForeignKey("vehicleid")]
        public Vehicle Vehicle { get; set; }
    }
}
