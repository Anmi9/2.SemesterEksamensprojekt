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

        // Guard mod forretningslogik-loops
        private bool _isSynchronizing;

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
        public DateTime MinSelectableDate { get; } = DateTime.Today;
        public DateTime MaxSelectableDate { get; } = DateTime.Today.AddDays(28);

        public CreateBookingViewModel(BookingService service)
        {
            _bookingService = service;
            PopulateTimeSlots();

            var now = DateTime.Now;
            int remainder = now.Minute % 15;
            int minutesToAdd = remainder == 0 ? 0 : 15 - remainder;
            TimeSpan defaultStart = new TimeSpan(now.Hour, now.Minute, 0).Add(TimeSpan.FromMinutes(minutesToAdd));
            TimeSpan defaultEnd = defaultStart.Add(_defaultDuration);

            _isSynchronizing = true;
            try
            {
                SelectedDate = DateTime.Today;
                SelectedStartTime = defaultStart;
                SelectedEndTime = defaultEnd;
                SyncCoreBookingTimes();
            }
            finally
            {
                _isSynchronizing = false;
            }
        }

        // ---------------------------------------------------------
        // INDEPENDENT UI STATE PROPERTIES (Klassiske Backing Felter)
        // ---------------------------------------------------------

        public DateTime? SelectedDate
        {
            get;
            set
            {
                if (field == value) return;
                field = value;
                OnPropertyChanged();
                ApplyDateContextRules();
            }
        }

        public TimeSpan SelectedStartTime
        {
            get;
            set
            {
                if (field == value) return;
                field = value;
                OnPropertyChanged();
                EnforceContract(startChanged: true);
            }
        }

        public TimeSpan SelectedEndTime
        {
            get;
            set
            {
                if (field == value) return;
                field = value;
                OnPropertyChanged();
                EnforceContract(startChanged: false);
            }
        }

        // ---------------------------------------------------------
        // CORE DOMAIN FIELDS
        // ---------------------------------------------------------

        // Standard Auto-Properties er 100% sikre for WPF
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        public IEnumerable<Vehicle> AvailableVehicles { get; internal set; }
        public VehicleTypes Type { get; set; }

        // ---------------------------------------------------------
        // PRIVATE MECHANISMS
        // ---------------------------------------------------------

        private void EnforceContract(bool startChanged)
        {
            if (_isSynchronizing) return;

            try
            {
                _isSynchronizing = true;

                if (SelectedEndTime - SelectedStartTime < _minDuration)
                {
                    if (startChanged)
                    {
                        SelectedEndTime = SelectedStartTime.Add(_defaultDuration);
                    }
                    else
                    {
                        SelectedStartTime = SelectedEndTime.Subtract(_defaultDuration);
                    }
                }

                SyncCoreBookingTimes();
            }
            finally
            {
                _isSynchronizing = false;
            }
        }

        private void ApplyDateContextRules()
        {
            if (_isSynchronizing) return;

            try
            {
                _isSynchronizing = true;

                if (SelectedDate == DateTime.Today)
                {
                    var now = DateTime.Now;
                    int remainder = now.Minute % 15;
                    int minutesToAdd = remainder == 0 ? 0 : 15 - remainder;
                    SelectedStartTime = new TimeSpan(now.Hour, now.Minute, 0).Add(TimeSpan.FromMinutes(minutesToAdd));
                }
                else
                {
                    SelectedStartTime = TimeSpan.FromHours(8);
                }

                SelectedEndTime = SelectedStartTime.Add(_defaultDuration);
                SyncCoreBookingTimes();
            }
            finally
            {
                _isSynchronizing = false;
            }
        }

        private void SyncCoreBookingTimes()
        {
            if (SelectedDate.HasValue)
            {
                Start = SelectedDate.Value.Date + SelectedStartTime;
                End = SelectedDate.Value.Date + SelectedEndTime;
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