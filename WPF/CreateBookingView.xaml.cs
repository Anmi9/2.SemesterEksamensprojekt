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

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
                // _viewModel.Type = App.Models.VehicleType.Car;
        }
    }
}
