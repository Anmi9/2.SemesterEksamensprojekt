Elaboration 2 repræsenterer et skift fra analyse og modelering til egentlig implementering. Vi havde forud for denne iteration udført enormt meget inceptionarbejde, og planlægningen af iterationen var derfor præget af 'sense of urgency' i forhold til vores deadline for opgaven. Derfor blev der truffet en beslutning om et fravalg af et decideret planlægningsartefakt og derimod prøve at oversætte den akkumulerede viden til kode, og løbende reflektere over om de antagelser vi byggede videre på holdt.

Vi fravalgte at udarbejde et klassisk iterationsplan-dokument med opgaveliste, ansvarsfordeling og tidsestimater. Vores beslutning tog udgangspunkt i vores tidligere erfaring med iterationsplaner og tidsestimering og vores erkendelse af, at tidsestimater kun er meningsfulde hvis estimaterne er pålidelige. Vores evne til at estimere softwareudviklings-scopet på dette tidspunkt af vores uddannelse og er udelukkende baseret på intuition fremfor erfaring. Altså ville udførelsen af sådan et dokument primært bestå i en ekstra arbejdsbyrde med sideløbende tilsyn frem for et artifakt vi kunne støtte os opad. (SE BILAG)

I stedet definerede vi et klart 'iterationsmål', altså hvad iterationen skulle resultere i, formuleret som en enkelt sætning:

#quote(
  block: true,
  [_Implementering af Must-have-kravet 'Opret booking' i overensstemmelse med de definerede forretningsregler_ (#link(<uc1>, [se UC1])).],
)

Det var en måde at udskifte process kompleksitet med et konkret og verificerbar krav der var sammenfaldende med vores eksamensopgavekrav og derfor af høj risiko..

Det var endvidere en pragmatisk beslutning: Vi forventede ikke at nå målet fuldt ud inden for iterationen. Iterationslængden på 3,5 dage var derfor ikke et estimat for, hvornår "Opret booking" ville stå færdig, men en bevidst timeboksning, der skulle tvinge os til et naturligt refleksionsstop. Ved iterationens udløb kunne vi træde et skridt tilbage og vurdere, om vi stadig var inden for scope, og om vores forretningsantagelser holdt - frem for at arbejde videre i blinde. Fordi "Opret booking" var en grundsten for både produktet og opgaven og under alle omstændigheder skulle færdiggøres, var overflow til næste iteration en forventet og accepteret konsekvens.

Vi besluttede at lave et sekvensdiagram for at "aligne" vores mentale modeller og kom igang med implementeringen.
