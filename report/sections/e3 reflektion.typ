I vores projekt har vi orienteret os efter Craig Larmans beskrivelse af Unified Process, der fokuserer på at udvikling sker i vertikale _slices_ @larmanApplyingUMLPatterns, frem for med en traditionel lagdelt tilgang; bottom-up, top-down eller middle-out. Fordelen er at integrations kompleksiteten reduceres og den kognitive belastning minimeres da et slice kun introducere minimalt ny kode til hvert lag. Samtidig forskriver Larman at fordi kode skrives i små scopes ad gangen, skal den skrives i en kvalitet der er produktionsklar fra starten. Dette virkede som den rigtige tilgang for os som studerende men realiteten er at vi undervuderede den kognitive belastning der lå i at (1) integrere på tværs af alle lag samtidig med at (2) skulle lære teknologierne at kende, samtidig med at (3) skrive så god kvalitets kode vi kunne. Resultatet blev at den grundlæggende arkitektur, det første slice, først stod færdig  i denne iteration, elaboration 3, og ikke i elabortaion 1. Forklaringen er at vi har glemt at UP skal tilpasses til hvert enkelt projekt og at vi havde brug for at sænke kompleksitetesviveauet yderligere end hvad der er standard. At integrere alt fra WPF til dependency injection root, til data persistering har været for svært at gøre på en gang. Og det var på trods af at vi valgte at gå på kompromis med kvalitetskravet om produktions klar kode og fokusere på at skrive noget der "bare" kunne kompileres.

Vi har lært at vores tilgang ikke kan følge et framework blindt. Det skal altid oversættes og tilpasses vores udgangspunkt. Ideen om at lave tynde vertikale integrationer var en god start da antallet af nye dependencies der skal håndteres på en gang reduceres. Og fordi det medfører korte integrations-intervaller. Men vi skulle også have begrænset os i højden - altså hvor vidt alle lag er inkluderet i det vertikale slice. Vi kunne have startet med at udelade præsentationslaget, og verificere vores backend med et konsol- eller unit-test projekt. Med denne begrænsning kunne vi bruge et arbejdsflow, som vist i @korregeret-workflow, til at arbejde mere effektivt og fokuseret på vores første slice/_walking skeleton_ fordi vi ville have klart defineret ansvarfordelingen i programmet, med kontrakter (API) mellem hvert lag. Derved kunne vi have undgået _integration hell_ @zotero-item-66.
#figure(
  [
    #align(left, [
      + Use case over vigtigste krav
      + System sekvensdiagram (SSD)
      + Lagdelt sekvensdiagram
      + Klassediagram over lagenes `public` API
    ])
  ],
  caption: [Arbejdsflow],
  kind: "list",
  supplement: [Liste],
) <korregeret-workflow>


