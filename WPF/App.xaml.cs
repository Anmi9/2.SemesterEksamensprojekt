using App.Application;
using App.Data;
using App.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data;
using System.Windows;

namespace WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //Composition root, Constructor injection, Dependency injection, Dependency Inversion Principle, SOLID
        protected override void OnStartup(StartupEventArgs e)
        {
            using (var setupContext = new Context())
            {
                setupContext.Database.Migrate();
            }

            base.OnStartup(e);
            var context = new Context();
            var bookingRepo = new BookingRepository(context);
            var vehicleRepo = new VehicleRepository(context);
            var bookingService = new BookingService(bookingRepo, vehicleRepo);
            var mainWindow = new MainWindow(bookingService);
            mainWindow.Show();
        }
    }
}
