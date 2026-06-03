using App.ViewModel;
using System.Windows;

namespace WPF
{
    public partial class CreateBookingView : Window
    {
        public CreateBookingView(CreateBookingViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
