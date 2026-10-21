# Dataverse Contact API

A small ASP.NET Core API that updates a contact in Microsoft Dataverse by using a mobile number.

I built this project as a practical example of connecting a layered .NET application to an on-premises Dataverse or Dynamics 365 environment. The API, application logic, domain models, and Dataverse integration live in separate projects, so the code is easier to read, understand, and maintain.

## What does it do?

The API exposes one endpoint that:

1. Receives a mobile number.
2. Finds the matching contact in Dataverse.
3. Updates the contact's status reason.
4. Returns whether the update was successful.

It is intentionally small. That makes it a useful starting point or reference for similar Dataverse integrations.

## Project structure

```text
Test.Domain            Dataverse entities and domain models
Test.Application       Commands, services, validation, and interfaces
Test.Infrastructure    Dataverse connection and repositories
Test.Presentation.Api  Controllers, middleware, logging, and Swagger
```

## Main technologies

- .NET 8 and ASP.NET Core
- Microsoft Dataverse
- Data8 Power Platform Dataverse Client
- MediatR
- FluentValidation
- Serilog
- Swagger / OpenAPI

## Getting started

You will need the .NET 8 SDK and access to a compatible Dataverse or Dynamics 365 environment.

Clone the repository, then restore and build the solution:

```bash
dotnet restore
dotnet build
```

### Configure the connection

Connection details are not stored in this repository. Provide them through environment variables, .NET User Secrets, or your preferred secret manager.

The application reads these values:

```text
Dataverse__ServiceUrl
Dataverse__Username
Dataverse__Password
```

If the application should use the current Windows account, leave the username and password unset.

### Run the API

```bash
dotnet run --project Test.Presentation.Api
```

Once the API is running, open `/swagger` to explore and test the available endpoint.

## API example

```http
POST /api/v1/Contact/UpdateStatusReasonByMobilePhone
Content-Type: application/json

{
  "mobilePhone": "<mobile-number>"
}
```

When the contact is found and updated, the API returns:

```json
{
  "updated": true
}
```

If no matching contact is found, the API returns `404 Not Found`.

## A note about security

Keep service URLs, credentials, tokens, and local configuration outside version control. The included `.gitignore` already excludes common local settings, logs, IDE files, and build output.

If a credential was committed in the past, removing it from the latest file is not enough. Rotate the credential and remove it from the repository history before making the project public.
