# TechEd 2025 - Generátor kódu z OAS

> [!NOTE]  
> Níže uvedená dokumentace k projektu je automaticky generovaná službou GitHub Copilot.

TechEd 2025 - Ukázka automatického generování C# Web API z OpenAPI specifikace

## Přehled

Tento projekt demonstruje automatické generování kódu pro ASP.NET Core Web API z OpenAPI (OAS) specifikace. Aplikace se skládá ze dvou hlavních částí:

1. **Codegen** - Konzolová aplikace pro generování C# kódu z OpenAPI specifikace
2. **WebApi** - ASP.NET Core Web API aplikace využívající vygenerovaný kód

## Funkcionalita

Aplikace implementuje systém pro správu workshopů s následujícími funkcemi:

### API Endpointy

- `GET /workshops` - Seznam workshopů s možností filtrování a stránkování
- `POST /workshops` - Vytvoření nového workshopu
- `GET /workshops/{id}` - Detail konkrétního workshopu
- `PUT /workshops/{id}` - Aktualizace workshopu
- `DELETE /workshops/{id}` - Odstranění workshopu
- `GET /registrations` - Seznam registrací na workshopy
- `DELETE /registrations/{id}` - Odstranění registrace

### Automaticky generované komponenty

Z OpenAPI specifikace se automaticky generují:

- **Endpoint handlery** - Implementace HTTP endpointů s příslušnými parametry
- **Kontrakty** - C# třídy pro request/response modely
- **Validátory** - FluentValidation validátory pro vstupní data
- **Routing** - Konfigurace tras s příslušnými omezeními typů

## Struktura projektu

### Codegen projekt

```
Codegen/
├── Program.cs                  # Hlavní vstupní bod generátoru
├── ApiRoutesGenerator.cs       # Generování endpoint handlerů
├── ContractGenerator.cs        # Generování C# tříd pro DTOs
├── ValidatorGenerator.cs       # Generování FluentValidation validátorů
├── Helpers/
│   └── GenHelpers.cs          # Pomocné metody pro generování
├── Dto/                       # Interní DTO třídy
└── workshopy.openapi.json     # OpenAPI specifikace
```

### WebApi projekt

```
WebApi/
├── Program.cs                  # Konfigurace ASP.NET Core aplikace
├── Generated/                  # Vygenerovaný kód
│   ├── ApiRoutes.cs           # Registrace všech API tras
│   ├── Endpoints/             # Jednotlivé endpoint handlery
│   ├── Contracts/             # Request/Response DTOs
│   └── Validators/            # FluentValidation validátory
├── Data/                      # Databázová vrstva (Entity Framework)
├── ErrorHandling/             # Zpracování chyb a výjimek
└── Constraints/               # Vlastní route constraints
```

## Proces generování kódu

1. **Načtení OpenAPI specifikace** - Ze souboru `workshopy.openapi.json`
2. **Analýza operací** - Projití všech definovaných API operací
3. **Generování komponent**:
   - Pro každou operaci se vytvoří endpoint handler
   - Z request schémat se generují C# třídy a validátory
   - Z response schémat se generují výstupní DTOs
4. **Vytvoření registrace** - Automatické propojení všech endpointů

### Ukázka generovaného kódu

#### Endpoint handler (CreateWorkshopApi.cs)
```csharp
public static class CreateWorkshopApi
{
    /// <summary>
    /// Vytvoření workshopu
    /// </summary>
    public static RouteHandlerBuilder CreateWorkshop(this IEndpointRouteBuilder api)
    {
        return api.MapPost("/workshops", async ([FromBody]CreateWorkshopRequest model, 
            [FromServices]ContractValidator validator, [FromServices]AppDbContext db) =>
        {
            validator.EnsureValid(model);
            return Results.Created();
        });
    }
}
```

#### Kontrakt (CreateWorkshopRequest.cs)
```csharp
public partial class CreateWorkshopRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int MaxParticipants { get; set; }
}
```

## Technické požadavky

- .NET 9.0 SDK
- ASP.NET Core
- Entity Framework Core (In-Memory databáze)
- FluentValidation
- Microsoft.OpenApi

## Spuštění aplikace

### 1. Generování kódu

```bash
cd Codegen
dotnet run
```

Tento příkaz přečte OpenAPI specifikaci a vygeneruje všechny potřebné C# soubory do složky `WebApi/Generated/`.

### 2. Spuštění Web API

```bash
cd WebApi
dotnet run
```

API bude dostupné na `https://localhost:5001` nebo `http://localhost:5000`.

## Klíčové funkce generátoru

### Mapování OpenAPI typů na C# typy

Generátor automaticky mapuje OpenAPI datové typy na odpovídající C# typy:

- `string` → `string`
- `integer` → `int`
- `number` → `double`/`decimal`/`float`
- `boolean` → `bool`
- `string(date)` → `DateTime`
- `string(uuid)` → `Guid`

### Route constraints

Automaticky se generují route constraints na základě OpenAPI schémat:

- `{id:int}` pro integer parametry
- `{id:guid}` pro UUID parametry  
- `{id:apid}` pro vlastní formát `^[a-z0-9]{8}$`

### Validace

Pro každý request model se automaticky generuje FluentValidation validátor s pravidly odvozenými z OpenAPI schématu.

## Výhody tohoto přístupu

1. **Konzistence** - API je vždy v souladu s OpenAPI specifikací
2. **Rychlost vývoje** - Automatické generování eliminuje ruční kódování
3. **Údržba** - Změny v API se provedou pouze v OpenAPI specifikaci
4. **Dokumentace** - OpenAPI specifikace slouží jako živá dokumentace
5. **Validace** - Automatické generování validačních pravidel

## Rozšíření

Generátor lze snadno rozšířit o další funkce:

- Generování dokumentace
- Generování klientských SDK
- Podpora dalších HTTP operací
- Pokročilá validační pravidla
- Generování testů

---

Tento projekt demonstruje moderní přístup k vývoju REST API pomocí OpenAPI specifikace a automatického generování kódu, což je klíčové téma pro efektivní vývoj API aplikací.
