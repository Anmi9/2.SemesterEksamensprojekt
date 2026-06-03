using App.Application;
using App.ViewModel;
using System.Windows;

namespace WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow(BookingService bookingservice)
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(bookingservice);

            NotificationBox.DataContext = DataContext;
        }
    }
}