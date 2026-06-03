using App.Application;
using App.Models;
using System.Collections.ObjectModel;
using WPF.Commands;

namespace App.ViewModel
{
    public class CreateBookingViewModel : ViewModelBase
    {
        private readonly BookingService _bookingService;
        private const int _maxBookingDaysInFuture = 28;
        private const int _defaultWorkDayStartHour = 8;
        private static readonly TimeSpan _defaultDuration = TimeSpan.FromHours(1);
        private static readonly TimeSpan _minDuration = TimeSpan.FromMinutes(TimeSlotIntervalMinutes);
        private static readonly TimeSpan _endOfDay = TimeSpan.FromHours(24).Subtract(TimeSpan.FromMinutes(TimeSlotIntervalMinutes));

        static CreateBookingViewModel()
        {
            if (_defaultDuration <= _minDuration)
            {
                throw new TypeInitializationException(
                    nameof(CreateBookingViewModel),
                    new ArgumentException($"Kritisk invariant brudt: {nameof(_defaultDuration)} skal være større end {nameof(_minDuration)}."));
            }
        }

        public CreateBookingViewModel(BookingService service)
        {
            _bookingService = service;
            PopulateTimeSlots();

            RegisterBookingCommand = new RelayCommand(
                execute: async param => await ExecuteBookingAsync(param),
                canExecute: param => CanPlaceBooking(param)
            );
            Date = DateTime.Today;
        }

        public ObservableCollection<TimeSpan> TimeSlots { get; } = new();
        public DateTime MinDate { get; } = DateTime.Today;
        public DateTime MaxDate { get; } = DateTime.Today.AddDays(_maxBookingDaysInFuture);
        public RelayCommand RegisterBookingCommand { get; }

        public DateTime? Date
        {
            get;
            set
            {
                if (field == value) return;
                field = value;
                OnPropertyChanged();
                ApplyDefaultTime();
            }
        }

        public TimeSpan StartTime
        {
            get;
            set
            {
                if (field == value) return;
                field = value;
                OnPropertyChanged();
                EnforceMinDuration(startChanged: true);
            }
        }

        public TimeSpan EndTime
        {
            get;
            set
            {
                if (field == value) return;
                field = value;
                OnPropertyChanged();
                EnforceMinDuration(startChanged: false);
            }
        }

        // ---------------------------------------------------------
        // DOMAIN PROPERTIES
        // ---------------------------------------------------------

        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        public IEnumerable<Vehicle> AvailableVehicles
        {
            get;
            private set
            {
                if (field == value) return;
                field = value;
                OnPropertyChanged();
                RegisterBookingCommand.RaiseCanExecuteChanged(); 
            }
        }

        // ---------------------------------------------------------
        // PRIVATE METHODS (Commands & Execution)
        // ---------------------------------------------------------

        private bool CanPlaceBooking(object? param)
        {
            if (param is not VehicleTypes requestedType)            return false;
            if (Start == default || End == default || Start >= End) return false;
            if (AvailableVehicles == null)                          return false;

            return AvailableVehicles.Any(v => v.Type == requestedType);
        }

        private async Task ExecuteBookingAsync(object? param)
        {
            if (param is not VehicleTypes requestedType) return;
            if (!CanPlaceBooking(requestedType)) return;

            try
            {
                var booking = await _bookingService.TryBookOptimalVehicleAsync(Start, End, requestedType);
                if (booking is null)
                {
                    StatusMessage = "Ingen ledige køretøjer i den valgte periode.";
                }
                else
                {
                    StatusMessage = $"{booking.Vehicle!.Type} bekræftet: {booking.Vehicle.LicensePlate}\nDato: {booking.Start:dd/MM} kl. {booking.Start:HH:mm}–{booking.End:HH:mm}";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Booking fejlede: {ex.Message}");
            }
            finally
            {
                _ = LoadAvailableVehiclesAsync();
            }
        }

        // ---------------------------------------------------------
        // PRIVATE METHODS (Helper/Logic)
        // ---------------------------------------------------------

        private void EnforceMinDuration(bool startChanged)
        {
            if (EndTime - StartTime < _minDuration)
            {
                if (startChanged)
                {
                    EndTime = GetValidEndTime(StartTime);
                }
                else
                {
                    StartTime = GetValidStartTime(EndTime);
                }
            }
            UpdateBookingPeriod();
        }

        private void ApplyDefaultTime()
        {
            if (Date == DateTime.Today)
            {
                StartTime = RoundUpToNearestTimeSlot(DateTime.Now.TimeOfDay);
            }
            else
            {
                StartTime = TimeSpan.FromHours(_defaultWorkDayStartHour);
            }

            EndTime = GetValidEndTime(StartTime);
            UpdateBookingPeriod();
        }

        private void UpdateBookingPeriod()
        {
            if (Date.HasValue)
            {
                Start = Date.Value.Date + StartTime;
                End = Date.Value.Date + EndTime;

                _ = LoadAvailableVehiclesAsync();
            }
        }

        private async Task LoadAvailableVehiclesAsync()
        {
            if (Start != default && End != default && Start < End)
            {
                AvailableVehicles = await _bookingService.GetAvailableVehicles(Start, End);
            }
        }

        private void PopulateTimeSlots()
        {
            TimeSlots.Clear();
            TimeSpan maxTime = TimeSpan.FromHours(24);
            for (TimeSpan i = TimeSpan.Zero; i < maxTime; i = i.Add(TimeSpan.FromMinutes(TimeSlotIntervalMinutes)))
            {
                TimeSlots.Add(i);
            }
        }

        private TimeSpan GetValidEndTime(TimeSpan start)
        {
            var proposed = start.Add(_defaultDuration);
            return proposed > _endOfDay ? _endOfDay : proposed;
        }

        private TimeSpan GetValidStartTime(TimeSpan end)
        {
            var proposed = end.Subtract(_defaultDuration);
            return proposed < TimeSpan.Zero ? TimeSpan.Zero : proposed;
        }
    }
}