using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models
{
    public enum VehicleType
    {
        Car,
        Bike,
    }

    internal class Vehicle
    {

        public int VehicleId { get; set; }
        public required string Type { get; set; } 
        public required string LicensePlate { get; set; } 

    }
}
