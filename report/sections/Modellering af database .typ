Ud fra en fælles oplevelse af at have brugt rigtig meget tid på domæneanalyse og kravspecifikation blev vi enige om at domænemodellen som værktøj ikke længere ville tilføje ekstra til vores forståelse. Vi vurderede, at vi havde fået os en rigtig god domæneindsigt i vores indledende arbejde, der gjorde det muligt for os at gå direkte i gang med et ER-diagram over vores database - vi prioriterede tid. Med dette diagram ville vi kunne få et overblik over de regler, der gælder for vores systems data, og hvordan de hænger sammen med hinanden. Denne proces blev sværere end forventet, hvorfor vi måtte stoppe op og i stedet lave en use case for vores mest centrale must-krav: "Opret booking" for at strømline vores tanker om implementeringen. (INDSÆT evt. Use case af Opret Booking her) I den proces gik det op for os, at vi trods meget domænearbejde alligevel ikke var helt enige om det videre arbejde. Det fik også til at træde et skridt tilbage og stille spørgsmålet: "Hvad er formålet egentlig?" Selvom use case-arbejdet hjalp os til at forstå, hvordan systemet skulle fungere, manglede vi stadig afklaring for at kunne strukturere dataen optimalt. Derfor formulerede vi en række forretningsregler med udgangspunkt i vores must-krav og foreløbige tanker endtil nu.
(INDSÆT evt. forretningsregler her)
Det endte med at give os den klarhed, vi havde savnet for at kunne modellere vores database hensigtsmæssigt til vores system. Vi tog da en fælles beslutning om at gå direkte videre med at modellere en logisk model ud fra de entiteter, vi havde identificeret i det indledende arbejde.

Den logiske model blev udviklet i etaper, hvor vi løbende tog nogle strategiske beslutninger. I første version identificerede vi de mest centrale entiteter og nøgler. (INDSÆT logisk model version 1) Her diskuterede vi blandt andet nødvendigheden af at have en separat tabel for køretøjer, og hvor meget følsom data der skulle gemmes om brugerne. Her blev den endelige logiske model udviklet efter, hvor vi var i processen, og hvad der gav mest mening i forhold til projektets omfang og formål. Det endte med tre tabeller: Employee, Vehicle og Booking. Medarbejdernes interne bookingsystem havde ikke brug for at gemme GDPR/følsomme data for brugerne, hvorfor vi kun gemte id og initialer. På den måde Samtidig valgte vi at oprette en samlet Vehicle-tabel fremfor at splitte den op i særskilte tabeller. Vi forsøgte at holde vores model så simpel som muligt på dette stadie - med en mulighed for at udvide den senere, hvis det skulle blive nødvendigt i takt med at vi blev klogere under implementeringsarbejdet. Det var et bevidst valg om at holde mulighden åben for at skalere op og ikke låse os fast for tidligt.
(INDSÆT version 2 af logisk model her)
Vi får endvidere defineret relationerne mellem tabellerne.

Oprettelse af Database

















Bygger DB
- Valg af SQLite over SQL Server (MS)
- Trade-offs
- Politiske overvejelser
