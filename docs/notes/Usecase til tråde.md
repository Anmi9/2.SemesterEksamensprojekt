---
created: 2026-05-26
section:
exclude: false
sortKey: 20.34516
---

# Pre-condition
	Medarbejderen har valgt et tidsinterval
	Medarbejderen har valgt en ledig køretøjstype
	Systemet har valgt et specifikt køretøj

# Primær aktør
	Systemet

# Success kriterie
	Undgå dobbeltbooking

# Success scenarie 
	Vores system sender en bookingentitet af sted til databasen
	Entiteten bliver valideret og persisteres eller afvises
	Returnerer status [[Use case - opret booking]] 




