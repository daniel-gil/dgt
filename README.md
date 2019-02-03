# DGT System

## .Net Core Solution
This solution uses Entity Framework Core with the migrations enabled.

### Structure
The solution follows the clean architecture and is structured in the following projects: Models, Data, Services and WebApi.

#### DGT.Models
It's a class library which contain all the domain objects. Those objects will be used by the EntityFramework to build the database.

#### DGT.Data
It's a class library responsible to access the database.

#### DGT.Services
It's a class library that exposes the available operations to the clients (MVC or Web API controllers). This layer contains the business logic.

#### DGT.WebApi
It's a Web API application and represents the presentation layer.

### appsettings.json
This file contains the database connection `DefaultConnection` which by default connects to the local DB server.

It also has the flag `InMemoryProvider` to enable an in-memory database, useful for testing purposes.

### Seed data
The project DGT.Data includes a file called `DgtDbInitializer.cs` which initialize the databse in case it's empty. If you want to disable this seeding, just comment the line `SeedDatabase();` from the same file.

DGT system to control drivers, vehicles and infractions in .Net Core.

## Documentation
When launching the solution, it can be read the online documentation under the route `/swagger`. It is using the swagger open-source documentation framework.

## Postman
It can be found in this repository a Postman file with all the available operations. You can import this file in your Postman application, launch the .Net Core solution and start testing it.

## Operations

### Drivers

#### Add new driver

### Vehicles

### Infraction types

### Vehicle infraction



