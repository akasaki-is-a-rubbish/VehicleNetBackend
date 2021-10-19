using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleNetBackend
{
    [Table("users")]
    public class User
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("username")]
        public string Username { get; set; }

        [Column("passwd")]
        public string Password { get; set; }

        [InverseProperty("owner")]
        public List<Vehicle> Vehicles { get; set; }
    }


    [Table("logins")]
    public class LoginRecord
    {
        [Key]
        public string token { get; set; }
        public DateTime login_date { get; set; }
        public DateTime last_used { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
