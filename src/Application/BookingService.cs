using App.Data.Repositories;
using App.Models;
using App.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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


        //Algoritmemetode, der finder det mest optimale køretøj baseret på eksisterende bookinger og den nye booking's start- og sluttidspunkt.
        public Vehicle FindBestOptimalVehicle(CreateBookingViewModel viewModel, List<Booking> allActiveBookings)
            {
                DateTime newBookingStart = viewModel.Start.Value;  
                DateTime newBookingEnd = viewModel.End.Value;

                Vehicle optimalVehicle = null;
                TimeSpan smallestGap = TimeSpan.MaxValue;    //Vi starte med at sætte den mindste gap til max value, så vi kan sammenligne med de faktiske gaps mellem bookingerne.

            foreach (var vehicle in viewModel.AvailableVehicles) //Iterer gennem alle køretøjerne i AvailableVehicles-listen.
                {
                    var vehicleBookings = allActiveBookings                                            
                        .Where(b => b.VehicleId == vehicle.VehicleId) // Behold bookinger, hvis VehicleId matcher det aktuelle køretøjs VehicleId.
                        .ToList(); 

                    var previousBooking = vehicleBookings // Leder efter tidligere booking, der slutter før den nye booking starter
                        .Where(b => b.End <= newBookingStart) // Behold bookinger, hvis sluttidspunkt er mindre end eller lig med den nye booking's starttidspunkt.
                        .OrderByDescending(b => b.End) //Vi sorterer baglæns for at få den seneste booking, der slutter før den nye booking starter.
                        .FirstOrDefault(); 

                    var nextBooking = vehicleBookings
                        .Where(b => b.Start >= newBookingEnd)
                        .OrderBy(b => b.Start) // Vi sorterer kronologisk for at få den tidligeste booking efter den nye booking slutter
                        .FirstOrDefault();

                DateTime gapStart = previousBooking != null ? previousBooking.End : DateTime.MinValue; //Ternary operator (:) i stedet for en if/else-blok
                DateTime gapEnd = nextBooking != null ? nextBooking.Start : DateTime.MaxValue;

                TimeSpan gap = gapEnd - gapStart;

                if (gap < smallestGap) // Hvis det beregnede gap er mindre end det mindste gap, vi har fundet indtil nu, opdaterer vi smallestGap og optimalVehicle.
                {
                    smallestGap = gap;
                    optimalVehicle = vehicle;
                }
            }

            return optimalVehicle;
        }

    }



}
    

