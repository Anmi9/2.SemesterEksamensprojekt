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
    internal class VehicleRepository // Overvej om denne klasse skal hedde vehiclerepository?
    {
        private readonly Context _context;

        public VehicleRepository(Context context)   // Dependency injection af context-objektet
        {
            _context = context; //gemmer objektet i en privat variabel, så det kan bruges i metoderne i klassen
        }   

        public async Task<List<Vehicle>> GetVehicles(DateTime StartDate, DateTime EndDate) // Metode returnerer liste af ledige køretøjer i et bestemt tidsrum
        {
            return await _context.Vehicles
                .Where(v => !_context.Bookings.Any(b =>
                    b.VehicleId == v.VehicleId && //Bookingens bil-id svarer til denne bils id
                    b.Start < EndDate && // En booking starter før den ønskede slutdato
                    b.End > StartDate)) // En booking slutter efter den ønskede startdato
                .ToListAsync(); 






        }
    }
}
