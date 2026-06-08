---
created: 2026-06-04
section:
exclude: false
sortKey: 29.51472
---
[[Git historik]]
### Det Kronologiske Arbejdsflow

- **Arkitektonisk Afkobling (1. - 2. juni):** Ugen startede med at flytte ViewModel-laget isoleret over i WPF-projektet for at håndhæve arkitektoniske grænser. I servicelaget blev afhængigheder af UI'et minimeret; eksempelvis blev `TryBookOptimalVehicle()` refaktoreret til selv at hente data fra databasen, frem for at stole på lister sendt som parametre fra interfacet. Repository-kald returnerer nu sekvenser (`IEnumerable<Vehicle>`) i stedet for konkrete lister (`List<Vehicle>`), hvilket sikrer løsere kobling. Sideløbende implementerede Lasse data seeding og dynamisk knap-tilgængelighed baseret på systemets tilstand.
    
- **Robusthed og Concurrency (3. juni):** Med de arkitektoniske grænser på plads, skiftede fokus til systemets integritet under samtidige anmodninger. Anna udviklede og færdiggjorde `RaceConditionTest` for at verificere systemets adfærd, hvorefter Lasse implementerede løsninger for identificerede race conditions. Domænemodellen blev udvidet med en simpel lynbooking-funktion, mens brugergrænsefladen modtog notifikationer om bekræftelser, kombineret med en generel oprydning af ubrugte namespaces og overflødig C#-kode.
    
- **Polering og Systemdokumentation (4. juni):** Ugen afsluttes med at stramme de sidste invariants i systemets logik, herunder Lasses implementering af nedrunding til nærmeste kvarter ved lynbookinger samt færdiggørelsen af Annas test- og bookingmetoder. For at bevare en præcis mental model af systemets endelige interaktioner, udarbejdede Matias de endelige sekvensdiagrammer, der detaljeret kortlægger datastrømmen mellem View og ViewModel.