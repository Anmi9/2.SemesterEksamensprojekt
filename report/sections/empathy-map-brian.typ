`Note fra Obsidian
created: 2026-05-07
section: inception`

#set page(paper: "a4", flipped: true, margin: 1cm)
#set text(font: "Liberation Sans", size: 10pt)

#let quadrant(title, items) = block(
  width: 100%,
  height: 100%,
  inset: 15pt,
  radius: 4pt,
  stroke: 0.5pt + rgb("#b0b0b0"),
  fill: rgb("#fcfcfc"),
  stack(
    spacing: 10pt,
    text(weight: "bold", size: 14pt, fill: rgb("#2c3e50"), title),
    line(length: 100%, stroke: 0.5pt + rgb("#e0e0e0")),
    list(..items, marker: text(fill: rgb("#3498db"), "•")),
  ),
)

#stack(
  spacing: 15pt,

  // Øverste række tilpasser sig dynamisk højden af "Siger"
  grid(
    columns: (1fr, 1fr),
    rows: auto,
    gutter: 15pt,

    quadrant("Siger", (
      [Der sker uforudsete ting],
      [\(jeg\) kan godt lide at være ude i god tid],
      [Jeg har struktureret mit arbejde så jeg har tid hver anden fredag til at lave dokumentation],
      [Jeg er meget principfast - jeg vil ikke bruge egen bil på lav takst],
      [I Outlook vises bookingerne oven i hinanden, der mister jeg overblikket],
      [Vi bliver et større team så der kommer et større press på bilerne],
      [Jeg cykler, det skal i også lige have med],
      [Der er tit nogen der booker længere end de har brug for],
      [Det sker ind imellem at jeg må skrive ud til kollegaer om en bil på grund af akut bogerkørsel],
      [Bogerkørsel trumfer borgerbesøg],
      [Der var et par dage hvor alle skulle have skiftet dæk, der var ingen info omkring hvilke biler og hvornår],
      [Stress forkommer fordi der er nogen der booker meget forud],
      [Det kan godt fylde det med bilerne],
      [Jeg bruger meget iPad],
      [Det handler om at kende sine borger godt, erfaringer giver bedre mulighed for planlægning og derfor mindre stress],
      [Man skal til at booke i et andet system, man skal til at hente den, nøglen er et andet sted. Der er en mental barriere i det.],
      [Møder altid ind på arbejdspladsen om morgenen],
      [Kunne godt bruge en opkvalificering i IT værktøjer til at kunne visualisere energi diagrammer],
    )),

    quadrant("Tænker", (
      [Han har ret til at holde på/handle efter sine rettigheder],
      [Hans adfærd skaber problemer for hele holdet, men det er ikke hans ansvar],
      [Borgerkørsel trumfer borgerbesøg, derfor er jeg i min gode ret til at overtage en bil fra en kollega selvom det besværliggøre deres dag],
    )),
  ),

  // Nederste række fylder det resterende vertikale niveau ud
  grid(
    columns: (1fr, 1fr),
    rows: 1fr,
    gutter: 15pt,

    quadrant("Føler", (
      [Føler at ad hoc opgaver er årsagen bag hans arbejdsrelateret stress],
      [Er indigneret over at få fjernet den høje takst],
      [Faglig stolthed og sine kompetencer],
      [Tager de svære/konfliktfyldte beslutninger som de andre ikke gør],
    )),

    quadrant("Gør", (
      [Tager aldrig egen bil],
      [Stille protest ved at presse systemet og aldrig tager egen bil],
      [Skriver ud når han mangler en bil],
      [Har tidligere brugt botilbudets bil],
    )),
  ),
)
