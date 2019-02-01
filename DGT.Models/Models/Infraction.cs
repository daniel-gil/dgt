using System;
using System.Collections.Generic;
using System.Text;

namespace DGT.Models
{
    public class Infraction
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public sbyte PointsToDiscount { get; set; }
    }
}
