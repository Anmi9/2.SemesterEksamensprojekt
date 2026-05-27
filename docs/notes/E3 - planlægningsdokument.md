---
created: 2026-05-26
section: elaboration 3
exclude: false
sortKey: 20.70822
---
Elaboration 3 - planlægningsdokument.
26-28 maj 2026


Overflow fra tidligere iteration:

Færdig gør "Opret Booking".
	Færdiggøre skelettet.
	Create operation
Færdiggør DI-root.
	Der åbner to vinduer lige nu.
Udfærdig sekvensdiagram.
Opdaterer klassediagram.

MÅL: Når vi kan se booking er korrekt oprettet og gemt i databasen.



Outcome: 
	Medarbejderen skal være sikre på, at de deres booking er gyldig (tråde/async), 
	Flere ledige køretøjer (Maksimere tilgængelighed af køretøjer ved at gøre brug af algoritmer til at placere bookings på vehicleid hvor det giver bedst mening)

Tidsestimering
	Ensretning omkring tid de to use cases vil kræve og validering om de begge kan være i den samme 3 dags iteration.

Iterations dokument
	Business Modeling
		Validering af at vi stadig er i tråd med opgavenformuleringen,
		Identificering af fit mellem hårde krav - risk (system) - value (usercentric).
Requirements
	Use Cases, én til hver. (alignment)
Analysis & Design
	Opdater sekvensdiagram med async.
Implementation
Test
	Unit-tests
Deployment

MÅL: Booking oprettes asynkron og bekræftigelse til brugeren om det er lykkes (med det samme)
MÅL: Bookinger placeres automatisk hensigtsmæssigt på et køretøj. (Booking placeres det mindst mulige timeslot hvor det stadig passer ind, så der er plads til flest mulige bookings.

**Iterationsmål: Booking kan oprettes asynkront, gemmes korrekt i databasen og give brugeren tydelig bekræftelse, mens systemet automatisk vælger et hensigtsmæssigt køretøj.**

**Kronologi**:
Tirsdag:
	Tekniske use cases:
	UC algoritme
	UC tråde
	Klassediagram opdatering (forud for design og implementering)
