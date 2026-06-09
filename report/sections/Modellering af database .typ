Ud fra en fælles oplevelse af at have brugt rigtig meget tid på domæneanalyse og kravspecifikation blev vi enige om at domænemodellen som værktøj ikke længere ville tilføje ekstra til vores forståelse. Vi vurderede, at vi havde fået os en rigtig god domæneindsigt i vores indledende arbejde, der gjorde det muligt for os at gå direkte i gang med et ER-diagram over vores database - vi prioriterede tid. Med dette diagram ville vi kunne få et overblik over de regler, der gælder for vores systems data, og hvordan de hænger sammen med hinanden. Denne proces blev sværere end forventet, hvorfor vi måtte stoppe op og i stedet skrive en use case for vores mest centrale must-krav: "Opret booking" for at strømline vores tanker om implementeringen. (INDSÆT evt. Use case af Opret Booking her) I den proces gik det op for os, at vi trods meget domænearbejde alligevel ikke var helt enige om det videre arbejde. Det fik også til at træde et skridt tilbage og stille spørgsmålet: "Hvad er formålet egentlig?" Selvom use case-arbejdet hjalp os til at forstå, hvordan systemet skulle fungere, manglede vi stadig afklaring for at kunne strukturere dataen optimalt. Derfor formulerede vi en række forretningsregler med udgangspunkt i vores must-krav og foreløbige tanker endtil nu.
(INDSÆT evt. forretningsregler her)
Det endte med at give os den klarhed, vi havde savnet for at kunne modellere vores database hensigtsmæssigt til vores system. Vi tog da en fælles beslutning om at gå direkte videre med at modellere en logisk model ud fra de entiteter, vi havde identificeret i det indledende arbejde.

Den logiske model blev udviklet i etaper, hvor vi løbende tog nogle strategiske beslutninger. I første version identificerede vi de mest centrale entiteter og nøgler. (INDSÆT logisk model version 1) Her diskuterede vi blandt andet nødvendigheden af at have en separat tabel for køretøjer, og hvor meget følsom data der skulle gemmes om brugerne. Her blev den endelige logiske model udviklet efter, hvor vi var i processen, og hvad der gav mest mening i forhold til projektets omfang og formål. Det endte med tre tabeller: Employee, Vehicle og Booking. Medarbejdernes interne bookingsystem havde ikke brug for at gemme følsomme data (GDPR) for brugerne, hvorfor vi kun gemte id og initialer. På den måde Samtidig valgte vi at oprette en samlet Vehicle-tabel fremfor at splitte den op i særskilte tabeller.
(INDSÆT version 2 af logisk model her)
Vi får endvidere defineret kardinaliteterne mellem tabellerne ud fra domænets forretningsregler. En medarbejdere kan have alt fra 0 til mange bookinger i systemet, og det samme er tilfældet for køretøjer. Den enkelte bil eller cykel kan være booket 0 til mange gange. Hver specifik booking, der oprettes, skal indeholde præcis en medarbejder og et køretøj. Derfor er der en mange til en relation mellem både Employee og Booking og mellem Vehicle og Booking. Hver tabel har et unikt id, der fungerer som primærnøgle (PK), samtidig med at booking-tabellen har fremmednøgler (FK) til både Employee og Vehicle.
Vi forsøgte at holde vores model så simpel som muligt på dette stadie - med en mulighed for at udvide den senere, hvis det skulle blive nødvendigt i takt med at vi blev klogere under implementeringsarbejdet. Det var et bevidst valg om at holde muligheden åben for at skalere op og ikke låse os fast for tidligt.

Valg af Database-management system:
Vi havde i starten af projektet taget en beslutning om at benytte os af EF Core som object-relational mapper til at håndtere interaktionen mellem vores kode og databasen.
Efter det indledende designarbejde af database-tabellerne ovenfor, og da vi fik afklaret, at der ikke var krav om en serverbaseret løsning, valgte vi at konfigurere EF Core til at køre med SQLite fremfor en standard databaseserver som MS SQL Server. SQLite er en enkel, filbaseret database, og det passede rigtig godt til vores skoleprojekt-scope og med vores erfaring i databasesystemer på dette tidspunkt. Da SQLite gemmer hele databasen i en fil, undgik vi at installere og konfigurere en server. Det gør det samtidig nemt at dele vores projekt i forbindelse med aflevering.

Entity Framework Core:
Valg af EF Core til dette projekt baserer sig på flere faktorer. Under den indledende projektplanlægning blev vi enige om at benytte vores fælles 'tech stack', så vi ikke faldt i den velkendte fælde at introducere en masse nye teknologier, vi ikke har tid til at lære. EF Core lå i stacken, og det gjorde det til et oplagt valg. Derudover giver EF Core os mulighed for en 'Code-First'-tilgang, så vi kan definere vores tabeller direkte i koden ved at oprette C\#-klasser. Herefter kan vi automatisk ændre strukturen i databasen ved hjælp af Migrations-værktøjet. Det går altså hånd i hånd med vores iterative proces, hvor vi løbende nemt skal kunne tilpasse og ændre databasen efter behov. Til sidst har brugen af EF Core også den fordel, at man uden problemer kan skifte management system i en senere udviklingsfase blot ved at ændre konfigurationen direkte i vores Context-klasse i metoden OnConfiguring()(Evt. kodeeksempel). Det gør vores WPF-app fremtidssikret, fordi vi ikke låser os fast på en specifik database-løsning.
















Bygger DB
- Valg af SQLite over SQL Server (MS)
- Trade-offs
- Politiske overvejelser
