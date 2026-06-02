using App.Application;
using App.ViewModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BookingService _bookingService;
        public MainWindow(BookingService bookingservice)
        {
            InitializeComponent();
            _bookingService = bookingservice;

            DataContext = new MainViewModel(bookingservice); //datacontext sættes så Bindings ved hvor de skal hente data
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var createBookingView = new CreateBookingView(new CreateBookingViewModel(_bookingService));
            createBookingView.Show();
        }
    }
}