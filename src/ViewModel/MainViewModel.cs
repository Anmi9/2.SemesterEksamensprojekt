using System;
using System.Collections.Generic;
using System.Text;

namespace App.ViewModel
{
    public class MainViewModel
    {
        private string TakeUserInput();
        private (bool, bool) AvailableTypes(); // Skal vi bruge unavngivne variabler i tuble eller er det bedre DX med navne?
        private int SelectVehicle(); // Algoritmisk vælger den optimale vehicle blandt de ledige. Skal navnet udpensle at det er den optimale fartøj der vælges?
        // handler metode  til click events fra view
    }
}
