---
created: 2026-05-13
section:
exclude: false
sortKey: 7.34613
---

```mermaid
erDiagram
    TRANSPORTMIDDEL{
    int id PK
    }
    
    CYKEL{
    int id PK, FK
    bool status 
    }
    
    BIL{
    int id PK, FK
    bool status
    }
    
    MEDARBEJDER{
    int id PK
    string initialer 
    bool status 
    }
    
    BOOKING{
    int id PK
    
    }
    
```