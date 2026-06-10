Under elaboration 3, havde vi fokuseret på at skrive et skelet der kunne kompilere hurtigtst muligt, selvom vi godt vidste at vi manglede at forholde os kritisk til vores arkitektur. Den priotering efterlod et rodet afhængigheds forhold mellem vores lag. For at skabe orden i det, orienterede vi os efter _kernel-shell_ tankegangen som følger Robert C. Martins' _Dependency Rule_:

#quote(block: true, attribution: [@martin2012])[
  [...] source code dependencies can only point inwards. Nothing in an inner circle can know anything at all about something in an outer circle. In particular, the
  name of something declared in an outer circle must not be mentioned by the code
  in the an inner circle.
]

Fordi arkitekturen i vores program var lagdelt og ikke fulgte Martins _Clean Architecture_, valgte vi at tilpasse principperne bag kernel-shell ideen, ved at definere vores applikations- (services) og domæne lag som kernen og WPF laget som shell. Efterfølgende fjernede vi afhængigheder der gik fra vores services ud til WPF klasserne. Eksempelvis `TryBookOptimalVehicleAsync()` i `BookingService` klassen fik ændret sin parameter fra at modtage et ViewModel objekt til primitive data typer. Som vi citerer #cite(<martin2012>, form: "prose") for, så sikrede vi at et (klasse)navn deklareret i shell ikke bliver brugt i kernel.

```cs
// Før
public async Task<bool> TryBookOptimalVehicleAsync(
  CreateBookingViewModel viewModel)

// Efter
public async Task<Booking?> TryBookOptimalVehicleAsync(
  DateTime Start,
  DateTime End,
  VehicleTypes Type)
```

En anden stor arkitektonisk ændring kom fordi vi lærte at _Model View View-Model (MVVM)_ arkitekturen, som vi brugte i forbindelse med WPF, var implementeret forkert. Vi havde været forvirret over hvilken del af mønsteret der ligger i hhv. presentations og applikations laget. Vi havde fejlagtigt placeret alt interaktions logik i view `CreateBookingView.xaml.cs`. Derfor gik vi igang med at migrerer alt dette over til view-model `CreateBookingViewModel.cs`.
