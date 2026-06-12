`Note fra Obsidian
created: 2026-05-26
section: elaboration 3`

#align(center)[
  #quote(block: true, align(center)[
    #text(size: 12pt, weight: "bold", fill: rgb("#444444"))[UC3: Vælg optimalt køretøj]
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
            Der bliver booket det mest optimale køretøj til det specifikke tidsinterval, så der er plads til flest mulige bookinger \
          ],
        )
      ]

      #align(left)[
        *Success scenarie*
        #quote(
          block: true,
          [
            Få listen af alle ledige køretøjer, der matcher typen. \
            Få liste af alle ledige tidsperioder for hvert køretøj \
            Vælg den korteste tid, der matcher det valgte tidsinterval \
            Det tilhørende køretøj persisteres i #link(<uc1>, [se UC1]) \
          ],
        )
      ]
    ],
  )
]
