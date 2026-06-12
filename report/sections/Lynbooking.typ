Lynbooking er vores mest direkte svar på problemformuleringen: at lynhurtigt eksponere transportmidler for brugeren. Funktionen lader medarbejderen booke et køretøj til lige nu med ét klik fra hovedvinduet uden at skulle vælge dato, tid eller et konkret køretøj. Det er ad hoc arbejdsgang fra persona-arbejdet kogt ned til den mest simple form.

Da denne funktion var den sidste der blev implementeret er temaet for den genbrug. Alt ny kode ligger i præsentationslaget i klassen `MainWindowViewModel` mens applikationslaget (`BookingService`) genbruges urørt.

Det første der blev genbrugt var `RelayCommand`-klassen fra forrige afsnit. Hovedvinduets to knapper får hver deres Command og sammen med en `CanExecute`-metode gør det knapperne tilstands-drevne, så WPF selv deaktivere knappen når der ikke er noget ledigt at booke.

```cs
  public MainWindowViewModel(BookingService bookingservice)
  {
    _bookingService = bookingservice;
    // Commands oprettes og metoder sendes som argumenter
    BookCarCommand =
      new RelayCommand(ExecuteBookCar, CanBookCar);
    BookBikeCommand =
      new RelayCommand(ExecuteBookBike, CanBookBike);
    _ = LoadAsync(); // ignorer task-objektet og fortsæt
  }

  // true = aktiv knap, false = grå og ikke klikbar
  private bool CanBookCar(object? parameter)
  {
    return _isCarAvailable;
  }
```
Det næste genbrug er selve bookingflowet. Fordi en lynbooking af en bil og en cykel kun adskiller sig på typen, deler begge knapper een metode med typen som parameter. Den fælles metode `ExecuteBookAsync(VehicleTypes type)` kalder `TryBookOptimalVehicleAsync()` fra `BookingService` på samme måde som den planlagte bookingfunktion gør. Dermed genbruger lynbooking automatisk både algoritmen, der vælger det mest optimale køretøj og trådhåndteringen med SemaphoreSlim uden nogle ændringer i servicelaget. Ligeledes genbruges bekræftelsen som ligger i superklassen `ViewModelBase`.

```cs
// Fælles metode til begge knapper
  private async Task ExecuteBookAsync(VehicleTypes type)
  {
    var (start, end) = GetQuickBookingPeriod();

    var booking = await _bookingService
      .TryBookOptimalVehicleAsync(start, end, type);

    if (booking == null)
    {
      StatusMessage =
        "Ingen ledige køretøjer i den valgte periode.";
    }
    else
    {
      StatusMessage =
        $"{booking.Vehicle!.Type} bekræftet: " +
        $"{booking.Vehicle.LicensePlate}";
    }
    await LoadAsync(); // opdaterer tilgængelighed
  }
```

Funktionens eneste nye logik er definitionen af "lige nu". Den ligger i `GetQuickBookingPeriod()` i `ViewModelBase` og indebærer en bevidst domænebeslutning, hvor den planlagte booking runder op til næste kvarter runder lynbookingen derimod ned. Således gælder en lynbooking fra nu og samtidig fastholder vi beslutningen om, at bookinger kun skal kunne oprettes for hvert kvarter. Varigheden på bookingen er ligenu sat til 2 timer for at reducere antallet af klik og for at vi var sikre på, at en varigheden kunne indeholde den længst mulige køretur.
```cs
protected (DateTime Start, DateTime End)
   GetQuickBookingPeriod()
 {
   DateTime now = DateTime.Now;
   int minutes =
     (now.Minute / TimeSlotIntervalMinutes)
     * TimeSlotIntervalMinutes; // runder ned til nuværende kvarter
   DateTime start = new DateTime(now.Year, now.Month,
     now.Day, now.Hour, minutes, 0);
   DateTime end = start.Add(QuickBookingDuration);
   return (start, end);
 }
```
Det sidste genbrug er query-metoden `GetAvailableVehicles()`, som `LoadAsync()` bruger til at opdatere knappernes tilstand og antallet af ledige køretøjer ud fra databasen ved opstart og efter hver booking.

```cs
private async Task LoadAsync() //bruges for at kunne vise om der er ledige køretøjer i de næste 2 timer
{
    var (start, end) = GetQuickBookingPeriod();

    IEnumerable<Vehicle> carResult = await _bookingService.GetAvailableVehicles(start, end, VehicleTypes.Car); // alle køretøjer

    List<Vehicle> cars = carResult.ToList(); //laves til liste så den kan bruges Count på den

    _isCarAvailable = cars.Count > 0; //tilgængeligheden er true hvis over 0 på listen
    _availableCarCount = cars.Count; //antal ledige (vist i knappen)

    BookCarCommand.RaiseCanExecuteChanged(); //fortæller wpf at den skal kalde CanBook igen, for at se om den har ændret sig - fortæller om knappen er grå eller grøn
    OnPropertyChanged(nameof(BookCarLabel)); //ændring af property
}
```
Genbruget har dog en pris som denne metode viser. `LoadAsync()` skal kun bruge antallet af ledige køretøjer, men fordi query-metoden genbruges og `GetAvaiableVehicles` er formet efter bookingflowets behov henter vi hele `Vehicle`-objekter fra databasen for at lægge dem ind på en liste med den eneste hensigt at tælle dem. Et alternativ ville være en metode i repositorylaget, og lade databasen tælle. SQLite understøtter ikke stored procedures, men EF Core har en indbygget `CountAsync()`-metode, som omsættes til en  `SELECT COUNT`-query, så kun ét tal returneres fra databasen. På grund af de få køretøjer samt den lokale database accepterede vi dog dette trade-off for at undgå yderligere refaktorering.

Et simpelt overblik flowet for en lynbooking ser således ud:

#image("../assets/Sekvensdiagram - lynbooking (blackbox).png")

Samlet set bekræftede implementeringen af lynbooking, at iterationens oprydningsarbejde betalte sig. Vi kunne indfri et Must-krav næsten udelukkende ved brug af eksisterende byggeklodser.
