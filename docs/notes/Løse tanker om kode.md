---
created: 2026-06-01
section:
exclude: false
sortKey: 26.49086
---

For at undgå hård kobling mellem ViewModel og Servicelag dropper vi at sende viewmodel-objekt med til bookingservice i bookingflowet. I stedet sendes start, end og type. 
Det er bookingservice der henter listen af den valgte type og hele listen af bookinger

Lige nu genbruger vi metode i vehiclerepo GetAllAvailableVehicles, så UI'et kan opdatere efter brugerinput, og så bookingflowet fungerer og algoritmen i bookingservice har en valgt type af lister at arbejde med (Måske der skal laves en overload i stedet?)

Overvejer om der skal laves test af vores trådhåndtering, så vi kan demonstrere, hvad semaphoreSlim kan, hvis bookingsystemet lå på server. 
Skal vi pakke Semaphoreslim-delen ind i en metode a la TryCreateBooking() i Bookingrepository? 

Skal vi implementere factory-pattern - så bookingservice ikke har ansvar for at oprette objekt?  