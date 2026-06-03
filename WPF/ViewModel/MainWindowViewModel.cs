using App.Application;
using App.Models;
using WPF.Commands;

namespace App.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        //Fields
        private readonly BookingService _bookingService; //Kommunikation med databsen
        private bool _isCarAvailable; //Fortæller om der er bil/cykel ledig lige nu, opdateres når LoadSync kører
        private bool _isBikeAvailable;

        //Properties
        public RelayCommand BookCarCommand { get; } //Objekter som xaml kan bindes til - wpf kalder execute på commanden og canExecute for om den er tilgængelig
        public RelayCommand BookBikeCommand { get; }
        public RelayCommand OpenCreateBookingCommand { get; }

        //Constructors
        public MainWindowViewModel(BookingService bookingservice)
        {
            _bookingService = bookingservice;
            BookCarCommand = new RelayCommand(ExecuteBookCar, CanBookCar); //Commands oprettes og metoder sendes som argumenter
            BookBikeCommand = new RelayCommand(ExecuteBookBike, CanBookBike);
            OpenCreateBookingCommand = new RelayCommand(ExecuteOpenCreateBooking);
            _ = LoadAsync(); //ignorer task objektet og fortsæt
        }

        //Methods
        private bool CanBookCar(object? parameter) //kaldes automatisk af wpf, true = grøn knap og muligt at klikke, false = grå ikke muligt at klikke, parameter bruges ikke
        {
            return _isCarAvailable;
        }

        private bool CanBookBike(object? parameter)
        {
            return _isBikeAvailable;
        }

        private async void ExecuteBookCar(object? parameter) //når der klikkes på knappen, relaycommand forventer void
        {
            await ExecuteBookAsync(VehicleTypes.Car);
        }
        private async void ExecuteBookBike(object? parameter)
        {
            await ExecuteBookAsync(VehicleTypes.Bike);
        }

        private async Task ExecuteBookAsync(VehicleTypes type) //fælles metode til begge knapper, loadAsync bruges til at opdatere tilgængelighed
        {
            var (start, end) = GetQuickBookingPeriod();

            var booking = await _bookingService.TryBookOptimalVehicleAsync(start, end, type);
            if (booking == null)
            {
                StatusMessage = "Ingen ledige køretøjer i den valgte periode.";
            }
            else
            {
                StatusMessage = $"{booking.Vehicle!.Type} bekræftet: {booking.Vehicle.LicensePlate}\nPeriode: {booking.Start:HH:mm} – {booking.End:HH:mm}";
            }
            await LoadAsync();
        }

        private void ExecuteOpenCreateBooking(object? parameter)
        {
            var createBookingView = new WPF.CreateBookingView(new CreateBookingViewModel(_bookingService));
            createBookingView.Show();
        }

        private async Task LoadAsync() //bruges for at kunne vise om der er ledige køretøjer i de næste 2 timer
        {
            var (start, end) = GetQuickBookingPeriod();

            IEnumerable<Vehicle> carResult = await _bookingService.GetAvailableVehicles(start, end, VehicleTypes.Car); // alle køretøjer 
            List<Vehicle> cars = carResult.ToList(); //laves til liste så den kan bruges Count på den

            IEnumerable<Vehicle> bikeResult = await _bookingService.GetAvailableVehicles(start, end, VehicleTypes.Bike);
            List<Vehicle> bikes = bikeResult.ToList();

            _isCarAvailable = cars.Count > 0; //tilgængeligheden er true hvis over 0 på listen
            _isBikeAvailable = bikes.Count > 0;

            BookCarCommand.RaiseCanExecuteChanged(); //fortæller wpf at den skal kalde CanBook igen, for at se om den har ændret sig
            BookBikeCommand.RaiseCanExecuteChanged();

        }
    }
}
