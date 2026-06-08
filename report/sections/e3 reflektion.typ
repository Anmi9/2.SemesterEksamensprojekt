I vores projekte har vi orienteret os efter Craig Larmans
Vi har fulgt UP ikke tilpasset up til vores proojekt og behov
Vi har ville lave produktionsklar kode fra starten hvilket er over vores evner -> gik i gang med at lave proof of ability kode
Larman siger at man laver slices ikke lag, det har gjort os langsomme fordi vi har skulle overskue hvordan alle lag i vores walking skeleton snakkede sammen, hvilket vi ikke kunne og derfor heller ikke kunne uddelegere opgaver og øge produktiviteten
teoretisk UP faser vs rapport framework faser

Måske en delrefleksion om test af trådhåndtering?:
Vores system kører på nuværende tidspunkt lokalt med en lokal database og en enkelt delt context-instans. Selvom det rent teknisk kan lade sig gøre at registrere lynhurtige og samtidige brugerhandlinger ved at taste hurtigt, fordi vi har lavet asynkrone metoder, vil det i praksis være umuligt at fremprovokere manuelt for at demonstrere risikoen. Dette skyldes vores bevidste placering af vores lås. EF Core ville crashe vores system, før en race condition opstår. Her overvejer vi at implementere en test i en senere iteration, der kan simulere samtidigheden med to oprettede context-instanser og demonstrere, at vores SemaphoreSlim fungerer efter hensigten, havde der været instanser.

Refleskion over risikoen ved asynkronitet vs. UI-blokering:




