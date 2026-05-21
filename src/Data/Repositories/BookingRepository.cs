using System;
using System.Collections.Generic;
using System.Text;
using App.Models;
using SQLitePCL;

namespace App.Data.Repositories
{
    public class BookingRepository
    {
        private readonly Context _context;
        public BookingRepository(Context context) //Constructor der tager context med, så vi kan sikre at vi har adgang til databasen når BookingRepository oprettes.
        {
            _context = context;
        }

        public void DBCreate(Booking booking)
        {
            _context.Bookings.Add(booking); //Tilføjer den til databasen, men EFcore holder det kun i "hukommelsen"
            _context.SaveChanges(); //Objektet oversættes til SQL (INSERT INTO Bookings (VehiID, start, end) VALUES (.., ..., ..,) og EFcore lukker selv adgangen
            // TODO: opret context i root og send dem til repos. (App.xaml.spørgsmålstegn
        }
    }
}
