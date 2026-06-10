// --- DESIGN PARAMETRE ---
#let body-font = "Roboto"
#let heading-font = "Roboto"
#let code-font = "Fira Code"
#let primary-color = rgb("#262626")
#let bg-color = rgb("#fdfcf7")
#let font-size = 11pt
#let target-width = 30 * font-size
#let author-map = (
  "a": "Anna Vognstoft",
  "l": "Lasse Agerskov",
  "m": "Matias Heiberg",
)

#let author(keyword) = {
  let name = author-map.at(keyword, default: "Ukendt forfatter")

  text(
    weight: "regular",
    size: 0.6em, // Skalerer relativt til den aktuelle overskrift (f.eks. mindre end overskriften)
    style: "italic", // Gør forfatternavnet kursivt for visuel separation
    fill: rgb("#555555"), // Sætter en dæmpet grå farve, uafhængig af overskriftens farve
  )[(#name)]
}

// --- HOVED FUNKTIONEN ---
#let project(
  title: "",
  authors: (),
  date: none,
  toc-target: heading.where(level: 1).or(heading.where(level: 2)), // Default to showing level 1 and 2
  anslag: 0,
  body,
) = {
  // 1. DOKUMENT OPSÆTNING
  set document(author: authors, title: title)
  set text(font: body-font, lang: "da", size: font-size, fill: primary-color)
  set page(
    paper: "a4",
    margin: (x: 3cm, y: 3cm),
    numbering: none, // Ingen sidetal på forside/TOC
    fill: bg-color,
  )

  set bibliography(style: "apa")

  // 2. TEKST FORMATERING
  set par(justify: true, leading: 0.8em)

  // Funktion der begrænser bredden og centrerer indholdet (padding på begge sider)
  let limit-width(it) = layout(size => {
    if size.width > target-width {
      let margin = (size.width - target-width) / 2
      pad(x: margin, it)
    } else {
      it
    }
  })

  // Vi anvender reglen på både tekstafsnit, lister og overskrifter over en bred kam
  show par: limit-width
  show list: limit-width
  show enum: limit-width
  show terms: limit-width
  show bibliography: limit-width
  show quote: limit-width


  // 3. OVERSKRIFTER

  set heading(numbering: "1.1", supplement: none)
  show heading: it => {
    if it.level == 1 {
      pagebreak(weak: true)
    }

    let styled = {
      set text(font: heading-font, weight: "bold", fill: primary-color)
      v(0.5em)
      if it.numbering != none {
        counter(heading).display(it.numbering)
        h(0.3em)
      }
      it.body
      v(0.5em)
    }

    limit-width(block(styled))
  }

  // 4. KODE BLOKKE
  show raw.where(block: true): set raw(theme: "assets/themes/vs-dark.tmTheme")
  show raw.where(block: true): set text(fill: rgb("#d4d4d4"))
  show raw.where(block: true): it => limit-width(
    block(
      fill: rgb("#1e1e1e"),
      stroke: rgb("#3c3c3c"),
      inset: 10pt,
      radius: 4pt,
      width: 100%,
      it,
    ),
  )
  show raw: set text(font: code-font)

  // 5. FORSIDEN
  align(center)[
    #v(3em)
    #image("assets/logo.png", width: 30%)
    #v(2em)
    #text(weight: "bold", size: 24pt, title)
    #v(1em)
    #text(size: 14pt, date)
    #v(2em)

    #grid(
      columns: (1fr,) * calc.min(3, authors.len()),
      gutter: 1em,
      ..authors.map(author => strong(author)),
    )
    #v(2fr)

    // Build date explicitly in Danish to avoid locale differences.
    #let today = datetime.today()
    #let da-months = (
      "januar",
      "februar",
      "marts",
      "april",
      "maj",
      "juni",
      "juli",
      "august",
      "september",
      "oktober",
      "november",
      "december",
    )
    #let today-da = str(today.day()) + ". " + da-months.at(today.month() - 1) + " " + str(today.year())

    // Semester info
    Teknisk Rapport \
    Datamatiker uddannelsen \
    #today-da \
    #link("https://github.com/Anmi9/2.SemesterEksamensprojekt", underline[*GitHub*])

    #v(2em)

    // --- MANUELT FORMALIA FELT ---
    #block(
      fill: luma(250),
      stroke: luma(200),
      inset: 1em,
      radius: 4pt,
      align(left)[
        *Formalia* \
        Antal anslag: #anslag \
        Antal normalsider: #calc.round(anslag / 2400, digits: 1)
      ],
    )
  ]

  pagebreak()

  // 6. INDHOLDSFORTEGNELSE
  limit-width(block(width: 100%, [
    #align(left, text(font: heading-font, weight: "bold", size: 1.2em, fill: primary-color)[Indhold])
    #v(0.5em)
    #outline(
      title: none,
      depth: 2,
      indent: auto,
      target: toc-target,
    )
  ]))
  pagebreak()

  // 7. HOVEDINDHOLD
  // Sidetal: Brødtekst vises som x/y, bilag vises som romertal fra appendix-pages-start.

  set page(
    numbering: (..nums) => {
      let n = nums.pos().first()
      let body_start = query(<body-start>)
      let appendix_pages_start = query(<appendix-pages-start>)

      if body_start.len() == 0 {
        str(n)
      } else {
        let body_start_page = body_start.first().location().page()

        if appendix_pages_start.len() == 0 or n < appendix_pages_start.first().location().page() {
          let body_page = n - body_start_page + 1
          str(body_page)
        } else {
          let app_start_page = appendix_pages_start.first().location().page()
          let app_page = n - app_start_page + 1
          numbering("I", app_page)
        }
      }
    },
    footer: context {
      let n = counter(page).get().first()
      let body_start = query(<body-start>)
      let body_end = query(<body-end>)
      let appendix_pages_start = query(<appendix-pages-start>)

      if body_start.len() == 0 or body_end.len() == 0 {
        align(center, str(n))
      } else {
        let body_start_page = body_start.first().location().page()
        let body_end_page = body_end.first().location().page()
        let total_body_pages = body_end_page - body_start_page + 1

        if appendix_pages_start.len() == 0 or n < appendix_pages_start.first().location().page() {
          let body_page = n - body_start_page + 1
          align(center, str(body_page) + "/" + str(total_body_pages))
        } else {
          let app_start_page = appendix_pages_start.first().location().page()
          let app_page = n - app_start_page + 1
          align(center, numbering("I", app_page))
        }
      }
    },
  )

  // Markør for start af brødtekst (til beregning af sideantal)
  [#metadata("Body Start") <body-start>]

  body
}
