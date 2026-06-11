// false = inkluder bilag | true = ekskluder bilag
#let skjul-bilagsord = false

// Opsætning: Gør level 2 overskrifter til "Bilag X" format
#set heading(numbering: (..nums) => {
  let vals = nums.pos()
  if vals.len() == 2 {
    return "Bilag " + numbering("A", vals.at(1))
  }
})

// === C. INDSÆT DINE BILAG HERUNDER ===

#pagebreak()
#metadata("Appendix numbering start") <appendix-pages-start>
= Arbejdet med ER-diagram <bilag:er-diagram>
#{
  set heading(offset: 2)
  if not skjul-bilagsord {
    include "../sections/IdentificeredeEntiteter.typ"
  }
}

#pagebreak()
= Planlægning af E2 <bilag:e2-planlægning>
#{
  set heading(offset: 2)
  if not skjul-bilagsord {
    include "../sections/PlanlægningE2.typ"
  }
}

#pagebreak()
= Interview pædagog A - nedslagsnoter 1 <bilag:interview-laura-1>
#{
  set heading(offset: 2)
  if not skjul-bilagsord {
    include "../sections/Laura-interview-1-Matias-noter.typ"
  }
}

#pagebreak()
= Interview pædagog A - nedslagsnoter 2 <bilag:interview-laura-2>
#{
  set heading(offset: 2)
  if not skjul-bilagsord {
    include "../sections/Laura-interview-1-Anna-noter.typ"
  }
}

#pagebreak()
= Interview pædagog B - nedslagsnoter 1 <bilag:interview-brian-1>
#{
  set heading(offset: 2)
  if not skjul-bilagsord {
    include "../sections/Brian-interview-Annas-noter.typ"
  }
}

#pagebreak()
= Interview pædagog B - nedslagsnoter 2 <bilag:interview-brian-2>
#{
  set heading(offset: 2)
  if not skjul-bilagsord {
    include "../sections/Brian-interview-Matias-noter.typ"
  }
}
<bilag-end>
