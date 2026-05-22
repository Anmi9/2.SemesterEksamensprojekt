#import "template.typ": project

// --- KONFIGURATION AF FORMALIA ---
#let anslag = 0 // Ændres manuelt når rapporten er færdig

// Anvender templaten på hele dokumentet
#show: project.with(
  title: "Systemudvikling og\nsoftwarearkitektur",
  authors: ("Lasse Agerskov", "Anna Vognstoft", "Matias Heiberg"),
  date: "Juni 2026",
  toc-target: heading.where(level: 1).or(heading.where(level: 2).before(<body-end>)),
  anslag: anslag,
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

= Bilag <body-end>
// Indsæt bilag her

