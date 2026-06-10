Den oprindelige interaktionslogik var event-driven og skrevet i code-behind. Vi havde brugt den standard event handler kode som oprettes når man dobbeltklikker på et UI control-element i WPF Designer. Så da vi ville flytte logikken over til vores view-model C\# klasse mødte vi en begrænsning ved WPF. Nemlig at de autogenerede event handlers kun fungerede fordi de var placeret i code-behind. Vi lærte at for at forbinde view med en view-model, skal vi bruge WPFs implementeringer af _Observer_ og _Command_ mønstrene. Konkret via _Bindings_ abstraktionen og `ICommand` interfacet. Bindings skaber en tovejs forbindelse mellem en UI controller og en data property, hvilket sikre at tilstandsændringer propagere mellem view og view-model. Det implementeres via xaml binding syntaksen `{Binding Path=PropertyName}` i view og ved at realisere `INotifyPropertyChanged` interfacet i view-model. Vi implementerede interfacet i alle vores view-models med nedarvning fra superklassen `ViewModelBase`:

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


