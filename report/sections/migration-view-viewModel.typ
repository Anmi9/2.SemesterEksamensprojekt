Den oprindelige interaktionslogik var event-driven og skrevet i code-behind. Vi havde brugt den standard event handler kode som oprettes når man dobbeltklikker på et UI control-element i WPF Designer. Så da vi ville flytte logikken over til vores view-model C\# klasse mødte vi en begrænsning ved WPF. Nemlig at de autogenerede event handlers kun fungerede fordi de var placeret i code-behind. Vi lærte at for at forbinde view med en view-model, skal vi bruge WPFs implementeringer af _Observer_ og _Command_ mønstrene. Konkret via _Bindings_ abstraktionen og `ICommand` interfacet.

Bindings skaber en tovejs forbindelse mellem en UI controller og en data property, hvilket sikre at tilstandsændringer propagere mellem view og view-model. Det implementeres via xaml binding syntaksen `{Binding Path=PropertyName}` i view og ved at realisere `INotifyPropertyChanged` interfacet i view-model. Vi implementerede interfacet i alle vores view-models med nedarvning fra superklassen `ViewModelBase`:

```cs
public abstract class ViewModelBase
  : INotifyPropertyChanged
{
  public event PropertyChangedEventHandler?
  PropertyChanged;

  protected virtual void OnPropertyChanged(
    [CallerMemberName] string? propertyName = null)
  {
    PropertyChanged?.Invoke(
      this,
      new PropertyChangedEventArgs(propertyName));
  }
}
```
og kaldte interfacets `OnPropertyChanged` metode i vores view-model property `set` metoder:

```cs
public class CreateBookingViewModel : ViewModelBase
{
  public DateTime? Date
  {
    get;
    set
    {
      if (field == value) return;
      field = value;
      OnPropertyChanged();
      ApplyDefaultTime();
    }
  }
}
```

Resultatet blev at når en bruger ændrer dato eller tid i brugergrænsefladen kaldes property `set` metoden og opdatere tilstanden i view-model. Når vores hjælpe metoder (eks. `ApplyDefaultTime()`) kalder `set` metoderne og sikre at `StartTime` og `EndTime` ikke er tomme, så bliver værdierne synlige i brugergrænsefladen.

Til vores overraskelse er bindings ikke nok til at implementere knappers funktionalitet. WPF forventer et `ICommand` objekt, med metoderne `Execute()` og `CanExecute()`. I stedet for at implementere interfacet direte i vores view-model klasser, brugte vi et lag af _indirection_ til at adskille realiseringen og funktionaliteten. Fordelen i vores program var at vi slap for at flere klasser skulle realisere interfacet ved i stedet kun at implementere det i klassen `RelayCommand`.

```cs
public class RelayCommand : ICommand
{
  private readonly Action<object?> _execute;
  private readonly Predicate<object?>? _canExecute;

  public bool CanExecute(object? parameter)
  {
    return _canExecute?.Invoke(parameter) ?? true;
  }

  public void Execute(object? parameter)
  {
    if (!CanExecute(parameter)) return;
    _execute(parameter);
  }
}
```

Klassen er en tom skal der ikke gør andet end at gemme hver view-models knap funktionalitet i en delegate. For at koble hver instans til en bestemt view-model brugte vi komposition. Relay instansen instantieres i constructoren og får injektet den konkrete view-models execute og canExecute metoder ind i sine private variabler.

```cs
public class CreateBookingViewModel : ViewModelBase
{
  public CreateBookingViewModel(BookingService service)
  {
    RegisterBookingCommand = new RelayCommand(
      execute: async param =>
        await ExecuteBookingAsync(param),
      canExecute: param => CanPlaceBooking(param)
    );
  }

  public RelayCommand RegisterBookingCommand { get; }
}

```

Binding spiller stadig en rolle, fordi knapperne i view binder til de properties der peger på `RelayCommand` objektet med samme xaml syntaks `Command="{Binding RegisterBookingCommand}"`. Internt ved WPF at binding skal leverer et `ICommand` objekt til `Command` attributten for at den kan kalde interfacets metoder. Command mønstret kommer til sin ret, fordi det tillod os at oprette forskellige kommandoer i forskellige view-models uden at have en direkte kobling mellem et bestemt view og view-model.

Med disse to mønstre, kunne vi flytte interaktionslogikken væk fra view-code-behind, hvor den var direkte koblet til brugergrænsefladen og over til vores view-model. Resultatet blev at view-model kunne fungerer som en adapter mellem brugergrænsefladen og vores forretnings- og domænekerne. Dette eksemplificeres bedst med hvordan kontrakten til præsentations- og applikations laget er forskellige trods at dataen er den samme.

```cs
public class CreateBookingViewModel : ViewModelBase
{
  //------------------------------------------------------
  // UI kontrakt: properties der binder til view
  //------------------------------------------------------
  public DateTime? Date
  {
    get;
    set
    {
      field = value;
      OnPropertyChanged();
    }
  }

  public TimeSpan StartTime
  {
    get;
    set
    {
      field = value;
      OnPropertyChanged();
    }
  }

  public TimeSpan EndTime
  {
    get;
    set
    {
      field = value;
      OnPropertyChanged();
    }
  }
  //------------------------------------------------------
  // Domæne kontrakt: properties der sendes til kernen
  //------------------------------------------------------
  public DateTime Start { get; private set; }
  public DateTime End { get; private set; }
}
```
