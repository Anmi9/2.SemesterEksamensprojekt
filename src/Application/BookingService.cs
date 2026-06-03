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
        public async Task<Booking> CreateBookingAsync(DateTime start, DateTime end, int VehicleId) //Metoden er gjort asynkron for at kunne håndtere databaseoperationer.
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

        //Metode, der forsøger at booke det mest optimale køretøj baseret på den nye booking's start- og sluttidspunkt. Den bruger en SemaphoreSlim for at sikre, at kun én booking kan oprettes ad gangen, hvilket hjælper med at forhindre race conditions.
        public async Task<Booking?> TryBookOptimalVehicleAsync(DateTime Start, DateTime End, VehicleTypes Type)
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
        public Vehicle FindBestOptimalVehicle(DateTime Start, DateTime End, List<Vehicle> availableVehicleTypes, List<Booking> allActiveBookings)
        {
            if (availableVehicleTypes == null || availableVehicleTypes.Count == 0)

            {
                throw new InvalidOperationException("Kan ikke finde optimal bil, da der ikke er nogen ledige biler på listen.");
            }

            DateTime newBookingStart = Start;
            DateTime newBookingEnd = End;
            TimeSpan smallestGap = TimeSpan.MaxValue; // Vi starter med at sætte smallestGap til den største mulige værdi, så enhver faktisk gap vil være mindre.

            Vehicle optimalVehicle = availableVehicleTypes[0];

            List<Booking> relevantBookings = new List<Booking>(); // Vi opretter en liste, der kun indeholder bookinger, der kommer efter i går

            foreach (Booking b in allActiveBookings) // Itererer gennem alle aktive bookinger
            {
                if (b.End > DateTime.Today.AddDays(-1)) // Sørger for at kun bookinger, der slutter senere end i går, inkluderes i relevantebookinger
                {
                    relevantBookings.Add(b);
                }

            }

            foreach (Vehicle vehicle in availableVehicleTypes) //Iterer gennem alle køretøjerne i AvailableVehicles-listen.
            {



                //List<Booking> vehicleBookings = allActiveBookings                                            
                //    .Where(b => b.VehicleId == vehicle.VehicleId) // Finder alle bookinger for det aktuelle køretøj ved at filtrere allActiveBookings-listen baseret på VehicleId
                //    .ToList(); 

                //var previousBooking = vehicleBookings // Leder efter tidligere booking, der slutter før den nye booking starter
                //    .Where(b => b.End <= newBookingStart) // Behold bookinger, hvis sluttidspunkt er mindre end eller lig med den nye booking's starttidspunkt.
                //    .OrderByDescending(b => b.End) //Vi sorterer baglæns for at få den seneste booking, der slutter før den nye booking starter.
                //    .FirstOrDefault(); 

                //var nextBooking = vehicleBookings
                //    .Where(b => b.Start >= newBookingEnd)
                //    .OrderBy(b => b.Start) // Vi sorterer kronologisk for at få den tidligeste booking efter den nye booking slutter
                //    .FirstOrDefault();



                Booking previousBooking = null!; // Null-operatoren ! bruges for at sige, at vi bevidst tillader de to booking-variabler at være null
                Booking nextBooking = null!;



                foreach (Booking b in relevantBookings) // Itererer gennem relevante bookinger
                {

                    if (b.VehicleId == vehicle.VehicleId) // Vi tjekker kun bookinger for det aktuelle køretøj
                    {

                        if (b.End <= newBookingStart && (previousBooking == null || b.End > previousBooking.End)) // Tjekker om aktuelle booking slutter før den nye starter. Det bliver også tjekket, om den aktuelle booking er senere end den tidligere booking, vi har fundet (hvis der er en)
                        {
                            previousBooking = b;
                        }


                        if (newBookingEnd <= b.Start && (nextBooking == null || b.Start < nextBooking.Start)) // Tjekker om den nye booking slutter før den aktuelle booking starter. Det bliver også tjekket, om den aktuelle booking er tidligere end den næste booking, vi har fundet (hvis der er en)
                        {
                            nextBooking = b;
                        }

                    }
                }


                //DateTime gapStart = previousBooking != null ? previousBooking.End : DateTime.MinValue; //Ternary operator (:) i stedet for en if/else-blok
                //DateTime gapEnd = nextBooking != null ? nextBooking.Start : DateTime.MaxValue;

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

                TimeSpan gap = gapEnd - gapStart; // De to DateTime-variabler bliver automatisk konverteret til TimeSpan, når vi trækker den fra hinanden.

                if (gap < smallestGap) // Hvis det beregnede gap er mindre end det mindste gap, vi har fundet indtil nu, opdaterer vi smallestGap og optimalVehicle.
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


