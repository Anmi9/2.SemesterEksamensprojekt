using App.Application;
using App.Models;
using System.Collections.ObjectModel;
using WPF.Commands;

namespace App.ViewModel
{
    public class CreateBookingViewModel : ViewModelBase
    {
        private readonly BookingService _bookingService;

        // Konstanter til konfiguration
        private const int _timeSlotIntervalMinutes = 15;
        private const int _maxBookingDaysInFuture = 28;
        private const int _defaultWorkDayStartHour = 8;
        private static readonly TimeSpan _defaultDuration = TimeSpan.FromHours(1);
        private static readonly TimeSpan _minDuration = TimeSpan.FromMinutes(_timeSlotIntervalMinutes);
        private static readonly TimeSpan _endOfDay = TimeSpan.FromHours(24).Subtract(TimeSpan.FromMinutes(_timeSlotIntervalMinutes));

        public ObservableCollection<TimeSpan> TimeSlots { get; } = new();
        public DateTime MinDate { get; } = DateTime.Today;
        public DateTime MaxDate { get; } = DateTime.Today.AddDays(_maxBookingDaysInFuture);

        static CreateBookingViewModel() // Objektet oprettes kun hvis kontrakten overholdes
        {
            if (_defaultDuration <= _minDuration) // Kontrakten
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
            Date = DateTime.Today;

            RegisterBookingCommand = new RelayCommand(
                execute: async param => await ExecuteBookingAsync(param),
                canExecute: param => CanPlaceBooking(param)
            );
        }

        // ---------------------------------------------------------
        // UI STATE PROPERTIES
        // ---------------------------------------------------------

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

        public RelayCommand RegisterBookingCommand { get; } 

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
                RegisterBookingCommand.RaiseCanExecuteChanged(); // Sender event til WPF - som kalder CanPlaceBooking() i respons (via DelayCommand.cs)
            }
        }

        // ---------------------------------------------------------
        // Private Functions
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

        private void PopulateTimeSlots()
        {
            TimeSlots.Clear();
            TimeSpan maxTime = TimeSpan.FromHours(24);
            for (TimeSpan i = TimeSpan.Zero; i < maxTime; i = i.Add(TimeSpan.FromMinutes(_timeSlotIntervalMinutes)))
            {
                TimeSlots.Add(i);
            }
        }

        private TimeSpan RoundUpToNearestTimeSlot(TimeSpan time)
        {
            double totalMinutes = time.TotalMinutes;
            double intervalsPassed = totalMinutes / _timeSlotIntervalMinutes;
            double roundedIntervals = Math.Ceiling(intervalsPassed);
            double totalRoundedMinutes = roundedIntervals * _timeSlotIntervalMinutes;

            return TimeSpan.FromMinutes(totalRoundedMinutes);
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

        public async Task LoadAvailableVehiclesAsync()
        {
            if (Start != default && End != default && Start < End)
            {
                AvailableVehicles = await _bookingService.GetAvailableVehicles(Start, End);
            }
        }

        public async Task Book()
        {
            await _bookingService.TryBookOptimalVehicleAsync(Start, End, Type);
        }
        private bool CanPlaceBooking(object? param)
        {
            if (param is not VehicleTypes requestedType)            return false;   // Guard: gemmer param i variabel og tjekker typen
            if (Start == default || End == default || Start >= End) return false;   // Guard: tjekker at properties er udfyldt og i en gyldig tilstand 
            if (AvailableVehicles == null)                          return false;   // Guard: er samlingen blevet instantieret af baggrundstråden

            return AvailableVehicles.Any(v => v.Type == requestedType);
        }
        private async Task ExecuteBookingAsync(object? param)
        {
            if (param is not VehicleTypes requestedType) return;
            if (!CanPlaceBooking(requestedType)) return;

            try
            {
                await _bookingService.TryBookOptimalVehicleAsync(Start, End, requestedType);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Booking fejlede: {ex.Message}");
            }
        }

    }
}