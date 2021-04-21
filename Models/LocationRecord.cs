using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;

namespace VehicleNetBackend
{
    [Table("location_records")]
    public class LocationRecord
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("vehicleid")]
        public long VehicleId { get; set; }

        [ForeignKey("vehicleid")]
        public Vehicle Vehicle { get; set; }

        [Column("location")]
        public Point Location { get; set; }

        [Column("speed")]
        public float Speed { get; set; }
    }
}
