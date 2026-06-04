using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace App.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        protected const int TimeSlotIntervalMinutes = 15;
        protected static readonly TimeSpan QuickBookingDuration = TimeSpan.FromHours(2);

        public string StatusMessage
        {
            get;
            set { field = value; OnPropertyChanged(); }
        } = string.Empty;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected TimeSpan RoundUpToNearestTimeSlot(TimeSpan time)
        {
            double totalMinutes = time.TotalMinutes;
            double intervalsPassed = totalMinutes / TimeSlotIntervalMinutes;
            double roundedIntervals = Math.Ceiling(intervalsPassed);
            return TimeSpan.FromMinutes(roundedIntervals * TimeSlotIntervalMinutes);
        }

        protected (DateTime Start, DateTime End) GetQuickBookingPeriod()
        {
            DateTime now = DateTime.Now;
            int minutes = (now.Minute / TimeSlotIntervalMinutes) * TimeSlotIntervalMinutes; // runder ned til nuværende kvarter
            DateTime start = new DateTime(now.Year, now.Month, now.Day, now.Hour, minutes, 0);
            DateTime end = start.Add(QuickBookingDuration);
            return (start, end);

        }
    }
}