﻿namespace DGT.Models
{
    public class Infraction : IEntityBase
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public sbyte PointsToDiscount { get; set; }
    }
}
