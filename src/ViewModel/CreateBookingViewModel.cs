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
        private static readonly TimeSpan _defaultDuration = TimeSpan.FromHours(1);
        private static readonly TimeSpan _minDuration = TimeSpan.FromMinutes(15);

        static CreateBookingViewModel()
        {
            if (_defaultDuration <= _minDuration)
            {
                throw new TypeInitializationException(
                    nameof(CreateBookingViewModel),
                    new ArgumentException($"Kritisk invariant brudt: {_defaultDuration} skal være større end {_minDuration}."));
            }
        }

        public ObservableCollection<TimeSpan> TimeSlots { get; } = new();
        public DateTime MinDate { get; } = DateTime.Today;
        public DateTime MaxDate { get; } = DateTime.Today.AddDays(28);

        public CreateBookingViewModel(BookingService service)
        {
            _bookingService = service;
            PopulateTimeSlots();

            var now = DateTime.Now;
            var defaultStart = TimeSpan.FromMinutes(Math.Ceiling(now.TimeOfDay.TotalMinutes / 15.0) * 15.0);
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
                StartTime = TimeSpan.FromMinutes(Math.Ceiling(now.TimeOfDay.TotalMinutes / 15.0) * 15.0);
            }
            else
            {
                StartTime = TimeSpan.FromHours(8);
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
            for (TimeSpan i = TimeSpan.Zero; i < endOfDay; i = i.Add(TimeSpan.FromMinutes(15)))
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