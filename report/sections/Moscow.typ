På dette tidspunkt i projektet havde vi brug for at få lidt struktur på vores krav og opdele dem efter, hvad der var vigtigst at prioritere først. En erfaring, vi i et tidligere projekt har gjort os, er, at vi med Moscow-modellen kan lave en klar prioritering, samtidig med at vi forbinder eksamenskravene med de krav, vi har indsamlet og formuleret gennem vores research. Vi har brugt meget tid på at gå til problemdomænet med en åben tilgang, hvilket har gjort inceptionfasen ekstra lang og skabt et behov for at komme hurtigt videre. Her fungerer moscow som et effektivt og simpelt diskussionsværktøj, der hurtigt hjælper os med et prioriteringsoverblik. De forskellige afsnit i modellen (Must, should, could og would) har vi udfyldt, så must-kravene er de strengt nødvendige for at opfylde eksamenskravene og udgør de grundlæggende funktioner i programmet. Should-kravene er dem, der har høj risiko og høj værdiskabelse, could-kravene har middel risiko og værdiskabelse, mens would-kravene har lav risiko og værdiskabelse. Vi har også tilladt os at prioritere internt i hvert afsnit ad hoc, da ikke alle krav under samme kategori nødvendigvis er lige vigtige.

#align(center)[
  #quote(
    block: true,
    [
      #align(left)[
        *Must:*
        #quote(
          block: true,
          [
            Opret booking \
            Database \
            Algoritmer \
            Tråde \
            Vise der er ledige køretøjer lige nu \
          ],
        )
      ]

      #align(left)[
        *Should:* <link:should>
        #quote(
          block: true,
          [
            Se ledige tider \
            Se din booking \
            Booking-bekræftigelse \
            Slette bookinger \
            Redigere booking (F) \
            -Forlæng igangværende booking \
          ],
        )
      ]

      #align(left)[
        *Could:*
        #quote(
          block: true,
          [
            Nem redigering af bookinger (!F) \
            Frigiv bil \
            Se alle bookinger \
            Kalendervisning \
          ],
        )
      ]

      #align(left)[
        *Would:*
        #quote(
          block: true,
          [
            Marker og book \
            Gentag planlagte bookinger \
            Forny eksisterende bookinger \
            Venteliste og notification ved afmelding af bil \
          ],
        )
      ]
    ],
  )
]
