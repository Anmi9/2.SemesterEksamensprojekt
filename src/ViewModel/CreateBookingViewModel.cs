using App.Models;
using System;
using System.Collections.Generic;
using System.Text;
using App.Application;

namespace App.ViewModel
{
    public class CreateBookingViewModel
    {
        private readonly BookingService _bookingService;

        public CreateBookingViewModel(BookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public DateTime? Start {  get; set; }
        public DateTime? End { get; set; }
        public VehicleType Type { get; set; }

        public List<Vehicle> AvailableVehicles { get; set; } // Liste over ledige valgte typer, der sendes med som parameter til algoritme.

        public void Book()
        {
            if (Start == null || End == null) return;
            _bookingService.CreateBooking(Start.Value, End.Value, 1);
        }
        private string TakeUserInput() => throw new NotImplementedException();
        private (bool, bool) AvailableTypes() => throw new NotImplementedException(); // Skal vi bruge unavngivne variabler i tuble eller er det bedre DX med navne?
        private int SelectVehicle() => throw new NotImplementedException(); // Algoritmisk vælger den optimale vehicle blandt de ledige. Skal navnet udpensle at det er den optimale fartøj der vælges?
        // handler metode  til click events fra view

    }
}
