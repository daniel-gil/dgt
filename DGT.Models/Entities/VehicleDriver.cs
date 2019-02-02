namespace DGT.Models
{
    public class VehicleDriver : IEntityBase
    { 
        public string Id { get; set; }

        public string VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public string DriverId { get; set; }
        public Driver Driver { get; set; }
    }
}
