Beslutningen om at lave personaer udsprang fra to betragninger. Vi havde svært ved at overskue den store mængde data vi havde indsamlet i vores første interviewrunde og fundet igennem Empathy maps, og dertil for at sørge for, at vi havde et fælles udgangspunkt at designe udfra.

Vi designede skabelonerne så det gav mening, med vores arbejde i Costumer-delen af Value Proposition Canvas samt Empathy mapping, således vi kunne genbruge så meget som muligt men stadig havde en konkret persona at henvise til i designarbejdet. I vores forudgående arbejde havde vi identificeret to forskellige arbejdsgange i brugen af deres nuværende booking-system. Den ene var brugen af systemet som planlægningsværktøj og den anden var ad-hoc booking. Naturlig udsprang der forskellige behov fra disse.  Da disse arbejdsgange var tilstede i alle vores interviewpersoner, lavede vi fiktive personaer som hver representerede een af dem.
// Persona: Christian
#figure(
  block(
    width: 100%,
    stroke: 0.5pt + luma(150),
    radius: 4pt,
    fill: rgb("#fafafa"),
    inset: 15pt,
  )[
    #text(weight: "bold", size: 16pt, fill: rgb("#1a3a5f"))[Persona: Christian]
    #v(4pt)
    #text(size: 9pt, fill: luma(100))[Alder: 30 | Uddannelse: Pædagog | Bopæl: Randers]
    #v(8pt)
    #line(length: 100%, stroke: 0.5pt + rgb("#e0e0e0"))

    #text(weight: "bold", size: 11pt, fill: rgb("#1a3a5f"))[Biografi]
    #text(size: 10pt)[Christian er forholdsvis ny medarbejder på tulipanvej... (indsæt fuld tekst)]

    #v(10pt)
    #grid(
      columns: (1fr, 1fr),
      gutter: 15pt,
      [
        #text(weight: "bold", size: 10pt)[Mål]
        #list([Altid have borgernes behov for øje], [Finde en fast rutine])
      ],
      [
        #text(weight: "bold", size: 10pt)[Frustrationer]
        #list([Navigere i uskrevne regelsæt], [Manuelt arbejde])
      ],
    )
  ],
  caption: [Persona-profil for Christian],
)

#v(20pt)

// Persona: Malene
#figure(
  block(
    width: 100%,
    stroke: 0.5pt + rgb("#2c3e50"), // Anden farve for at vise lokal styring
    radius: 4pt,
    fill: rgb("#fcfcfc"),
    inset: 15pt,
  )[
    #text(weight: "bold", size: 16pt, fill: rgb("#2c3e50"))[Persona: Malene]
    #v(4pt)
    #text(size: 9pt, fill: luma(100))[Alder: 46 | Uddannelse: Pædagog | Bopæl: Silkeborg]
    #v(8pt)
    #line(length: 100%, stroke: 0.5pt + rgb("#bdc3c7"))

    #text(weight: "bold", size: 11pt, fill: rgb("#2c3e50"))[Biografi]
    #text(size: 10pt)[Malene har arbejdet mange år som bostøtte... (indsæt fuld tekst)]

    #v(10pt)
    #grid(
      columns: (1fr, 1fr),
      gutter: 15pt,
      [
        #text(weight: "bold", size: 10pt)[Mål]
        #list([Udføre arbejde uden kompromis], [Undgå stress])
      ],
      [
        #text(weight: "bold", size: 10pt)[Frustrationer]
        #list([Manglende overblik], [Ukontrollerbare bookinger])
      ],
    )
  ],
  caption: [Persona-profil for Malene],
)
(Indsæt Persona Christian)
Christian er forholdsvis ny medarbejder på Tulipanvej og navigerer stadig i arbejdspladsens uskrevne normer. Han prioriterer fleksibilitet, booker ad hoc og vælger ofte at benytte egen bil da "det er nemmere". Han frustation er, at det er uklart om en booking rent faktisk er registreret og det tager derfor for lang tid at booke. Derfor er systemets vigtigste egenskab hastighed og forsikring. Han skal hurigt kunne se hvad der er ledigt lige nu og være helt sikker på at bookingen er gået igennem - ellers er det nemmere at tage egen bil. <personachristian>

(Indsæt persona Malene)
Malene er erfaren og principfast. Hun bruger aldrig egen bil efter overgangen til lav takst og planlægger derfor sine bookinger i god tid. Hendes frustation er, at det manglende overblik gør dagligdagen uforudsigelig når man ikke kan regne med sine bookinger. Hun vil hellere aflyse et borgerbesøg fremfor at gå på kompromis med sine principper. For Malene er systemets vigtigste egenskaber derfor overblik og pålidelighed.

Tilsæmmen spænder disse to personaer således over domænets yderpunkter ad-hoc og planlæggeren. Fælles for dem er overblikket over ledige transportmidler, hurtigere booking process og forsikring om at bookingen er gået igennem. For at indfri begge personaers behov skulle et kommende system således tydeligt vise tilgængelighed og reducere usikkerheden omkring bookingstatus.

Vi udvidede desuden persona-arbejdet med behov og outcomes for, at skabe en mere direkte overgang til kravspecifikation og tilsikre at vores krav var holdt opimod et konkret behov og samtidig 'målbart' for os om vi rent faktisk indfriede det.
Fælles outcomes:
- Mere tid og energi til kerneydelser.
- Gøre det hurtigere at foretage en booking
- Øge tillid til sine bookinger.
Behovet blev tilføjet til personaerne og outcomes blev holdt i analyse noterne til senere brug.
