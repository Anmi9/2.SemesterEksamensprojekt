Ud fra en fælles oplevelse af at have brugt rigtig meget tid på domæneanalyse og kravspecifikation blev vi enige om, at domænemodellen som værktøj ikke længere ville tilføje ekstra til vores forståelse. Vi vurderede, at vi havde fået os en rigtig god domæneindsigt i vores indledende arbejde, der gjorde det muligt for os at gå direkte i gang med en konceptuel model over vores database - vi prioriterede tid. Med denne model ville vi kunne få et overblik over de regler, der gælder for vores systems data, og hvordan de hænger sammen med hinanden. Denne proces blev sværere end forventet, hvorfor vi måtte stoppe op og i stedet skrive en use case for vores mest centrale must-krav: "Opret booking" for at strømline vores tanker om implementeringen.

#align(center)[
  #quote(block: true, align(center)[
    #text(size: 12pt, weight: "bold", fill: rgb("#444444"))[UC1: Opret booking]
  ])<uc1>

  #quote(
    block: true,
    [
      #align(left)[
        *Pre-condition*
        #quote(
          block: true,
          [
            Medarbejderen er logget ind \
            På internettet \
          ],
        )
      ]

      #align(left)[
        *Primær aktør*
        #quote(
          block: true,
          [
            Pædagog fra autismeteamet \
          ],
        )
      ]

      #align(left)[
        *Success kriterie*
        #quote(
          block: true,
          [
            Medarbejder har registreret en tidsbestemt booking af et ledigt køretøj af en konkret type \
          ],
        )
      ]

      #align(left)[
        *Success scenarie*
        #quote(
          block: true,
          [
            Ser oversigt \
            Vælger start tid \
            Vælger slut tid \
            Vælger køretøjstype \
            Systemet skal vælge det optimale transportmiddel [Usecase til algoritme] \
            Booking bliver persisteret [Usecase til tråde] \
            Bekræfter om bookingen er gået igennem \
          ],
        )
      ]
    ],
  )
]

I den proces gik det op for os, at vi trods meget domænearbejde alligevel ikke var helt enige om det videre arbejde. Det fik os til at træde et skridt tilbage og stille spørgsmålet: "Hvad er formålet egentlig?" Som dokumentation skrev vi vores tanker ned, hvilket retrospektivt fungerede som vores de facto første problemformulering (se version 2 i afsnit @e2:problemformulering-v2):

_Vi har fokus på at optimere medarbejdernes arbejdsdag med et nytænkt bookingsystem - ikke optimering med fokus på økonomi eller besparelser, men på at minimere medarbejdernes mentale belastning, når det kommer til planlægning og koordinering. Det resulterer i mindre stress og frustrationer hos medarbejderne og skaber mere ansigt til ansigt for borgerne som afledt effekt._ <link:problemformulering-v1>

Selvom use case-arbejdet hjalp os til at forstå, hvordan systemet skulle fungere, manglede vi stadig afklaring for at kunne strukturere dataen optimalt. Derfor formulerede vi en række forretningsregler med udgangspunkt i vores must-krav og problemformulering.

#align(center)[
  #quote(
    block: true,
    [#align(left)[
        *Forretningsregler*
        #quote(
          block: true,
          [
            En medarbejder skal kunne oprette en booking af et transportmiddel \
            En medarbejder skal kunne vælge en cykel eller bil i forbindelse med en booking \
            En medarbejder skal kunne reservere et bestemt slags køretøj i en bestemt tidsperiode \
            En medarbejder skal kunne se ledige tidsperioder for et bestemt slags køretøj \
            En medarbejder skal kunne få et ledigt transportmiddel med færrest mulige klik \
          ],
        )
      ]
    ],
  )
]


Det endte med at give os den klarhed, vi havde savnet for at kunne modellere vores database hensigtsmæssigt til vores system. Vi tog da en fælles beslutning om at gå direkte videre med at modellere en logisk model ud fra de entiteter, vi havde identificeret i det indledende arbejde.

Den logiske model blev udviklet i etaper, hvor vi løbende tog nogle strategiske beslutninger. I første version identificerede vi de mest centrale entiteter og nøgler.

#align(center)[#image("../assets/logiskdiagram-v1.svg", width: 100%)]

Her diskuterede vi blandt andet nødvendigheden af at have en separat tabel for køretøjer, og hvor meget følsom data der skulle gemmes om brugerne. Her blev den endelige logiske model designet ud fra, hvor vi var i processen, og hvad der gav mest mening i forhold til projektets omfang og formål. Det endte med tre tabeller: Employee, Vehicle og Booking. Medarbejdernes interne bookingsystem havde ikke brug for at gemme følsomme data (GDPR) for brugerne, hvorfor vi kun gemte id og initialer. Samtidig valgte vi at oprette en samlet Vehicle-tabel fremfor at splitte den op i særskilte tabeller.

#align(center)[#image("../assets/logiskdiagram-v2.svg", width: 65%)]

Vi får endvidere defineret kardinaliteterne mellem tabellerne ud fra domænets forretningsregler. En medarbejder kan have alt fra 0 til mange bookinger i systemet, og det samme er tilfældet for køretøjer. Den enkelte bil eller cykel kan være booket 0 til mange gange. Hver specifik booking, der oprettes, skal indeholde præcis en medarbejder og et køretøj. Derfor er der en mange til en relation mellem både Employee og Booking og mellem Vehicle og Booking. Hver tabel har et unikt id, der fungerer som primærnøgle `PK`, samtidig med at booking-tabellen har fremmednøgler `FK` til både Employee og Vehicle.
Vi forsøgte at holde vores model så simpel som muligt på dette stadie - med en mulighed for at udvide den senere, hvis det skulle blive nødvendigt, i takt med at vi blev klogere under implementeringsarbejdet. Det var et bevidst valg om at holde muligheden åben for at skalere op og ikke låse os fast for tidligt.



