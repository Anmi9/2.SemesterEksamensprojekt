// false = inkluder bilag | true = ekskluder bilag
#let skjul-bilagsord = false

// Opsætning: Gør level 2 overskrifter til "Bilag X" format
#set heading(numbering: (..nums) => {
  let vals = nums.pos()
  if vals.len() == 2 {
    return "Bilag " + numbering("A", vals.at(1))
  }
})


#pagebreak()
#metadata("Appendix numbering start") <appendix-pages-start>

#if not skjul-bilagsord [
  = Arbejdet med ER-diagram <bilag:er-diagram>
  #{
    set heading(offset: 2)
    include "../sections/IdentificeredeEntiteter.typ"
  }
] else [
  #heading(level: 1, numbering: (..nums) => [], supplement: [], outlined: false, "") <bilag:er-diagram>
]

#pagebreak()
#if not skjul-bilagsord [
  = Planlægning af E2 <bilag:e2-planlægning>
  #{
    set heading(offset: 2)
    include "../sections/PlanlægningE2.typ"
  }
] else [
  #heading(level: 1, numbering: (..nums) => [], supplement: [], outlined: false, "") <bilag:e2-planlægning>
]

#pagebreak()
#if not skjul-bilagsord [
  = Interview pædagog A - nedslagsnoter 1 <bilag:interview-laura-1>
  #{
    set heading(offset: 2)
    include "../sections/Laura-interview-1-Matias-noter.typ"
  }
] else [
  #heading(level: 1, numbering: (..nums) => [], supplement: [], outlined: false, "") <bilag:interview-laura-1>
]

#pagebreak()
#if not skjul-bilagsord [
  = Interview pædagog A - nedslagsnoter 2 <bilag:interview-laura-2>
  #{
    set heading(offset: 2)
    include "../sections/Laura-interview-1-Anna-noter.typ"
  }
] else [
  #heading(level: 1, numbering: (..nums) => [], supplement: [], outlined: false, "") <bilag:interview-laura-2>
]

#pagebreak()
#if not skjul-bilagsord [
  = Interview pædagog B - nedslagsnoter 1 <bilag:interview-brian-1>
  #{
    set heading(offset: 2)
    include "../sections/Brian-interview-Annas-noter.typ"
  }
] else [
  #heading(level: 1, numbering: (..nums) => [], supplement: [], outlined: false, "") <bilag:interview-brian-1>
]

#pagebreak()
#if not skjul-bilagsord [
  = Interview pædagog B - nedslagsnoter 2 <bilag:interview-brian-2>
  #{
    set heading(offset: 2)
    include "../sections/Brian-interview-Matias-noter.typ"
  }
] else [
  #heading(level: 1, numbering: (..nums) => [], supplement: [], outlined: false, "") <bilag:interview-brian-2>
]

#pagebreak()
#if not skjul-bilagsord [
  = Klassediagram version 1 <bilag:klassediagram-v1>
  #{
    page(
      width: auto,
      height: 90em,
      flipped: true,
      margin: 0pt,
      image("../assets/klassediagram-v1.svg"),
    )
  }
] else [
  #heading(level: 1, numbering: (..nums) => [], supplement: [], outlined: false, "") <bilag:klassediagram-v1>
]

#pagebreak()
#if not skjul-bilagsord [
  = Value propersition canvas - customer segment <bilag:vpc-customer-segment>
  #{
    page(
      width: auto,
      height: 90em,
      flipped: true,
      margin: 0pt,
      image("../assets/value-propertion-canvas.png"),
    )
  }
] else [
  #heading(level: 1, numbering: (..nums) => [], supplement: [], outlined: false, "") <bilag:vpc-customer-segment>
]

#pagebreak()
#if not skjul-bilagsord [
  = Empathy map - Laura <bilag:empathy-map-laura>
  #{
    set heading(offset: 2)
    include "../sections/empathy-map-laura.typ"
  }
] else [
  #heading(level: 1, numbering: (..nums) => [], supplement: [], outlined: false, "") <bilag:empathy-map-laura>
]

#pagebreak()
#if not skjul-bilagsord [
  = Empathy map - Laura <bilag:empathy-map-brian>
  #{
    set heading(offset: 2)
    include "../sections/empathy-map-brian.typ"
  }
] else [
  #heading(level: 1, numbering: (..nums) => [], supplement: [], outlined: false, "") <bilag:empathy-map-brian>
]

<bilag-end>

