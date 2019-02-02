using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DGT.Models
{
    public class Driver : IEntityBase
    {
        [Display(Name = "DNI")]
        public string Id { get; set; } // DNI
        public string Name { get; set; }
        public string Surname { get; set; }
        public sbyte Points { get; set; }
        public sbyte NumVehicles { get; set; }

        public string FullName()
        {
            return Name + " " + Surname;
        }
    }
}
