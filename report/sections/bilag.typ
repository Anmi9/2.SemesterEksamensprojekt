// Opsætning: Gør level 2 overskrifter til "Bilag X" format
#set heading(numbering: (..nums) => {
  let vals = nums.pos()
  if vals.len() == 2 {
    // Level 2 (e.g., 8.1) vises som "Bilag A"
    return "Bilag " + numbering("A", vals.at(1))
  }
})

// === C. INDSÆT DINE BILAG HERUNDER ===
// Husk: Brug '==' for titel, og '#pagebreak()' før hver ny.

#pagebreak()
#metadata("Appendix numbering start") <appendix-pages-start>
= Arbejdet med ER-diagram
#{
  set heading(offset: 2)
  include "../sections/IdentificeredeEntiteter.typ"
}

#pagebreak()
= Planlægning af E2
#{
  set heading(offset: 2)
  include "../sections/PlanlægningE2.typ"
}

#pagebreak()
= Interview pædagog A - nedslagsnoter 1
#{
  set heading(offset: 2)
  include "../sections/Laura-interview-1-Matias-noter.typ"
}

#pagebreak()
= Interview pædagog A - nedslagsnoter 2
#{
  set heading(offset: 2)
  include "../sections/Laura-interview-1-Anna-noter.typ"
}

#pagebreak()
= Interview pædagog B - nedslagsnoter 1
#{
  set heading(offset: 2)
  include "../sections/Brian-interview-Annas-noter.typ"
}

#pagebreak()
= Interview pædagog B - nedslagsnoter 2
#{
  set heading(offset: 2)
  include "../sections/Brian-interview-Matias-noter.typ"
}
<bilag-end>


