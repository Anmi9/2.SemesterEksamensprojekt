---
created: 2026-05-19
section:
exclude: false
sortKey: 13.32858
---
Vi har valgt at holde kompleksiteten lav, ved gå væk fra at oprette et tomt booking objekt og sette data i. I stedet samler vi alt dataen objektet skal indeholde og så opretter obkjektet. Vi starter ned at lave en simplecere inplementering der går på kompromis med vores sekvensdiagram: konkret ved returnere en fuld liste af tilgængelige vehicles fra databasen frem for kun de typer som brugeren har mulighed for at booke. Det kræver mindre logik og gør det hurtigere for at få vist noget på skærmen.

---