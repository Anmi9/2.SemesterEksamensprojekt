using App.Data.Repositories;
using App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application
{
    internal class BookingService
    {

        public BookingService()
        {
            Booking booking = new Booking();

            //Dummy data for testing
            var start = new DateTime(2024, 6, 1, 8, 0, 0);
            var end = new DateTime(2024, 6, 1, 17, 0, 0);
            var Employee = 1;

            booking.Start = start;
            booking.End = end;
            booking.EmployeeId = Employee;

            VehicleRepository bookingRepository = new BookingRepository();
            bookingRepository.GetVehicles(booking);


        }




    }
}
