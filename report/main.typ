#import "template.typ": project
#import "template.typ": author

// --- KONFIGURATION AF FORMALIA ---

#let anslag = 94151  // Ændres manuelt når rapporten er færdig

// Anvender templaten på hele dokumentet
#show: project.with(
  title: "2. Semestereksamen",
  authors: ("Lasse Agerskov", "Anna Vognstoft", "Matias Heiberg"),
  date: "Juni 2026",
  toc-target: heading.where(level: 1).or(heading.where(level: 2).before(<body-end>)),
  anslag: anslag,
)
#metadata("start") <start-formalia>
= Introduktion
Vi har i løbet af hele projektet benyttet generative AI-værktøjer eks. Gemini og ChatGPT som sparringspartner. Vi har benyttet dem til at få feedback og til genere et udkast vi kunne arbejde ud fra. AI er ikke brugt som kilde til rapportens faglige indhold. Vi har forholdt os kritisk til værktøjernes output.
= Inception   #author("l", "a")
#{
  set heading(offset: 1)
  include "sections/inception.typ"
}

= Elaboration 1   #author("a")
#{
  set heading(offset: 1)
  include "sections/elaboration 1.typ"
}
= Elaboration 2   #author("l", "a", "m")
#{
  set heading(offset: 1)
  include "sections/elaboration 2.typ"
}
= Elaboration 3   #author("a", "l")<e3>
#{
  set heading(offset: 1)
  include "sections/elaboration 3.typ"
}
= Construction   #author("m", "l", "a")
#{
  set heading(offset: 1)
  include "sections/construction.typ"
}
= Konklusion
#{
  set heading(offset: 1)
  include "sections/SlutKonklusion.typ"
}
= Perspektivering
#{
  set heading(offset: 1)
  include "sections/perspektivering.typ"
}

= Litteraturliste

ChatGPT 5.0, OpenAI

Opus 4.6, Claude Antrophics

Gemini 3.5 Flash, Google Gemini.

#bibliography("references.bib", title: none)

= Bilag <body-end>
#{
  set heading(offset: 1)
  include "sections/bilag.typ"
}


