using System.Collections.Generic;

namespace DGT.WebApi.ViewModels
{
    public class VehicleViewModel
    {
        public string Id { get; set; } // Frame Number (Número bastidor)
        public string Brand { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
        public List<string> RegularDrivers { get; set; }
    }
}
