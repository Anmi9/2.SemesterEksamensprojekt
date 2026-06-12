
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using App.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using App.Models;

namespace App.Data.Repositories
{
    public class VehicleRepository 
    {
        private readonly Context _context;

        public VehicleRepository(Context context)   // Dependency injection af context-objektet
        {
            _context = context; 
        }   

        public async Task<IEnumerable<Vehicle>> GetAvailableVehiclesAsync(DateTime StartDate, DateTime EndDate, VehicleTypes? Type = null) // Metode returnerer liste af ledige køretøjer i et bestemt tidsrum
        {
            var query = _context.Vehicles
                .Where(v => !_context.Bookings.Any(b =>
                    b.VehicleId == v.VehicleId && 
                    b.Start < EndDate && 
                    b.End > StartDate)); 
                
                if (Type.HasValue) // HasValue er en property til nullabletyper - den er enten true eller false
                {
                    query = query.Where(v => v.Type == Type.Value); 
                }

                return await query.ToListAsync(); 
        }
    }
}
