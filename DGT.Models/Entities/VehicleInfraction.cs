using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DGT.Models
{
    public class VehicleInfraction : IEntityBase
    {
        public string Id { get; set; } 

        public string VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public string InfractionId { get; set; }
        public Infraction Infraction { get; set; }
    }
}
