namespace DGT.Models
{
    public class Vehicle : IEntityBase
    {
        public string Id { get; set; } // Frame number (Número de bastidor)
        public string LicensePlate { get; set;}
        public string Brand { get; set; }
        public string Model { get; set; }
    }
}
