using App.Application;
using App.Data;
using App.Data.Repositories;
using App.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace WPF
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            using (var setupContext = new Context())
            {
                setupContext.Database.Migrate();
                if (!setupContext.Vehicles.Any() && !setupContext.Employees.Any())
                {
                    setupContext.Vehicles.AddRange(

                       new Vehicle { LicensePlate = "AB 12 345", Type = VehicleTypes.Car },
                       new Vehicle { LicensePlate = "CD 67 890", Type = VehicleTypes.Car },
                       new Vehicle { LicensePlate = "EF 34 567", Type = VehicleTypes.Car },
                       new Vehicle { LicensePlate = "GH 89 012", Type = VehicleTypes.Car },
                       new Vehicle { LicensePlate = "CYKEL 01", Type = VehicleTypes.Bike },
                       new Vehicle { LicensePlate = "CYKEL 02", Type = VehicleTypes.Bike },
                       new Vehicle { LicensePlate = "CYKEL 03", Type = VehicleTypes.Bike }
                       );

                    var employee = new Employee { Initials = "JDO" };
                    setupContext.Employees.Add(employee);

                    setupContext.SaveChanges();
                };
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
