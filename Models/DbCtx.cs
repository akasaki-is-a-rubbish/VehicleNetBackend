using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;

namespace VehicleNetBackend
{
    public class DbCtx : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<LoginRecord> Logins { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<LocationRecord> LocationRecords { get; set; }

        public DbCtx(DbContextOptions options) : base(options)
        {
        }

        public Task<User> FindUser(string username) => findUser(this, username);

        public UserService UserService { get; internal set; }

        private static Func<DbCtx, string, Task<User>> findUser
            = EF.CompileAsyncQuery((DbCtx db, string username) =>
                db.Users.FirstOrDefault(u => u.Username == username)
            );

        public Task<LoginRecord> FindLogin(string token) => findLogin(this, token);
        private static Func<DbCtx, string, Task<LoginRecord>> findLogin
            = EF.CompileAsyncQuery((DbCtx db, string token) =>
                db.Logins.Include(l => l.User).FirstOrDefault(l => l.token == token)
            );

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("postgis");
        }
    }
}
