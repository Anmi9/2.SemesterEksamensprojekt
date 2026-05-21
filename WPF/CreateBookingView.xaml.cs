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

namespace WPF
{
    /// <summary>
    /// Interaction logic for CreateBookingView.xaml
    /// </summary>
    public partial class CreateBookingView : Window
    {
        private readonly CreateBookingViewModel _viewModel;
        public CreateBookingView()
        {
            InitializeComponent();
            _viewModel = new CreateBookingViewModel();

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
                // Brug @"HH\:mm" (eller string format med InvariantCulture) for at tvinge kolon (:) som separator (Da-DK kultur tvinger ellers punktum)
                times.Add(new DateTime().Add(currentTime).ToString(@"HH\:mm"));
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

                string timeString = roundedTime.ToString(@"HH\:mm");

                // Prøv at finde tidspunktet i comboboxen
                var index = StartTimeComboBox.Items.IndexOf(timeString);
                StartTimeComboBox.SelectedIndex = index;
            }
            else
            {
                // Hvis ikke det er i dag, sæt til 07:00
                string defaultTime = "07:00";
                var index = StartTimeComboBox.Items.IndexOf(defaultTime);
                StartTimeComboBox.SelectedIndex = index;
            }
        }

        // Time picker logik: DisplayTime er now hvis DateStar er Today(), ellers start på 07:00
        private void BookCarClick(object sender, RoutedEventArgs e)
        {
            _viewModel.Type = VehicleType.Car;
        }

        private void BookBikeClick(object sender, RoutedEventArgs e)
        {
            _viewModel.Type = VehicleType.Bike;
        }
    }
}
