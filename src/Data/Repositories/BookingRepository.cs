using System;
using System.Collections.Generic;
using System.Text;
using App.Models;
using SQLitePCL;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Repositories
{  /// <summary>
   /// 
   /// </summary>
   /// <author>Anna</author>
    public class BookingRepository
    {
        private readonly Context _context;
        public BookingRepository(Context context) 
        {
            _context = context;
        }

        public async Task DBCreateAsync(Booking booking)
        {
            _context.Bookings.Add(booking); 
            await _context.SaveChangesAsync(); 
           
        }

        public async Task<List<Booking>> DBGetAllBookingsAsync()
        {
            return await _context.Bookings.ToListAsync(); //Henter alle bookinger fra databasen og returnerer dem som en liste.
        }

        public async Task<bool> DBIsVehicleAvailableAtTimeAsync(int vehicleId, DateTime start, DateTime end)
        {
            bool isOverlapping = await _context.Bookings
                .AnyAsync(b => b.VehicleId == vehicleId &&
                b.Start < end && 
                b.End > start);

            return !isOverlapping;
        }


    }
}
