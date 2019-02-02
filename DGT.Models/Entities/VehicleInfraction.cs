using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DGT.Models
{
    public class VehicleInfraction : IEntityBase
    {
        public string Id { get; set; }
        public DateTime InfractionDate { get; set; }

        [JsonIgnore]
        public Vehicle Vehicle { get; set; }
        public string VehicleId { get; set; }

        [JsonIgnore]
        public Infraction Infraction { get; set; }
        public string InfractionId { get; set; }
    }
}
