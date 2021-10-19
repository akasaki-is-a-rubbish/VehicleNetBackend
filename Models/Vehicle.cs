using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleNetBackend
{
    [Table("vehicles")]
    public class Vehicle
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("owner")]
        public long Owner { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("secrettoken")]
        public string SecretToken { get; set; }

        [InverseProperty("vehicleid")]
        public List<LocationRecord> LocationRecords { get; set; }
    }
}
