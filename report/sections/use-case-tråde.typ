`Note taget fra Obsidian
created: 2026-05-26
section: elaboration 3`

#align(center)[
  #quote(block: true, align(center)[
    #text(size: 12pt, weight: "bold", fill: rgb("#444444"))[UC2: Persistér booking / Undgå dobbeltbooking]
  ])

  #quote(
    block: true,
    [
      #align(left)[
        *Pre-condition*
        #quote(
          block: true,
          [
            Medarbejderen har valgt et tidsinterval \
            Medarbejderen har valgt en ledig køretøjstype \
            Systemet har valgt et specifikt køretøj \
          ],
        )
      ]

      #align(left)[
        *Primær aktør*
        #quote(
          block: true,
          [
            Systemet \
          ],
        )
      ]

      #align(left)[
        *Success kriterie*
        #quote(
          block: true,
          [
            Undgå dobbeltbooking \
          ],
        )
      ]

      #align(left)[
        *Success scenarie*
        #quote(
          block: true,
          [
            Vores system sender en bookingentitet af sted til databasen \
            Entiteten bliver valideret og persisteres eller afvises \
            Returnerer status #link(<uc1>, [se UC1]) \
          ],
        )
      ]
    ],
  )
]
