using App.Data.Repositories;
using App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application
{
    public class BookingService
    {
        public void CreateBooking(int VehicleId)
        {
            var repo = new BookingRepository();
            Booking booking = new Booking
            {
                Start = DateTime.Now,
                End = DateTime.Now + TimeSpan.FromHours(2),
                VehicleId = VehicleId
                //EmployeeID = ?
            }; 
            repo.DBCreate(booking);
        }
        public void CreateBooking(DateTime start, DateTime end, int VehicleId)
        {
            var repo = new BookingRepository();
            Booking booking = new Booking
            {
                Start = start,
                End = end,
                VehicleId = VehicleId
                //EmployeeID = ?
            };
            repo.DBCreate(booking);
        }
    }
}
