#import "template.typ": project

// Anvender templaten på hele dokumentet
#show: project.with(
  title: "Vores Semesterprojekt",
  authors: ("Fornavn Efternavn", "Fornavn Efternavn", "Fornavn Efternavn"),
  date: "Maj 2026",
)
#metadata("start") <start-formalia>
= Introduktion

= Inception (dato-dato)
#{
  set heading(offset: 1)
  include "sections/inception.typ"
}

= Elaboration 1 (dato-dato)
#{
  set heading(offset: 1)
  include "sections/elaboration 1.typ"
}
= Elaboration 2 (dato-dato)
#{
  set heading(offset: 1)
  include "sections/elaboration 2.typ"
}
= Construction (dato-dato)
#{
  set heading(offset: 1)
  // Inkluder construction fil
}
= Reflektion

= Litteraturliste
#bibliography("references.bib", title: none)

