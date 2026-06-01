I et forsøg på at løse to problemer på en gang - overholde formalia om implementering af algoritme i vores system, samtidig med at vi ønsker at skabe værdi for vores brugere - valgte vi at tilføje en algoritme, der skal udregne det ledige køretøj, som har den mindste tidsforskel (gap) mellem det valgte start- og sluttidspunkt. Ved at lade en bil med den mindste tidforskel være det optimale køretøj risikerer vi ikke en meget 'hullet' bookingkalender, hvor der spildes tid på små ledige intervaller mellem bookinger. I udarbejdelsen af algoritmen har vi på nuværende tidspunkt valgt at benytte os af LINQ-metoderne, OrderBy og OrderByDescending, der er en noget tungere sortering, end hvis vi havde implementeret fx en foreach, der kørte alle bookinger igennem en gang for hvert køretøj - den ville blot filtrere og beregne. Det har været et bevidst valg at prioritere en kortere og mere clean kode indtil videre, da vores system heller ikke forventes at skulle håndtere datamængder, der er så store, at LINQ-metoderne ville have en negativ påvirkning på bookingsystemets performance.

Vores nuværende system kører lokalt med en lokal database. Selvom det rent teknisk kan lade sig gøre at skabe samtidige brugerhandlinger ved at taste hurtigt, kan det være svært at fremprovokere manuelt. Her overvejer vi at implementere en test i en senere iteration, der kan simulere samtidigheden og demonstrere, at vores SemaphoreSlim fungerer efter hensigten. Implementeringen af SemaphoreSlim har været med henblik på, at systemet skal kunne køre på en server, hvor flere brugere kan foretage samtidige handlinger. Med hensyn til vores databasekald har vi valgt at gøre dem asynkrone. Det er for det første god praksis for at sikre, at UI-tråden ikke blokeres, men det er igen også for at forberede os på en fremtidig implementering, hvor systemet skal kunne håndtere samtidige brugerhandlinger.




synkroniserings mekanisme: hvilken har vi valgt? Hvorfor har vi brugt en (domæne krav/værdiskabelse: kræver distribution af database)
asynkron kode: til IO
seperation of concerns i view/viewmodel -> medførte brugen af bindings
business rules: start-/endtime
ingen magiske tal
Design by contrant (invarianter i koden/statisk konstruktor til at garantere defaultDuration > minDuration)


