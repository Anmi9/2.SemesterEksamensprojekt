---
created: 2026-05-20
section: elaboration 2
exclude: false
sortKey: 14.33703
---

FLOW

Bruger er logget ind
Viewmodel anmoder om start og sluttid
Bruger vælger starttid
Bruger vælger sluttid
Viewmodel sender start og slut af sted til vehiclerepository
Vehiclerepository finder ledig liste i database og sender tilbage til ViewModel
ViewModel filtrerer liste i to typer
ViewModel præsenterer tilgængelige typer for bruger
Bruger vælger type
Viewmodel sender start, slut og v.id af sted og kalder CreateBooking  i BookingService
Bookingservice opretter objekt
BookingService kalder Create() i bookingRepository og sender objektet med 
bookingRepository 'creater' db 




