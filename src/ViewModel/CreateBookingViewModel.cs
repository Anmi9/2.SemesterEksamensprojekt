using App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.ViewModel
{
    public class CreateBookingViewModel
    {
        public DateTime? Start {  get; set; }
        public DateTime? End { get; set; }
        public VehicleType Type { get; set; }

        private string TakeUserInput() => throw new NotImplementedException();
        private (bool, bool) AvailableTypes() => throw new NotImplementedException(); // Skal vi bruge unavngivne variabler i tuble eller er det bedre DX med navne?
        private int SelectVehicle() => throw new NotImplementedException(); // Algoritmisk vælger den optimale vehicle blandt de ledige. Skal navnet udpensle at det er den optimale fartøj der vælges?
        // handler metode  til click events fra view

    }
}
