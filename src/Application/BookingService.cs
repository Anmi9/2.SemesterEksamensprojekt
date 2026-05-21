using App.Data.Repositories;
using App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application
{
    public class BookingService
    {
        private readonly BookingRepository _repository;
        public BookingService(BookingRepository repo)
        {
            _repository = repo;
        }
        public void CreateBooking(int VehicleId)
        {
            Booking booking = new Booking
            {
                Start = DateTime.Now,
                End = DateTime.Now + TimeSpan.FromHours(2),
                VehicleId = VehicleId
                //EmployeeID = ?
            };
            _repository.DBCreate(booking);
        }
        public void CreateBooking(DateTime start, DateTime end, int VehicleId)
        {
            Booking booking = new Booking
            {
                Start = start,
                End = end,
                VehicleId = VehicleId
                //EmployeeID = ?
            };
            _repository.DBCreate(booking);
        }
    }
}
