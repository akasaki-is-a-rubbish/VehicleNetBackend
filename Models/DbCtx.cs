using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;

namespace VehicleNetBackend
{
    public class DbCtx : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<LocationRecord> LocationRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("postgis");
        }
    }
}
