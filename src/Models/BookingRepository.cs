using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using App.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace App.Models
{
    internal class BookingRepository // Overvej om denne klasse skal hedde vehiclerepository?
    {
        private readonly Context _context;

        public BookingRepository(Context context) 
        {
            _context = context;
        }   

        public async Task<List<Vehicle>> GetVehicles(DateTime StartDate, DateTime EndDate)
        {
            return await _context.Vehicles
                .Where(v => !_context.Bookings.Any(b =>
                    b.VehicleId == v.VehicleId &&
                    b.Start < EndDate &&
                    b.End > StartDate)) 
                .ToListAsync();






        }
    }
}
