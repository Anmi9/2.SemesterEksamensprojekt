using App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.ViewModel
{
    public class CreateBookingViewModel
    {
        private DateTime _start;
        private DateTime _end;
        private VehicleType _type;

        private string TakeUserInput() => throw new NotImplementedException();
        private (bool, bool) AvailableTypes() => throw new NotImplementedException(); // Skal vi bruge unavngivne variabler i tuble eller er det bedre DX med navne?
        private int SelectVehicle() => throw new NotImplementedException(); // Algoritmisk vælger den optimale vehicle blandt de ledige. Skal navnet udpensle at det er den optimale fartøj der vælges?
        // handler metode  til click events fra view

    }
}
