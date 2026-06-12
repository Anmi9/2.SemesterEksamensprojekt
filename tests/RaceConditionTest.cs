using Microsoft.VisualStudio.TestTools.UnitTesting;
using App.Application;
using App.Data;
using App.Data.Repositories;
using App.Models;
using System;
using System.Threading.Tasks;

namespace AppTests;
/// <summary>
/// 
/// </summary>
/// <author>Anna</author>
[TestClass]
public class RaceConditionTest
{
    private DateTime _start;
    private DateTime _end;
    private const VehicleTypes _carType = VehicleTypes.Car;

    [TestMethod]
     public async Task TryBookOptimalVehicle_DobbeltBooking_OnlyOneBookingGetsCreated()
    {
      //Arrange:

        _start = DateTime.Now.AddDays(25); 
        _end = _start.AddHours(2);

        //Act: 

        Task<Booking> thread1 = Task.Run(SimulateClick1Async); // Første klik

        await Task.Delay(10); 

        Task<Booking> thread2 = Task.Run(SimulateClick2Async); // Andet klik

        Booking?[] results = await Task.WhenAll(thread1, thread2); 

        // Assert:

        int successfulBookings = 0;

        foreach (Booking? b in results)
        {
            if (b != null)
            {
                successfulBookings = successfulBookings + 1;
            }
        };

        Assert.AreEqual(1, successfulBookings);

     }

    private async Task<Booking> SimulateClick1Async()
    {
        using (Context context1 = new Context())
        {
            BookingRepository repo1 = new BookingRepository(context1);
            VehicleRepository vehicleRepo1 = new VehicleRepository(context1);
            BookingService service1 = new BookingService(repo1, vehicleRepo1);

            return await service1.TryBookOptimalVehicleAsync(_start, _end, _carType);
        }
    }

    private async Task<Booking> SimulateClick2Async()
    {
        using (Context context2 = new Context()) 
        {
            BookingRepository repo2 = new BookingRepository(context2);
            VehicleRepository vehicleRepo2 = new VehicleRepository(context2);
            BookingService service2 = new BookingService(repo2, vehicleRepo2);
            return await service2.TryBookOptimalVehicleAsync(_start, _end, _carType);
        }
    }
}
