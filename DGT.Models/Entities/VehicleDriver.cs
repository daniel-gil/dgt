using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DGT.Models
{
    public class VehicleDriver
    { 
        public string Id { get; set; }

        public string VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public string DriverId { get; set; }
        public Driver Driver { get; set; }
    }
}
