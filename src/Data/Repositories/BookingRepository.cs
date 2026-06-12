using System;
using System.Collections.Generic;
using System.Text;
using App.Models;
using SQLitePCL;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Repositories
{
    public class BookingRepository
    {
        private readonly Context _context;
        public BookingRepository(Context context) //Constructor der tager context med, så vi kan sikre at vi har adgang til databasen når BookingRepository oprettes.
        {
            _context = context;
        }

        public async Task DBCreateAsync(Booking booking)
        {
            _context.Bookings.Add(booking); //Tilføjer den til databasen, men EFcore holder det kun i "hukommelsen"
            await _context.SaveChangesAsync(); //Objektet oversættes til SQL (INSERT INTO Bookings (VehiID, start, end) VALUES (.., ..., ..,) og EFcore lukker selv adgangen
           
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
