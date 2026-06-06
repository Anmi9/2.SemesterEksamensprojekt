I et forsøg på at løse to problemer på en gang - overholde formalia om implementering af algoritme i vores system, samtidig med at vi ønsker at skabe værdi for vores brugere - valgte vi at tilføje en algoritme, der skal udregne det ledige køretøj, som har den mindste tidsforskel (gap) mellem det valgte start- og sluttidspunkt. Ved at lade en bil med den mindste tidforskel være det optimale køretøj risikerer vi ikke en meget 'hullet' bookingkalender, hvor der spildes tid på små ledige intervaller mellem bookinger. Med et aftalt fokus på at skabe skelettet og bookingflowet som et udgangspunkt til refactoring, blev algoritmen i første omgang håndteret med moderne LINQ-sorteringsmetoder, OrderBy og OrderByDescending, der er relativt tunge sorteringer fremfor fx en foreach med if-sætninger. På dette tidspunkt i processen var det en beslutning om at prioritere en kortere, mere læsbar kode, så vi hurtigst muligt kunne se effekten af vores arbejde. Da vores system heller ikke forventes at skulle håndtere så store datamængder, at LINQ-metoderne ville have en negativ påvirkning på bookingsystemets performance, blev det løsningen indtil videre.


Valg af asynkrone databasekald:
Med hensyn til vores databasekald har vi valgt at gøre dem asynkrone. Det er for det første god praksis, når man har med I/O at gøre og det sikrer, at UI-tråden ikke blokeres, mens databasekaldene udføres. Samtidig er det også for at forberede os på en fremtidig implementering, hvor systemet skal kunne håndtere samtidige brugerhandlinger mest effektivt. Som systemet er i øjeblikket er det ikke muligt for to tråde at have adgang til databasen på samme tid, da vi har taget en bevidst beslutning om kun at oprette en instans af vores Context-klasse, der skal køre i hele systemet. Da EF Core ikke er trådsikker, tillader det ikke at dele det nuværende systems eneste context-objekt. Her forhindrer vores async/await kald som sagt kun UI-tråden i at blive blokeret. Havde vi i stedet valgt at oprette flere midlertidige objekter af Context-klassen, som ville være nødvendigt, hvis vores bookingsystem skulle fungere i en distribueret kontekst, ville kombinationen af asynkron kode og flere instanser tillade vores EF Core at håndtere flere kald parallelt. Flere instanser ville gøre vores brug af EF Core trådsikker, fordi et context-objekt ikke længere ville blive delt mellem flere tråde. VI undgår, at EF Core crasher systemet, fordi to tråde forsøger at få adgang til samme context-objekt.

Valg af synkroniseringsmekanisme:
Da vores system deler en enkelt context-instans risikerer vi som sagt at få problemer, hvis to tråde vil have adgang til vores context på samme tid. Det kræver en form for trådhåndteringsmekanisme. Umiddelbart skulle vi implementere en sådan mekanisme alle de steder i vores system, hvor vi kalder databasen, men det ville skabe en rodet kode og være svær at vedligeholde. Alternativt skulle vi have implementeret en global lås, der helt ville udelukke risikoen for et crash. Dog ville sådan en global mekanisme også ødelægge alle fordelene ved vores asynkrone metoder. Med tanke på at vi arbejder hen imod en fremtidig skalering, hvor systemet skal kunne håndtere flere brugere og samtidige handlinger, virker sådan en implementering unødvendig i dette projekt. Skal bookingsystemet i fremtiden flyttes til en central server, ville hele synkroniseringensansvaret ligge hos databasens sikkerhedssystemer. Vi har valgt at implementere en SemaphoreSlim, der skal håndtere adgangen til vores mest kritiske del af koden, hvor en validering og en bookingoprettelse sker. Vores beslutning skal derfor ses mest som en demonstration af trådhåndtering inden for de rammer, projektet har og altså ikke en endelig løsning for en produktionsklar version. Vores SemaphoreSlim



Måske en delrefleksion om test af trådhåndtering?:
Vores system kører på nuværende tidspunkt lokalt med en lokal database. Selvom det rent teknisk kan lade sig gøre at skabe samtidige brugerhandlinger ved at taste hurtigt, fordi vi har lavet asynkrone metoder, vil det være nærmest umuligt at fremprovokere manuelt for at demonstrere risikoen. Her overvejer vi at implementere en test i en senere iteration, der kan simulere samtidigheden og demonstrere, at vores SemaphoreSlim fungerer efter hensigten.





synkroniserings mekanisme: hvilken har vi valgt? Hvorfor har vi brugt en (domæne krav/værdiskabelse: kræver distribution af database)
asynkron kode: til IO
seperation of concerns i view/viewmodel -> medførte brugen af bindings
business rules: start-/endtime
ingen magiske tal
Design by contrant (invarianter i koden/statisk konstruktor til at garantere defaultDuration > minDuration)


