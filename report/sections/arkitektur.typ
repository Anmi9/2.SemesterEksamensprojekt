DI vs Composition

Valgte DI (proof of ability - så vi kan komme videre)

Reflektion over DIFactory docs/notes/Arkitketur - DbContext med Composition eller DI - en kombi

Reflektion over bookingfunktion, og hvordan vi var ved at genskabe det problem vi forsøgte at løse se docs/notes/Tanker omkring bookingfunktionen

Bevis hvorfor det var risikoen værd (indsæt risk-value matrix som kommunikations artefakt)


valg af DI root over komposition
overvejde en mellem ting: injekte en "kompositionbuilder/factory pattern". Det er en stærk mellemvej mellem de to andre tilgange, men har en øget kompleksitet ift. hvad vi har erfaring med og den kognitive byrde der ligger i at skulle bruge en nyt møsnter/teknologi
Besluttede at bruge facade pattern for at bedre at kunne uddelegere implementeringsarbejde blandt os
