---
created: 2026-06-01
section:
exclude: false
sortKey: 26.30865
---

Mål: at få et fungerende produkt hvor det fulde flow i use case - opret booking kan gennemføres
Tilstands: programmet kan allerede oprette en booking i databasen, men dette bekræftes ikke i UI'en, ingen bekræftigelse. UI'et kommunikere ikke om den indtastede information producere en gyldig booking eller ej hvilket strider imod kravet om at reducere antal kliks/tid for at udføre en booking.
Opgaver: 
- Data over tilgængelighed skal opdateres løbende når ny information er indtastet
	- Undgå at kalde databasen ved hver keystroke med debounce (kort delay)
- Gøre booking knapper dynamiske med state: on, off, default (mangler info) ^62aa9f
- Lukke booking vindue når booking er gennemført
- Adskille forretningslogik (service klasse) og UI (ViewModel)
	- Evt. med en DTO
- Fjerne knap/UI logik fra code-behind til ViewModel. View er kun ansvarlig for visning af UI elementer (initializeComponents) ikke adfærden
	- Brug command pattern (via ICommand) til at håndtere knappernes tilstande og funktionalitet uden om View klassen (se [[#^62aa9f|også]])
