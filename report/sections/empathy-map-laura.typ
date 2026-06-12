`Note fra Obsidian
created: 2026-05-07
section: inception`

#set page(
  paper: "a4",
  flipped: true,
  margin: (x: 1.5cm, y: 1cm),
)
#set text(
  font: "Liberation Sans",
  size: 8.5pt,
  fill: rgb("#2c3e50"),
)

#let quadrant(title, items, accent-color) = block(
  width: 100%,
  height: 100%,
  inset: 12pt,
  radius: 4pt,
  stroke: 0.5pt + rgb("#e0e0e0"),
  fill: rgb("#fefefe"),
  [
    #set par(leading: 0.45em)
    #stack(
      spacing: 8pt,
      text(weight: "bold", size: 12pt, fill: accent-color, title),
      line(length: 100%, stroke: 0.5pt + rgb("#f0f0f0")),
      list(
        ..items,
        marker: text(fill: accent-color, "•"),
        body-indent: 4pt,
      ),
    )
  ],
)

#grid(
  columns: (1.3fr, 1fr),
  rows: (1fr, 1fr),
  gutter: 12pt,

  quadrant(
    "Siger",
    (
      [Bookingerne er lidt et ømt punkt (for nogen)],
      [Hvordan skal jeg lige navigere i det her (booking)],
      [Logistikken gør at man tager sin egen bil],
      [Kamilla opfordrer til at vi skal tage tjenestebilerne men ikke at vi skal],
      [\(outlook\) er langsomt til at registrere (booke) Til det svarer hun, så tror jeg bare jeg tager min egen bil],
      [Jeg tager cyklen, det er et helvede med bilen],
      [Der kan godt være run på bilerne tirsdag da alle skal møde ind],
      [Det virker ikke. Kan du se om bilen er booket],
      [Nogen gange kommer folk til at tage en forkert bil],
      [Jeg kan ikke ændrer i andres bookinger eller slette dem ved sygdom eller aflysninger],
      [Jeg ved internt at jeg kan tage en booket bil hvis vedkommende er syg],
      [puljebiler: det er ikke noget vi bruger, jeg kender det ikke, jeg ved faktisk ikke hvor de står],
      [Det er lidt et sjus-agtigt emne med bilerne],
      [Planlægning er svært, der ligger meget i det før jeg bare kan køre. Fleksibiliteten er fed men det er nogen gange svært at få til at gå op.],
      [Jeg blev nød til at have et spreadsheet for at have overblik over min borgerliste (i starten)],
      [Vi (pædagogerne) er meget forskellige og har forskellige arbejdsdage],
      [Man må ikke booke en bil hele dagen, men det er der nogen der gør],
      [Outlook er langsom til at registrere en bookning så de beder andre om at bekræfte ens bookning hvis de har travlt med at komme ud af døren],
      [så de beder andre om at booke for sig],
      [Kun oplevet få gange hvor folk står og ikke har nogen ledige biler],
      [Med tjenestebil kan man ikke blive 10 minutter længere, hvis borgeren har brug for det, fordi der er en bagkant på den.],
    ),
    rgb("#2980b9"),
  ),

  quadrant(
    "Tænker",
    (
      [Egen bil skaber fleksibilitet],
      [Har jeg booket korrekt],
      [Hvad er normerne for booking <fd0e16>],
      [Planlægningen er svær],
      [Fleksibilitet er fedt (personligt og for borgerydelsen)],
      [At hun vil undgå stress],
      [Hun vil ikke tage en bil fra andre],
      [Tænker at outlook fungerer okay],
      [Hun skal tilpasse sig og give plads til dem med mere anciennitet],
    ),
    rgb("#16a085"),
  ),

  quadrant(
    "Føler",
    (
      [Det er hårdt at køre borgere i sjam-sjam-sjam (mange i træk)],
      [Føler stress over outlook],
      [Bliver usikker over vage normer <7886a0>],
      [Har berøringsangst med booking <5b69aa>],
    ),
    rgb("#e67e22"),
  ),

  quadrant(
    "Gør",
    (
      [Derfor tager hun sin egen bil <91c328>],
      [Få hjælp til at udføre booke <5494bf>],
      [Beder om andre skal bekræfte sine bookinger <91100a>],
    ),
    rgb("#8e44ad"),
  ),
)
