---
created: 2026-06-01
section: construction 1
exclude: false
sortKey: 26.30865
---
[[Reflektion over OOA-D til E3]]

---
Mål: 
- at få et fungerende produkt hvor det fulde flow i use case - opret booking kan gennemføres, 
- samt se ledige køretøjer (typer) lige nu/hurtig booking feature fra MoSCow - must 
- samt booking bekræftigelse fra should.
Tilstand/udgangspunkt: programmet kan allerede oprette en booking i databasen, men dette bekræftes ikke i UI'en, ingen bekræftigelse. UI'et kommunikere ikke om den indtastede information producere en gyldig booking eller ej hvilket strider imod kravet om at reducere antal kliks/tid for at udføre en booking.

Opgaver: 
- Data over tilgængelighed skal opdateres løbende når ny information er indtastet
	- Undgå at kalde databasen ved hver keystroke med debounce (kort delay)
- Gøre booking knapper dynamiske med state: on, off, default (mangler info) ^62aa9f
- Lukke booking vindue når booking er gennemført
- Adskille forretningslogik (service klasse) og UI (ViewModel) ^7a1d04
	- Evt. med en DTO
- Fjerne knap/UI logik fra code-behind til ViewModel. View er kun ansvarlig for visning af UI elementer (initializeComponents) ikke adfærden
	- Brug command pattern (via ICommand) til at håndtere knappernes tilstande og funktionalitet uden om View klassen (se [[#^62aa9f|også]])

> [!NOTE] Kommentar
> [[#^7a1d04]] blev klaret ved at flytte alt presentation kode (ViewModel) over i WPF projektet. 
> [[#^7a1d04]] Dependencies blev fjernet ved at sende data literals afsted frem for hele ViewModel objektet.
> [[#^7a1d04]] Undgik at DI root (application laget) skulle have kendskab til viewmodel ved at lade ViewModel tilgå repo data via en ny public query metode i BookingService (`GetAvailableVehicles()`).
> [[#^7a1d04]] MVVM siger ikke noget om at VM ikke må tilgå persisterings laget, men vi har valgt at gøre vores applications lag til en tragt hvor data skal flyde igennem. Dette er for at undgå at koble View og persistering sammen da vi pt implementere i SQLite men i fremtiden skal applicationen destribueres på en server.