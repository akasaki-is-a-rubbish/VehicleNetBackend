using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WhateverBackend {
    public class DbCtx : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=data/data.db");
    }

    public class Vehicle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Secret { get; set; }
    }
}
