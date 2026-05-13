---
created: 2026-05-13
section: elaboration 1
exclude: false
sortKey: 7.38641
---

Vi tager nogen beslutninger ang. tabeller. 

Vi holder det simpelt med kun at have vehicle-tabellen frem for bike og car. Vi tager udgangspunkt i hvor lille et projekt, vi arbejder med og vores begrænsede database. Havde det været et rigtigt projekt, skulle vi i en anden grad fremtidssikre ved at lave flere tabeller til flere typer

Derfor gemmer vi også køretøjstypen direkte på vehicle frem for en separat vehicle type tabel. 

Hensigt: Reducer kompleksitet uden at påvirke systemets nuværende behov