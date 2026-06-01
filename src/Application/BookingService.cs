using App.Data.Repositories;
using App.Models;

namespace App.Application
{
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
        public async Task CreateBookingAsync(int VehicleId) //Metoden er gjort asynkron for at kunne håndtere databaseoperationer.
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
        public async Task CreateBookingAsync(DateTime start, DateTime end, int VehicleId) //Metoden er gjort asynkron for at kunne håndtere databaseoperationer.
        {
            Booking booking = new Booking
            {
                Start = start,
                End = end,
                VehicleId = VehicleId,
                EmployeeId = 1
            };

            await _bookingRepo.DBCreateAsync(booking); 
        }

        //Metode, der forsøger at booke det mest optimale køretøj baseret på den nye booking's start- og sluttidspunkt. Den bruger en SemaphoreSlim for at sikre, at kun én booking kan oprettes ad gangen, hvilket hjælper med at forhindre race conditions.
        public async Task<bool> TryBookOptimalVehicleAsync(DateTime Start, DateTime End, VehicleTypes Type)
        {
            List<Booking> allActiveBookings = await _bookingRepo.DBGetAllBookingsAsync(); // Henter alle aktive bookinger fra databasen
            List<Vehicle> availableVehicleTypes = (await _vehicleRepo.GetAvailableVehiclesAsync(Start, End, Type)).ToList(); // Henter alle tilgængelige køretøjer for det givne tidsrum.>

            Vehicle optimalVehicle = FindBestOptimalVehicle(Start, End, availableVehicleTypes, allActiveBookings); // Algoritmemetoden finder optimalt køretøj og gemmer det i optimalVehicle-variablen.

            await _bookingSemaphore.WaitAsync(); // SemaphoreSlim sikrer at kun en booking oprettes af gangen. 

            try
            {
                bool stillAvailable = await _bookingRepo.DBIsVehicleAvailableAtTimeAsync(optimalVehicle.VehicleId, Start, End);
                if (stillAvailable)
                {
                    await CreateBookingAsync(Start, End, optimalVehicle.VehicleId);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                _bookingSemaphore.Release();
            }


        }


        //Algoritmemetode, der finder det mest optimale køretøj baseret på eksisterende bookinger og den nye booking's start- og sluttidspunkt.
        public Vehicle FindBestOptimalVehicle(DateTime Start, DateTime End, List<Vehicle> availableVehicleTypes, List<Booking> allActiveBookings)
            {
                DateTime newBookingStart = Start;  
                DateTime newBookingEnd = End;

                Vehicle optimalVehicle = null;
                TimeSpan smallestGap = TimeSpan.MaxValue;    //Vi starte med at sætte den mindste gap til max value, så vi kan sammenligne med de faktiske gaps mellem bookingerne.

            foreach (Vehicle vehicle in availableVehicleTypes) //Iterer gennem alle køretøjerne i AvailableVehicles-listen.
                {
                    List<Booking> vehicleBookings = allActiveBookings                                            
                        .Where(b => b.VehicleId == vehicle.VehicleId) // Behold bookinger, hvis VehicleId matcher det aktuelle køretøjs VehicleId.
                        .ToList(); 

                    var previousBooking = vehicleBookings // Leder efter tidligere booking, der slutter før den nye booking starter
                        .Where(b => b.End <= newBookingStart) // Behold bookinger, hvis sluttidspunkt er mindre end eller lig med den nye booking's starttidspunkt.
                        .OrderByDescending(b => b.End) //Vi sorterer baglæns for at få den seneste booking, der slutter før den nye booking starter.
                        .FirstOrDefault(); 

                    var nextBooking = vehicleBookings
                        .Where(b => b.Start >= newBookingEnd)
                        .OrderBy(b => b.Start) // Vi sorterer kronologisk for at få den tidligeste booking efter den nye booking slutter
                        .FirstOrDefault();

                DateTime gapStart = previousBooking != null ? previousBooking.End : DateTime.MinValue; //Ternary operator (:) i stedet for en if/else-blok
                DateTime gapEnd = nextBooking != null ? nextBooking.Start : DateTime.MaxValue;

                TimeSpan gap = gapEnd - gapStart;

                if (gap < smallestGap) // Hvis det beregnede gap er mindre end det mindste gap, vi har fundet indtil nu, opdaterer vi smallestGap og optimalVehicle.
                {
                    smallestGap = gap;
                    optimalVehicle = vehicle;
                }
            }

            return optimalVehicle;
        }
        public Task<IEnumerable<Vehicle>> GetAvailableVehicles(DateTime start, DateTime end, VehicleTypes? type) => _vehicleRepo.GetAvailableVehiclesAsync(start, end, type);
    }
}
    

