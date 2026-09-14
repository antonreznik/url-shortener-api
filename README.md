# URL Shortener API

An ASP.NET Core Minimal API that creates compact URLs and redirects short codes to their original URLs.

## What It Does

- Accepts a URL and returns a generated short URL.
- Uses a SQL Server sequence to allocate unique numeric IDs.
- Encodes those IDs as Base62 short codes.
- Stores URL mappings in SQL Server.
- Caches short-code lookups in Redis for faster redirects.
- Returns an HTTP redirect when a short code exists and `404 Not Found` otherwise.

## API

### Create a short URL

```http
POST /shorten
Content-Type: application/json

{
  "originalUrl": "https://example.com/a-long-url"
}
```

Response:

```json
{
  "shortUrl": "https://localhost:7290/2Bi"
}
```

The `originalUrl` value must be a valid absolute URL. Invalid input returns a validation problem response.

### Redirect to the original URL

```http
GET /{shortCode}
```

An existing short code returns a redirect to the original URL. An unknown short code returns `404 Not Found`.

## Architecture

The solution follows a small layered architecture:

```text
UrlShortener (API)
  -> Application (use cases, handlers, and interfaces)
        -> Domain (UrlShortenerModel and Base62 generation)
  -> Infrastructure (EF Core, SQL Server repository, Redis cache, migrations)
```

### API layer

`UrlShortener/` hosts the ASP.NET Core application and Minimal API endpoint mappings. It configures HTTPS redirection, OpenAPI in development, SQL Server, Redis, and dependency injection.

### Application layer

`Application/` contains the shorten and resolve use cases. Handlers depend on repository and cache interfaces, keeping the use-case logic independent of SQL Server and Redis implementations.

### Domain layer

`Domain/` contains `UrlShortenerModel`. A SQL sequence value is converted into a Base62 short code, and the model records the creation time in UTC.

### Infrastructure layer

`Infrastructure/` contains the EF Core `DbContext`, SQL Server repository, Redis cache implementation, dependency registrations, entity configuration, and migrations. Original URLs have a unique constraint, and short codes have a unique index.

### Caching

Redis is checked before SQL Server when resolving a short code. Successful lookups are cached for 24 hours by default. Newly created mappings are also written to the cache.

## Requirements

- .NET SDK 10.0
- SQL Server 2022 or compatible SQL Server instance
- Redis 6 or compatible Redis instance

The application currently targets `net10.0` and uses Entity Framework Core 10.0.12.

## Configuration

Edit `url-shortener/UrlShortener/appsettings.json`, or provide the values through environment variables or another configuration source:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=TestDatabase;User Id=sa;Password=YourStrongPassword123!;TrustServerCertificate=True;"
  },
  "Redis": {
    "Configuration": "localhost:6379",
    "InstanceName": "UrlShortener:"
  }
}
```

Do not commit real database credentials. The connection string in the checked-in settings is a local development placeholder and should be replaced for your environment.

## Run Locally

Start SQL Server and Redis, then apply the existing EF Core migrations:

```powershell
dotnet ef database update --project url-shortener/Infrastructure/Infrastructure.csproj --startup-project url-shortener/UrlShortener/UrlShortener.csproj
```

Run the API from the repository root:

```powershell
dotnet run --project url-shortener/UrlShortener/UrlShortener.csproj
```

The configured development URLs are:

- `http://localhost:5169`
- `https://localhost:7290`

OpenAPI is mapped in the Development environment. With the default HTTPS profile, the OpenAPI document is available at `/openapi/v1.json`.

## Build

```powershell
dotnet build url-shortener/UrlShortener/UrlShortener.csproj
```

There is currently no test project in the repository.

## Project Layout

```text
url-shortener/
├── Application/       Use cases, handlers, and abstractions
├── Domain/            URL shortening domain model and Base62 encoding
├── Infrastructure/    EF Core context, SQL repository, Redis cache, migrations
└── UrlShortener/      ASP.NET Core host and Minimal API endpoints
```
