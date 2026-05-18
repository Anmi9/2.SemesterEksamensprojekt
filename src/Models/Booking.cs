using System;
using System.Collections.Generic;
using System.Text;

namespace App.Models
{
    internal class Booking
    {
        public int BookingId { get; set; }
        public int EmployeeId { get; set; }
        public int VehicleId { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

    }
}
