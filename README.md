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
