using System;
using System.Collections.Generic;
using System.Text;

namespace DGT.Models
{
    public class Infraction : IEntityBase
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public sbyte PointsToDiscount { get; set; }

        //public ICollection<Vehicle> Vehicles { get; set; }

        // public Vehicle Vehicle { get; set; }
        //public string VehicleId { get; set; }
    }
}
