﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DGT.Models
{
    public class Vehicle
    {
        [Display(Name = "Matricula")]
        public string Id { get; set; } // License Plate
        public string Brand { get; set; }
        public string Model { get; set; }
        public List<Driver> Drivers { get; set; }
    }
}
