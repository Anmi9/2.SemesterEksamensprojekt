---
created: 2026-05-12
section: elaboration 1
exclude: false
sortKey: 6.39802
---

[[Forløbig plan]]
[[For at kunne modellere databasen skal vi have styr på vores forretningsregler]]


# Entiteter
- Transportmidler
	- Cykler
	- Biler
- Pædagoger
- Leder
- Flådestyring
- Borgere
- Booking
- Bookingsystem
- Bopæl
- Bosted
- Bookingstatus
- Borgerliste
- Booking tidsgrænse 
- Påmindelser om 
- sms til borger 

Vi er blevet enige om, at vi kan gå videre til logisk model i modelleringen af database - vi har domæneviden nok

```mermaid
erDiagram
    TRANSPORTMIDDEL{
    int id PK
    }
    
    CYKEL{
    int id PK, FK
     
    }
    
    BIL{
    int id PK, FK
    
    }
    
    MEDARBEJDER{
    int id PK
    
    }
    
    BOOKING{
    int id PK
    }
    
```

