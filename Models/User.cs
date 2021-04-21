using System.Collections.Generic;
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

        [InverseProperty("vehicleid")]
        public List<LocationRecord> LocationRecords { get; set; }
    }
}
