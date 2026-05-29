using App.Application;
using App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace App.ViewModel
{
    public class CreateBookingViewModel : ViewModelBase
    {
        private readonly BookingService _bookingService;

        // Konstanter til configuration
        private const int _timeSlotIntervalMinutes = 15;
        private const int _maxBookingDaysInFuture = 28;
        private const int _defaultWorkDayStartHour = 8;

        private static readonly TimeSpan _defaultDuration = TimeSpan.FromHours(1);
        private static readonly TimeSpan _minDuration = TimeSpan.FromMinutes(_timeSlotIntervalMinutes);

        static CreateBookingViewModel() // Objektet oprettes kun hvis kontrakten overholdes
        {
            if (_defaultDuration <= _minDuration) // Kontrakten
            {
                throw new TypeInitializationException(
                    nameof(CreateBookingViewModel),
                    new ArgumentException($"Kritisk invariant brudt: {nameof(_defaultDuration)} skal være større end {nameof(_minDuration)}."));
            }
        }

        public ObservableCollection<TimeSpan> TimeSlots { get; } = new();
        public DateTime MinDate { get; } = DateTime.Today;
        public DateTime MaxDate { get; } = DateTime.Today.AddDays(_maxBookingDaysInFuture);

        public CreateBookingViewModel(BookingService service)
        {
            _bookingService = service;
            PopulateTimeSlots();

            var now = DateTime.Now;
            var defaultStart = TimeSpan.FromMinutes(Math.Ceiling(now.TimeOfDay.TotalMinutes / _timeSlotIntervalMinutes) * _timeSlotIntervalMinutes);
            var defaultEnd = defaultStart.Add(_defaultDuration);

            Date = DateTime.Today;
            StartTime = defaultStart;
            EndTime = defaultEnd;
            SyncCoreBookingTimes();
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

        // ---------------------------------------------------------
        // DOMAIN FIELDS
        // ---------------------------------------------------------

        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        public IEnumerable<Vehicle> AvailableVehicles { get; internal set; }
        public VehicleTypes Type { get; set; }

        // ---------------------------------------------------------
        // PRIVATE MECHANISMS
        // ---------------------------------------------------------

        private void EnforceMinDuration(bool startChanged)
        {
            if (EndTime - StartTime < _minDuration)
            {
                if (startChanged)
                {
                    EndTime = StartTime.Add(_defaultDuration);
                }
                else
                {
                    StartTime = EndTime.Subtract(_defaultDuration);
                }
            }

            SyncCoreBookingTimes();
        }

        private void ApplyDefaultTime()
        {
            if (Date == DateTime.Today)
            {
                var now = DateTime.Now;
                StartTime = TimeSpan.FromMinutes(Math.Ceiling(now.TimeOfDay.TotalMinutes / _timeSlotIntervalMinutes) * _timeSlotIntervalMinutes);
            }
            else
            {
                StartTime = TimeSpan.FromHours(_defaultWorkDayStartHour);
            }

            EndTime = StartTime.Add(_defaultDuration);
            SyncCoreBookingTimes();
        }

        private void SyncCoreBookingTimes()
        {
            if (Date.HasValue)
            {
                Start = Date.Value.Date + StartTime;
                End = Date.Value.Date + EndTime;
            }
        }

        private void PopulateTimeSlots()
        {
            TimeSlots.Clear();
            TimeSpan endOfDay = TimeSpan.FromDays(1);
            for (TimeSpan i = TimeSpan.Zero; i < endOfDay; i = i.Add(TimeSpan.FromMinutes(_timeSlotIntervalMinutes)))
            {
                TimeSlots.Add(i);
            }
        }

        public void Book()
        {
            _bookingService.CreateBookingAsync(Start, End, 1);
        }
    }
}