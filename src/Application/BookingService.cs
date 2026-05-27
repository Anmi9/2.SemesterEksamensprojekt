using App.Data.Repositories;
using App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application
{
    public class BookingService
    {
        private readonly BookingRepository _bookingRepo;
        private readonly VehicleRepository _vehicleRepo;

        public BookingService(BookingRepository bookingRepo, VehicleRepository vehicleRepo)
        {
            _bookingRepo = bookingRepo;
            _vehicleRepo = vehicleRepo;
        }
        public void CreateBooking(int VehicleId)
        {
            Booking booking = new Booking
            {
                Start = DateTime.Now,
                End = DateTime.Now + TimeSpan.FromHours(2),
                VehicleId = VehicleId,
                EmployeeId = 1
            };
            _bookingRepo.DBCreate(booking);
        }
        public void CreateBooking(DateTime start, DateTime end, int VehicleId)
        {
            Booking booking = new Booking
            {
                Start = start,
                End = end,
                VehicleId = VehicleId,
                EmployeeId = 1
            };
            _bookingRepo.DBCreate(booking);
        }
    }
}
