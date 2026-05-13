---
created: 2026-05-12
section: elaboration 1
exclude: false
sortKey: 6.46719
---
[[database - konceptuelle model]]

---
# Pre-condition
Medarbejderen er logget ind
På internettet

# Primær aktør
Pædagog fra autismeteamet

# Success kriterie
Medarbejder har registreret en tidsbestemt booking af et ledigt køretøj af en konkret type

# Success scenarie 
- Ser oversigt
- Vælger start tid
- Vælger slut tid
- Vælger køretøj
- Systemet validere at bookingen er konfliktfri 
- Booking bliver persisteret 
- starter UC: bekræftigelse 

Tabel med køretøjer, medarbejdere, 
Køretøjerne skal have en status kolonne
Behov for at transportmidlerne har en unik primær key
Medarbejderne har en unik PK