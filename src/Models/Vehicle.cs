using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace App.Models
{
    [Flags]
    public enum VehicleTypes
    {
        None    = 0,
        Car     = 1,
        Bike    = 2
    }

    public class Vehicle
    {
        public required int VehicleId { get; init; }
        public required string LicensePlate { get; init; }
        public required VehicleTypes Type
        {
            get;
            init // Hvis køretøjet får tildelt mere end en type (aktive bits) fejler den
            {
                int activeBits = BitOperations.PopCount((uint)value); 
                if (activeBits != 1)
                {
                    throw new ArgumentException($"Et specifikt køretøj må kun have præcis én type fra {nameof(VehicleTypes)} aktiveret.");
                }
                field = value;
            }
        }

    }
}
