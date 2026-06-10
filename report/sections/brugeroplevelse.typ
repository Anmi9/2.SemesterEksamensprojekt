```
Skrivepunkter:
- Tilstand/udgangspunkt: programmet kan allerede oprette en booking i databasen, men dette bekræftes ikke i UI'en, ingen bekræftigelse.
  - Notification løser det for begge booking typer
- UI'et kommunikere ikke om den indtastede information producere en gyldig booking eller ej hvilket strider imod kravet om at reducere antal kliks/tid for at udføre en booking. Se #link(<personachristian>)[persona: Christian]
  - Knapper ændre dynamisk tilstand alt efter om dataen producerer en gyldig booking (gælder også lynbooking)
  - Lynbooking tager det et skridt videre og viser også hvor mange biler der er ledige før brugeren overhoved har fortaget et enkelt klik
```
