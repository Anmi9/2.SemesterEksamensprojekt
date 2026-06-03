using App.Application;
using App.ViewModel;
using System.Windows;

namespace WPF
{
    public partial class MainWindow : Window
    {
        private readonly BookingService _bookingService;
        public MainWindow(BookingService bookingservice)
        {
            InitializeComponent();
            DataContext = new MainViewModel(bookingservice);
        }
    }
}