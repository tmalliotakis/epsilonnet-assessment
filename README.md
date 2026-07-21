# Description

1. 	You are given a solution that contains a Blazor Web App with a Customer model class.

	You should download this project create your own repository (public or private) and provide a github link for your solution or an invitation in case of a private repository.

	You have to develop 

	Required: 
	- A grid with all customers with paging
	- CRUD Operations on “Customer” model with new, edit and delete functionalities
	- Expose all CRUD Operations as an API 
	- Configure application to use Sql Server
	- Manage migrations
	- Add Cookie authentication for the client  
	- Protect your API with JWT authentication
	
	Nice to have :
	- Blazor UI framework
	- Unit & Integration Tests
2. Below are the two classes Employee and Manager. Your task is to create a method in a new class that takes either Manager or an Employee as a parameter and prints its name.

	```
	public class Employee
	{
		public string Name { get; set; }
	}
	
	public class Manager
	{
		public string Name { get; set; }
	}
	```

## Requirements

- C#
- .NET 9+
- Blazor Interactive (wasm or server render mode)

## Getting started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- A SQL Server instance — Docker, a local SQL Server / SQLEXPRESS, or LocalDB (see [Database configuration](#database-configuration))

### Run the app

1. Choose and configure a database (see [Database configuration](#database-configuration)). The default targets the Dockerized SQL Server.
2. Start the app:

   ```bash
   dotnet run --project EpsilonWebApp
   ```

3. Open the app at `https://localhost:7234` (or `http://localhost:5234`).

The database schema is created automatically on startup (migrations are applied), so there are no manual DB setup steps.

### Log in to the Blazor UI

The app is protected by cookie authentication. Use the demo credentials:

| Username | Password |
|----------|-------------|
| `admin`  | `password123` |

After logging in you can manage customers at `/customers`.

### Use the REST API (JWT)

The API is protected with JWT bearer authentication:

1. `POST /api/auth/login` with `{ "username": "admin", "password": "password123" }` to obtain a token.
2. Send the token as an `Authorization: Bearer <token>` header on the `/api/customers` endpoints.

In Development, **Swagger UI** is available at `/swagger` — click **Authorize**, paste the token, and try the endpoints interactively.

### Run the tests

```bash
dotnet test
```

Tests run against an in-memory database and require no external SQL Server.

> **Note:** The demo credentials, JWT signing key, and SQL passwords are stored in `appsettings.json` for convenience. In a real deployment these would be moved to user-secrets / environment variables / a secrets manager.

## Database configuration

The application uses SQL Server. It ships with **three** connection strings and a selector so you can run it **with or without Docker** — no code changes required. Migrations are applied automatically on startup.

The active connection string is chosen by the `Database:ConnectionName` setting in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=127.0.0.1,1433;Database=EpsilonWebApp;User Id=sa;Password=YourStrong!Password123;Encrypt=False;TrustServerCertificate=True;MultipleActiveResultSets=true",
  "SqlExpressConnection": "Server=localhost\\SQLEXPRESS;Database=EpsilonWebApp;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True;MultipleActiveResultSets=true",
  "LocalDbConnection": "Server=(localdb)\\MSSQLLocalDB;Database=EpsilonWebApp;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True;MultipleActiveResultSets=true"
},
"Database": {
  "ConnectionName": "DefaultConnection"
}
```

### Option A — Dockerized SQL Server (default)

Start the container and run the app:

```bash
docker compose up -d
dotnet run --project EpsilonWebApp
```

`Database:ConnectionName` is already `DefaultConnection`, so no further changes are needed.

### Option B — Local SQL Server / SQLEXPRESS (no Docker)

If you already have a local SQL Server instance, point the app at the `SqlExpressConnection` string. You can do this without editing files by setting an environment variable:

```bash
# Windows (PowerShell)
$env:Database__ConnectionName="SqlExpressConnection"
dotnet run --project EpsilonWebApp

# Linux/macOS (bash)
Database__ConnectionName=SqlExpressConnection dotnet run --project EpsilonWebApp
```

Or simply change `"ConnectionName"` to `"SqlExpressConnection"` in `appsettings.json`.

Adjust the `SqlExpressConnection` string to match your instance (e.g. change the `Server` name, or swap `Trusted_Connection=True` for `User Id=...;Password=...` if you use SQL authentication).

### Option C — LocalDB (no Docker, ships with Visual Studio)

`(localdb)\MSSQLLocalDB` is installed with Visual Studio / the SQL Server Data Tools and needs no separate server. Point the app at the `LocalDbConnection` string:

```bash
# Windows (PowerShell)
$env:Database__ConnectionName="LocalDbConnection"
dotnet run --project EpsilonWebApp
```

Or set `"ConnectionName"` to `"LocalDbConnection"` in `appsettings.json`.

> **Note:** Integration tests do not require any of these options — they run against an in-memory database.
