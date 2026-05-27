using App.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using App.Models;
using App.Application;

namespace WPF
{
    /// <summary>
    /// Interaction logic for CreateBookingView.xaml
    /// </summary>
    public partial class CreateBookingView : Window
    {
        private readonly CreateBookingViewModel _viewModel;
        private readonly TimeSpan _defaultDuration = TimeSpan.FromHours(1); 

        public CreateBookingView(BookingService bookingService)
        {
            InitializeComponent();
            _viewModel = new CreateBookingViewModel(bookingService);

            // Begræns DatePicker til at vise i dag og op til 4 uger (28 dage) frem
            BookingDatePicker.DisplayDateStart = DateTime.Today;
            BookingDatePicker.DisplayDate = DateTime.Today;
            BookingDatePicker.DisplayDateEnd = DateTime.Today.AddDays(28);

            // Sæt default valg til at være i dag
            BookingDatePicker.SelectedDate = DateTime.Today;

            InitializeTimeComboBoxes();
        }

        private void InitializeTimeComboBoxes()
        {
            var times = new List<string>();
            var startTime = new TimeSpan(0, 0, 0);
            var endTime = new TimeSpan(23, 45, 0);
            var interval = new TimeSpan(0, 15, 0);

            var currentTime = startTime;
            while (currentTime <= endTime)
            {
                times.Add(currentTime.ToString(@"hh\:mm"));
                currentTime = currentTime.Add(interval);
            }

            StartTimeComboBox.ItemsSource = times;
            EndTimeComboBox.ItemsSource = times;

            UpdateTimeSelection();
        }

        private void BookingDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTimeSelection();
        }

        private void UpdateTimeSelection()
        {
            if (BookingDatePicker.SelectedDate == DateTime.Today)
            {
                // Find nærmeste kvarter fremad (f.eks. hvis kl. er 14:10, vælg 14:15)
                var now = DateTime.Now;
                var minutes = now.Minute;
                var adjustment = 15 - (minutes % 15);
                var roundedTime = now.AddMinutes(adjustment);

                TimeSpan defaultTimeStart = roundedTime.TimeOfDay;
                TimeSpan defaultTimeEnd = defaultTimeStart.Add(_defaultDuration);

                string startString = defaultTimeStart.ToString(@"hh\:mm");
                string endString = defaultTimeEnd.ToString(@"hh\:mm");

                // Prøv at finde tidspunktet i comboboxen
                var index = StartTimeComboBox.Items.IndexOf(startString);
                StartTimeComboBox.SelectedIndex = index;

                index = EndTimeComboBox.Items.IndexOf(endString);
                EndTimeComboBox.SelectedIndex = index;

                _viewModel.Start = roundedTime;
                _viewModel.End = BookingDatePicker.SelectedDate?.Add(defaultTimeEnd);
            }
            else
            {
                // Hvis ikke det er i dag, sæt til 08:00
                TimeSpan defaultTimeStart = TimeSpan.FromHours(8); // 08:00
                TimeSpan defaultTimeEnd = defaultTimeStart.Add(_defaultDuration);

                string startString = defaultTimeStart.ToString(@"hh\:mm");
                string endString = defaultTimeEnd.ToString(@"hh\:mm");

                var index = StartTimeComboBox.Items.IndexOf(startString);
                StartTimeComboBox.SelectedIndex = index;

                index = EndTimeComboBox.Items.IndexOf(endString);
                EndTimeComboBox.SelectedIndex = index;

                _viewModel.Start = BookingDatePicker.SelectedDate?.Add(defaultTimeStart); 
                _viewModel.End = BookingDatePicker.SelectedDate?.Add(defaultTimeEnd);
            }
        }

        // Time picker logik: DisplayTime er now hvis DateStart er Today(), ellers start på 07:00
        private void BookCarClick(object sender, RoutedEventArgs e)
        {
            _viewModel.Type = VehicleType.Car;
            _viewModel.Book();
        }

        private void BookBikeClick(object sender, RoutedEventArgs e)
        {
            _viewModel.Type = VehicleType.Bike;
            _viewModel.Book();
        }

        private void StartTimeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StartTimeComboBox.SelectedItem != null && BookingDatePicker.SelectedDate.HasValue)
            {
                string? selectedTimeStart = StartTimeComboBox.SelectedItem.ToString();
                string? selectedTimeEnd = EndTimeComboBox.SelectedItem?.ToString();

                if (TimeSpan.TryParse(selectedTimeStart, out TimeSpan timeStart))
                {
                    _viewModel.Start = BookingDatePicker.SelectedDate.Value.Add(timeStart);

                    if (!TimeSpan.TryParse(selectedTimeEnd, out TimeSpan timeEnd))
                    {
                        timeEnd = TimeSpan.Zero;
                    }

                    if (timeEnd - timeStart < TimeSpan.FromMinutes(15))
                    {
                        TimeSpan defaultTimeEnd = timeStart.Add(_defaultDuration);
                        string endString = defaultTimeEnd.ToString(@"hh\:mm");

                        var index = EndTimeComboBox.Items.IndexOf(endString);
                        if (index != -1)
                        {
                            EndTimeComboBox.SelectedIndex = index;
                        }
                    }
                }
            }
        }
        private void EndTimeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) 
        {
            if (EndTimeComboBox.SelectedItem != null && BookingDatePicker.SelectedDate.HasValue)
            {
                string? selectedTimeEnd = EndTimeComboBox.SelectedItem.ToString();
                string? selectedTimeStart = StartTimeComboBox.SelectedItem?.ToString();

                if (TimeSpan.TryParse(selectedTimeEnd, out TimeSpan timeEnd))
                {
                    _viewModel.End = BookingDatePicker.SelectedDate.Value.Add(timeEnd);

                    if (!TimeSpan.TryParse(selectedTimeStart, out TimeSpan timeStart))
                    {
                        // Sættes til MaxValue for at sikre, at betingelsen fejler hvis der ikke er nogen starttid valgt
                        timeStart = TimeSpan.MaxValue; 
                    }

                    if (timeEnd - timeStart < TimeSpan.FromMinutes(15))
                    {
                        TimeSpan defaultTimeStart = timeEnd.Subtract(_defaultDuration);
                        string startString = defaultTimeStart.ToString(@"hh\:mm");

                        var index = StartTimeComboBox.Items.IndexOf(startString);
                        if (index != -1)
                        {
                            StartTimeComboBox.SelectedIndex = index;
                        }
                    }
                }
            }
        }
    }
}
