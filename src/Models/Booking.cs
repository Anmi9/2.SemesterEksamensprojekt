using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models
{
    /// <summary>
    /// Booking
    /// </summary>
    /// <author>Lasse</author>
    public class Booking
    {
        public int BookingId { get; set; }
        public int EmployeeId { get; set; }
        public int VehicleId { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Vehicle? Vehicle { get; set; } //navigations property til at kunne vise nummerplade i bekræftigelsen

    }
}
