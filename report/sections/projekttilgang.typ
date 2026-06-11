Da vi modtog opgaveformuleringen og projektet formelt begyndte, startede vi med at afklare vores forventninger, klarlægge interne aftaler og arbejdsform, beslutte os for en techstack og lægge en overordnet plan jvf. Unified Process.


// Forventningsafstemning
I modsætning til tidligere projekter hvor vores primære fokus har været læringen, var vi enige om at flytte fokus denne gang til produktet. Grunden til dette var todelt, det var både for at tvinge os til, at blive bedre til produktudvikling og ikke falde tilbage i 'analyse paralyse' men ligeledes for at kunne tilføje et projekt til vores repositories som vi stolt kunne vise frem.

// Interne aftaler og arbejdsform
Vi indgik en række konkrete after for at sikre kontinuitet, koordinering og fremdrift i gruppen:
- Daily standup som primært koordineringsværktøj med det formål at synkronisere vores fokus.
- Fredage var dedikeret til rapportskrivning for at sikre en kronologisk korrekt rapport og undgå manglende dokumentation i slutfasen.
- Fredagsmøde kl. 12 for at vurdere fremdrift i løbet af ugen samt alignment til den kommende uge.

// Valg af techstack
Vi valgte en velkendt techstack: C\# med WPF til brugergrænseflade, Entity Framework Core med SQL (hvilket vi ændrede) til databasen. Dette var en form for risikostyring. Da dette projekt var det første hvor vi havde et reelt problemdomæne og reelebrugere at interviewe, vidste vi udemærket godt, at vi ville bruge mere tid på discovery-arbejde, end hvad vi har gjort i tidligere projekter. Vi genbrugte vores projektskabelon i Typst og Obsidian. Ved ikke at introducere ny teknologi kunne vi koncentrere vores kapacitet om domæneforståelse og kravanalyse.

// Iterationsplan og Gantt
For at lægge den overordnede plan for projektet valgte vi at benytte et Gantt diagram. Gantt skulle fungere som et udgangspunkt og ikke som en bindende kontrakt. Tidligere projekter har lært os, at et at lægge en fuldkommen plan på dette tidspunkt i projektet primært vil bygge på antagelser der ikke har hold i virkeligheden. Derfor besluttede vi, at vi for hver iteration ville udfærdige en plan ud fra og kun bruge dette oprindelige Gantt diagram som en oversigt over iterationsplanen.
Med udgangspunkt i UP's faser planlagde vi følgende forløb:

#quote(
  block: true,
  [
    Inception: 5 arbejdsdage\
    Elaboration 1-2: 2 x 4 arbejdsdage\
    Construction 1: 5 arbejdsdage\
    Rapport og aflevering: 5 arbejdsdage\
    Repport skrivning talte ikke som fasedage.
  ],
)

#page(
  image("../assets/Gantt v1.png"),
  flipped: true,
  width: auto,
  height: auto,
  margin: 0pt,
)

Vi var bevidste om, at overflow fra en iteration til den næste var sansynligt og vi aftalte at overflow skulle vurderes ved faseskift for at sikre det stadig var inden for scope. Vi diskuterede ligeledes at benytte burndown charts til arbejdsopgaverne i de enkelte iterationer for at sikre fremdrift og give en form for 'sense-of-urgency' - hvilket hurtigt viste sig ikke at være nødvendigt da den kom helt naturligt.

