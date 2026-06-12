using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace App.Models
{
    [Flags]
    public enum VehicleTypes
    {
        None    = 0, // Alle bit indekser er tomme
        Car     = 1, // Bit index 0
        Bike    = 2 // Bit index 1
    }
    /// <summary>
    /// 
    /// </summary>
    /// <author>Matias</author>
    public class Vehicle
    {
        public int VehicleId { get; init; }
        public required string LicensePlate { get; init; }
        public required VehicleTypes Type
        {
            get;
            init // Hvis køretøjet får tildelt mere end en type (én aktiv bit) fejler den
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
