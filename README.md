# Main API - Workshop 1 - System's Architecture

This repository contains the main API used by the **Perla Metro** system from the first workshop of the subject "Arquitectura de Sistemas" at Universidad Católica del Norte. Below are the tools used and how to setup this project locally.

## Pre-requisites

- [.NET SDK](https://dotnet.microsoft.com/es-es/download) (version 9.0.109)
- [Git](https://git-scm.com/) (version 2.49.0)
- [Docker or Docker Desktop](https://docs.docker.com/)

> You can run the project with the .NET SDK directly or through Docker only. Docker is recommended because it avoids installing the SDK locally.

The API depends on the following services. Ensure they are running and reachable in your environment:

- [Users Service](https://github.com/Taller-1-Arq-de-Sistemas/perla-metro-users-service)
- [Stations Service](https://github.com/Taller-1-Arq-de-Sistemas/perla-metro-station-service)
- [Routes Service](https://github.com/Taller-1-Arq-de-Sistemas/perla-metro-routes-services)
- [Tickets Service](https://github.com/Taller-1-Arq-de-Sistemas/perla-metro-ticket-service)

Each repository includes its own README with setup instructions.

## Installation and configuration

1. **Clone the repository**

```bash
git clone https://github.com/Taller-1-Arq-de-Sistemas/perla-metro-api-main.git
```

2. **Navigate to the project directory**

```bash
cd perla-metro-api-main
```

## Configuration with Docker

1. Create a `.env.development` file from the example template and provide values that match your local services.

```bash
cp .env.example .env.development
```

Key variables:

- `ASPNETCORE_ENVIRONMENT`: Use `Development` for local work so Swagger is available at `/swagger`.
- `ASPNETCORE_URLS`: Listening address for the API.
- `USERS_SERVICE_URL`, `STATIONS_SERVICE_URL`, `ROUTES_SERVICE_URL`, `TICKETS_SERVICE_URL`: URLs for each downstream service.
- `JWT_SECRET`: Secret used to sign and validate JWT tokens.

Once you have replaced everything, save the changes and move on to the next step.

2. Build and launch the containers

```bash
docker compose up --build -d
```

A Docker container would be running the web app in the port **8080**

## Configuration without Docker

1. Restore project dependencies

```bash
dotnet restore
```

2. Initialize user secrets

```bash
dotnet user-secrets init
```

3. Seed user secrets (adjust values as needed for your environment)

```bash
dotnet user-secrets set "JWT_SECRET" "568f60141067e2f67d755a45e6afa6cc0ae70fb5c687cc7a846a2d38baf678f9"
dotnet user-secrets set "USERS_SERVICE_URL" "http://localhost:3010"
dotnet user-secrets set "STATIONS_SERVICE_URL" "http://localhost:3011"
dotnet user-secrets set "ROUTES_SERVICE_URL" "http://localhost:3012"
dotnet user-secrets set "TICKETS_SERVICE_URL" "http://localhost:3013"
```

**Note (secret values)**: You can change these values to whatever you want, just make sure that these values match to the values in the entire system (like the JWT secret).

4. Run the application

```bash
dotnet run
```

The server starts at the configured address (for example, http://localhost:8080). Swagger UI is available while `ASPNETCORE_ENVIRONMENT=Development`.

## Operations

The operations that this application expose are 7 and are separated in four modules: the users module, the stations module, the routes module and the tickets module. Below is a detailed overview of these operations:

### Users module

In this module we have 7 operations:

- **Register**: This endpoint allows to any user to register in the system as a passenger user. Its specification is like this:

  - URI: auth/register
  - Type: POST
  - Input:
    - Parameters: None
    - Query parameters: None
    - Body:
      - Name: Name of the user.
      - Last names: Last names of the user.
      - Email: Email using Perla Metro domain.
      - Password: Password of the user with at least 8 characters that must include 1 upper letter, 1 lower letter, 1 number and 1 symbol.
  - Response:
    - Status code: 201 if successful.
    - Id: ID of the user.
    - Name: Name of the user.
    - Last names: Last names of the user.
    - Email: Email of the user.
    - Role: Role of the user.
    - Token: JWT created for user authentication.

- **Login**: This endpoint allows to any user registered in the system to login using its credentials. Its specification is like this:

  - URI: auth/login
  - Type: POST
  - Input:
    - Parameters: None
    - Query parameters: None
    - Body:
      - Email: Email of the user.
      - Password: Password of the user.
  - Response:
    - Status code: 200 if successful.
    - Body:
      - Token: JWT created for user authentication.

- **Create user**: This endpoint allows to only admins to register a new user in the system. Its specification is like this:

  - URI: users
  - Type: POST
  - Input:
    - Parameters: None
    - Query parameters: None
    - Body:
      - Name: Name of the user.
      - Last names: Last names of the user.
      - Email: Email using Perla Metro domain.
      - Password: Password of the user with at least 8 characters that must include 1 upper letter, 1 lower letter, 1 number and 1 symbol.
      - Role: Role of the user.
  - Response:
    - Status code: 201 if successful
    - Id: ID of the user.
    - Name: Name of the user.
    - Last names: Last names of the user.
    - Email: Email of the user.
    - Role: Role of the user.
    - Status: Status of the user.
    - Created at: Creation date of the user.

- **Get all users**: This endpoint allows to only admins to get all the users in the system. Its specification is like this:

  - URI: users
  - Type: GET
  - Input:
    - Parameters: None
    - Query parameters:
      - Name: Full or partial name of users.
      - Email: Full or partial email address.
      - Status: One of `active` (default), `deleted`, or `all`.
    - Body: None
  - Response: A set of users that includes:
    - Status code: 200 if successful.
    - Id: Id of a user.
    - Full name: Full name of a user.
    - Email: Email of a user.
    - Status: Status of a user.
    - Created at: Creation date of a user.

- **Get a user**: This endpoint allows to only admins to get a user in the system using its ID. Its specification is like this:

  - URI: users/{**id**}
  - Type: GET
  - Input:
    - Parameters:
      - ID: ID of the user to get.
    - Query parameters: None.
    - Body: None
  - Response:
    - Status code: 200 if successful.
    - Id: Id of the user.
    - Name: Name of the user.
    - Last names: Last names of the user.
    - Email: Email of the user
    - Role: Role of the user.
    - Status: Status of the user.
    - Created at: Creation date of the user.

- **Update user**: This endpoint allows to only admins to edit an user in the system. Its specification is like this:

  - URI: users/{**id**}
  - Type: PATCH
  - Input:
    - Parameters:
      - ID: ID of the user to edit.
    - Query parameters: None
    - Body:
      - Name: Name of the user.
      - Last names: Last names of the user.
      - Email: Email using Perla Metro domain.
      - Password: Password of the user with at least 8 characters that must include 1 upper letter, 1 lower letter, 1 number and 1 symbol.
  - Response:
    - Status code: 204 if successful.

- **Delete user**: This endpoint allows to only admins to delete a user in the system (soft delete). Its specification is like this:

  - URI: users/{**id**}
  - Type: DELETE
  - Input:
    - Parameters:
      - ID: ID of the user to delete.
    - Query parameters: None
    - Body: None
  - Response:
    - Status code: 204 if successful.

### Stations module

In this module we have 5 operations:

- **Create station**: This endpoint allows to create a new station in the system, avoiding duplicates by name and/or location. Its specification is like this:

  - URI: Station/CreateStation
  - Type: POST
  - Authorization: Only users with **admin role**
  - Input:
    - Parameters: None
    - Query parameters: None
    - Body (DTO):
      - Name: Name of the station.
      - Location: Location of the station.
      - Type: Type of the station (e.g., Origin, Intermediate, Destination).
  - Response:
    - Status code: 200 if successful.
    - Message: "Station created successfully"
    - Station:
      - Id
      - Name
      - Location
      - Type
      - Status

- **Get all stations**: This endpoint allows to get all stations in the system. Its specification is like this:

  - URI: Station/Stations
  - Type: GET
  - Authorization: Only users with **admin role** (since it includes the station status).
  - Input:
    - Parameters: None
    - Query parameters (optional):
      - Name: Full or partial station name.
      - Type: Station type.
      - Status: One of `active`, `disabled`, or `all`.
    - Body: None
  - Response:
    - Status code: 200 if successful.
    - Message: "Stations list retrieved successfully"
    - Stations list:
      - Id
      - Name
      - Location
      - Type
      - Status

- **Get a station by ID**: This endpoint allows to get a station in the system using its ID (only active stations). Its specification is like this:

  - URI: Station/{**id**}
  - Type: GET
  - Authorization: No role restriction.
  - Input:
    - Parameters:
      - ID: ID of the station to get.
    - Query parameters: None
    - Body: None
  - Response:
    - Status code: 200 if successful.
    - Message: "Station retrieved successfully"
    - Station:
      - Id
      - Name
      - Location
      - Type

- **Update station**: This endpoint allows to update a station in the system using its ID. The editable fields are Name, Location, and Type. Duplicates by Name and/or Location are not allowed. Its specification is like this:

  - URI: Station/EditStation
  - Type: POST
  - Authorization: Only users with **admin role**
  - Input:
    - Parameters: None
    - Query parameters: None
    - Body (DTO):
      - Id: ID of the station.
      - Name: New name of the station.
      - Location: New location of the station.
      - Type: New type of the station.
  - Response:
    - Status code: 200 if successful.
    - Message: "Station updated successfully"
    - Station:
      - Id
      - Name
      - Location
      - Type

- **Change station state (Soft delete / enable-disable)**: This endpoint allows to enable or disable a station (soft delete). The station record remains in the database, but its status is updated. Its specification is like this:

  - URI: Station/ChangeStateStation/{**id**}
  - Type: POST
  - Authorization: Only users with **admin role**
  - Input:
    - Parameters:
      - ID: ID of the station.
    - Query parameters: None
    - Body: None
  - Response:
    - Status code: 200 if successful.
    - Message: "Station state changed successfully"

### Routes module

In this module we have n operations:

### Tickets module

In this module we have n operations:

These operations with their specifications can also be checked in the URI [**swagger**](http://localhost:8080/swagger) when running in the `Development` environment. The OpenAPI JSON is available at `/openapi/v1.json`.

## System architecture

This app is a part of a SOA system mentioned in the beginning (Perla Metro). It is composed by a server made in .NET like the image shown below.

<img src="./src/RMUtils/SystemArchitecture.png">

## Design patterns applied

The service uses several patterns to keep the codebase modular, testable, and maintainable:

**Layered (Controller–Service):** Separates HTTP concerns (Controllers) and application/business logic (Services). Controllers delegate to services and services makes calls to the corresponding services to handle operations.

**Dependency Injection (IoC):** All dependencies are registered and resolved via the built‑in DI container. See `AddApplicationServices` and `AddWebApp` for service lifetimes (scoped/singleton) and framework services.

**DTO (Data Transfer Object):** Public API types decouple wire contracts from domain models. Request/response DTOs live in `src/DTOs` and are validated via DataAnnotations.

**Middleware:** Cross‑cutting concerns implemented as pipeline middleware, notably `ExceptionHandlerMiddleware` to catch errors.

**Configuration via Extension Methods:** Presentation/infrastructure wiring is grouped in extension methods (`AppServiceExtensions`) to keep `Program.cs` minimal and clarify composition.

**Routing Convention:** `LowercaseParameterTransformer` enforces lowercase route tokens via `RouteTokenTransformerConvention` for consistent URLs.

## Assumptions and decisions

The implementation closely follows the workshop specification with two notable adjustments:

- All endpoints except login and register require authentication.
- The station type attribute is omitted in the stations table to simplify communication between station and route services.

## Production notes

- This service is already deployed on Render and it is accessible through this URL: https://perla-metro-api-main-v1.onrender.com.

## Authors

- [@Jairo Calcina](https://github.com/Broukt)
- [@Francisco Concha](https://github.com/Pancho-UwU)
- [@Cristhian Montoya](https://github.com/srCochayuyo)
- [@Ignacio Morales](https://github.com/Thetrolxs)
