using App.ViewModel;
using System.Windows;
using System.Windows.Controls;

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
