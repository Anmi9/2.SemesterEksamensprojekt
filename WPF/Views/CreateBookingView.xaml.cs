using App.ViewModel;
using System.Windows;
using App.Application;

namespace WPF
{
    public partial class CreateBookingView : Window
    {
        public CreateBookingView(CreateBookingViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
