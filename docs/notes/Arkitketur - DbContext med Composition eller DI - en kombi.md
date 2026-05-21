---
created: 2026-05-21
section:
exclude: false
sortKey: 15.33036
---

> [!Question] Prompt
> Er det ikke mindre komplekst at have et objekt per process og bruge DI frem for at bruge komposition til at oprette en masse kortlivede instanser?

> [!info] Reply
> Det lyder umiddelbart simplere at have ét globalt objekt ("Singleton"), men i praksis med `DbContext` er det faktisk det modsatte. Ved at bruge ét objekt per proces (f.eks. i en lille Console app eller en baggrundsservice) bytter du **strukturel kompleksitet** ud med **runtime kompleksitet**, som er langt sværere at debugge.
> 
> Her er de tre største grunde til, at "én instans per proces" næsten altid ender i problemer:
> 
> ### 1. Change Tracker "Forgiftning"
> 
> Dette er den største fælde. `DbContext` husker alt, hvad den rører ved.
> 
> - **Problemet:** Hvis din proces kører i 24 timer, og du løbende henter data, vil din `DbContext` i slutningen af dagen tracke tusindvis af objekter.
>     
> - **Konsekvens:** Hver gang du kalder `.SaveChanges()`, skal EF Core løbe samtlige tracked objekter igennem for at se, om de er ændret. Din app bliver langsommere og langsommere, og hukommelsesforbruget stiger støt (memory leak).
>     
> 
> ### 2. Trådsikkerhed (Concurrency)
> 
> `DbContext` er **ikke trådsikker**.
> 
> - Hvis du har to opgaver, der kører samtidigt (f.eks. to forespørgsler eller to baggrundstråde) og bruger den samme instans, vil appen crashe med en `InvalidOperationException`.
>     
> - Hvis du bruger én instans per proces, tvinger du din app til at være strengt seriel, eller også skal du selv implementere et komplekst låse-system (`lock`) omkring din database-adgang.
>     
> 
> ### 3. State-inkonsistens
> 
> Hvis en lagring fejler (f.eks. en `DbUpdateException`), bliver den pågældende instans af `DbContext` ofte ustabil. Den holder fast i de ændringer, der fejlede. Hvis du genbruger den samme instans til den næste opgave, vil den prøve at gemme de "gamle" fejlbehæftede data igen.
> 
> ---
> 
> ## Den moderne løsning: DI uden "manuel" komposition
> 
> Du behøver ikke vælge mellem DI og komposition (manuel `new`). Du kan få det bedste fra begge verdener med **`IDbContextFactory`**. Det er indbygget i .NET og er designet specifikt til dette scenarie.
> 
> I stedet for at injicere selve din `DbContext`, injicerer du en fabrik:
> 
> C#
> 
> ```csharp
> // Program.cs
> builder.Services.AddPooledDbContextFactory<MyDbContext>(...);
> 
> // Din Service
> public class MyBackgroundWorker
> {
>     private readonly IDbContextFactory<MyDbContext> _factory;
> 
>     public MyBackgroundWorker(IDbContextFactory<MyDbContext> factory)
>     {
>         _factory = factory;
>     }
> 
>     public void DoWork()
>     {
>         // Du bruger stadig DI, men du styrer selv 'levetiden' helt simpelt
>         using var context = _factory.CreateDbContext();
>         var user = context.Users.First();
>         // ... gør arbejdet ...
>     } // Her dør instansen, forbindelsen renses, og alt er sikkert.
> }
> ```
> 
> ### Hvorfor dette er MINDRE komplekst:
> 
> 1. **Ingen Memory Leaks:** Hver `using`-blok starter med en tom, hurtig `ChangeTracker`.
>     
> 2. **Trådsikkerhed:** Hver tråd kan kalde `.CreateDbContext()` og få sin egen private instans.
>     
> 3. **Ren DI:** Du bruger stadig Dependency Injection, så din kode er let at teste (Mocking).
>     
> 
> > **Konklusion:** At have ét objekt per proces virker lettest på papiret, men det fører til "skygge-bugs", som er utroligt svære at finde. I .NET er den "simple" vej i dag at bruge **Scoped** (i Web API) eller **Factory** (i alt andet).