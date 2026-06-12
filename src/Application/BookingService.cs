using App.Data.Repositories;
using App.Models;

namespace App.Application
{
    /// <summary>
    /// Serviceklasse
    /// </summary>
    /// <author>Anna</author>
    public class BookingService
    {
        private readonly BookingRepository _bookingRepo;
        private readonly VehicleRepository _vehicleRepo;

        private static readonly SemaphoreSlim _bookingSemaphore = new SemaphoreSlim(1, 1);

        public BookingService(BookingRepository bookingRepo, VehicleRepository vehicleRepo)
        {
            _bookingRepo = bookingRepo;
            _vehicleRepo = vehicleRepo;
        }
        public async Task CreateBookingAsync(int VehicleId) 
        {
            Booking booking = new Booking
            {
                Start = DateTime.Now,
                End = DateTime.Now + TimeSpan.FromHours(2),
                VehicleId = VehicleId,
                EmployeeId = 1
            };
            await _bookingRepo.DBCreateAsync(booking);
        }
        private async Task<Booking> CreateBookingAsync(DateTime start, DateTime end, int VehicleId) 
        {
            Booking booking = new Booking
            {
                Start = start,
                End = end,
                VehicleId = VehicleId,
                EmployeeId = 1
            };

            await _bookingRepo.DBCreateAsync(booking);
            return booking;
        }

        //Metode, der forsøger at booke det mest optimale køretøj baseret på den nye booking's start- og sluttidspunkt. Den bruger en SemaphoreSlim for at sikre, at kun én booking kan oprettes ad gangen, hvilket hjælper med at forhindre raceconditions.
        public async Task<Booking?> TryBookOptimalVehicleAsync(DateTime Start, DateTime End, VehicleTypes Type)
        {
            List<Booking> allActiveBookings = await _bookingRepo.DBGetAllBookingsAsync(); 
            List<Vehicle> availableVehicleTypes = (await _vehicleRepo.GetAvailableVehiclesAsync(Start, End, Type)).ToList(); 

            Vehicle optimalVehicle = FindBestOptimalVehicle(Start, End, availableVehicleTypes, allActiveBookings); 

            await _bookingSemaphore.WaitAsync(); 

            try
            {
                bool stillAvailable = await _bookingRepo.DBIsVehicleAvailableAtTimeAsync(optimalVehicle.VehicleId, Start, End);
                if (stillAvailable)
                {
                    var booking = await CreateBookingAsync(Start, End, optimalVehicle.VehicleId);
                    booking.Vehicle = optimalVehicle;
                    return booking;
                }
                else
                {
                    return null;
                }
            }
            finally
            {
                _bookingSemaphore.Release();
            }


        }


        //Algoritmemetode, der finder det mest optimale køretøj baseret på eksisterende bookinger og den nye booking's start- og sluttidspunkt.
        private Vehicle FindBestOptimalVehicle(DateTime Start, DateTime End, List<Vehicle> availableVehicleTypes, List<Booking> allActiveBookings)
        {
            if (availableVehicleTypes == null || availableVehicleTypes.Count == 0)

            {
                throw new InvalidOperationException("Kan ikke finde optimal bil, da der ikke er nogen ledige biler på listen.");
            }

            DateTime newBookingStart = Start;
            DateTime newBookingEnd = End;
            TimeSpan smallestGap = TimeSpan.MaxValue; 

            Vehicle optimalVehicle = availableVehicleTypes[0];

            List<Booking> relevantBookings = new List<Booking>(); // Vi opretter en liste, der kun indeholder bookinger, der kommer efter i går

            foreach (Booking b in allActiveBookings) 
            {
                if (b.End > DateTime.Today.AddDays(-1)) 
                {
                    relevantBookings.Add(b);
                }

            }

            foreach (Vehicle vehicle in availableVehicleTypes) //Iterer gennem alle køretøjerne i AvailableVehicles-listen.
            {


                Booking previousBooking = null!; 
                Booking nextBooking = null!;



                foreach (Booking b in relevantBookings) 
                {

                    if (b.VehicleId == vehicle.VehicleId) 
                    {

                        if (b.End <= newBookingStart && (previousBooking == null || b.End > previousBooking.End)) 
                        {
                            previousBooking = b;
                        }


                        if (newBookingEnd <= b.Start && (nextBooking == null || b.Start < nextBooking.Start)) 
                        {
                            nextBooking = b;
                        }

                    }
                }


                DateTime gapStart;
                if (previousBooking != null)
                {
                    gapStart = previousBooking.End;
                }

                else
                {
                    gapStart = DateTime.MinValue;
                }

                DateTime gapEnd;
                if (nextBooking != null)
                {
                    gapEnd = nextBooking.Start;
                }

                else
                {
                    gapEnd = DateTime.MaxValue;
                }

                TimeSpan gap = gapEnd - gapStart; 

                if (gap < smallestGap) 
                {
                    smallestGap = gap;
                    optimalVehicle = vehicle;
                }
            }

            return optimalVehicle;
        }
        public Task<IEnumerable<Vehicle>> GetAvailableVehicles(DateTime start, DateTime end, VehicleTypes? type = null)
            => _vehicleRepo.GetAvailableVehiclesAsync(start, end, type);
    }
}


