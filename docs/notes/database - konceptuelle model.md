---
created: 2026-05-12
section: elaboration 1
exclude: false
sortKey: 6.39802
---

[[Forløbig plan]]

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

