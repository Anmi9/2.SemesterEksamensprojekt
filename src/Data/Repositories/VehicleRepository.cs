
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
    public class VehicleRepository // Overvej om denne klasse skal hedde vehiclerepository?
    {
        private readonly Context _context;

        public VehicleRepository(Context context)   // Dependency injection af context-objektet
        {
            _context = context; //gemmer objektet i en privat variabel, så det kan bruges i metoderne i klassen
        }   

        public async Task<IEnumerable<Vehicle>> GetAvailableVehiclesAsync(DateTime StartDate, DateTime EndDate, VehicleTypes? Type = null) // Metode returnerer liste af ledige køretøjer i et bestemt tidsrum
        {
            var query = _context.Vehicles
                .Where(v => !_context.Bookings.Any(b =>
                    b.VehicleId == v.VehicleId && //Bookingens bil-id svarer til denne bils id
                    b.Start < EndDate && // En booking starter før den ønskede slutdato
                    b.End > StartDate)); // En booking slutter efter den ønskede startdato
                
                if (Type.HasValue) // HasValue er en property til nullabletyper - den er enten true eller false
                {
                    query = query.Where(v => v.Type == Type.Value); // For hvert køretøj der er ledigt, tjekkes det om det er den ønskede værdi (Value)
                }

                return await query.ToListAsync(); 
        }
    }
}
