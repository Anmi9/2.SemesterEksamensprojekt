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
        public CreateBookingView(BookingService service)
        {
            InitializeComponent();
            _viewModel = new CreateBookingViewModel(service);
            this.DataContext = _viewModel;
        }
        private void BookCarClick(object sender, RoutedEventArgs e)
        {
            _viewModel.Type = VehicleTypes.Car;
            _viewModel.Book();
        }
        private void BookBikeClick(object sender, RoutedEventArgs e)
        {
            _viewModel.Type = VehicleTypes.Bike;
            _viewModel.Book();
        }
    }
}
