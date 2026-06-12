#import "template.typ": project
#import "template.typ": author

// --- KONFIGURATION AF FORMALIA ---

#let anslag = 93504  // Ændres manuelt når rapporten er færdig

// Anvender templaten på hele dokumentet
#show: project.with(
  title: "2. Semestereksamen",
  authors: ("Lasse Agerskov", "Anna Vognstoft", "Matias Heiberg"),
  date: "Juni 2026",
  toc-target: heading.where(level: 1).or(heading.where(level: 2).before(<body-end>)),
  anslag: anslag,
)
#metadata("start") <start-formalia>
= Introduktion <indledning>

Dette projekt tager udgangspunkt i et reelt problemdomæne: Bostøtten på Tulipanvej 100 i Silkeborg, hvor pædagoger dagligt besøger borgere og deler en lille flåde af biler og cykler. Den uforudsigelige arbejdsdag kræver fleksibilitet, men det nuværende bookingsystem i Outlook er uoverskueligt. Det skaber en manglende tillid til bookinger og individuelle workarounds. Det ledte os frem til projektets problemformulering:

#quote(
  block: true,
  [Informationsstøj og unødigt mange klik i det nuværende bookingsystem skaber en friktion, der gør det tidskrævende at få overblik over ledige transportmidler i en travl og uforudsigelig arbejdsdag. For at imødekomme det problem skal vi skabe en bookingoplevelse med minimal friktion. _Hvordan udvikler vi et system, der sorterer støjen fra og lynhurtigt eksponerer de ledige transportmidler for brugeren?_],
) <problemformulering2>


Vi har i løbet af hele projektet benyttet generative AI-værktøjer eks. Gemini og ChatGPT som sparringspartner. Vi har benyttet dem til at få feedback og til generere et udkast, vi kunne arbejde ud fra. AI er ikke brugt som kilde til rapportens faglige indhold. Vi har forholdt os kritisk til værktøjernes output. @anthropic2026 @google2026 @openai2026
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

#bibliography("references.bib", title: none)

= Bilag <body-end>
#{
  set heading(offset: 1)
  include "sections/bilag.typ"
}


