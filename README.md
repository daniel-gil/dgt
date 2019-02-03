# DGT System
DGT system to control drivers, vehicles and infractions in .Net Core.

## .Net Core Solution

### Structure
The solution follows the clean architecture and is structured in the following projects: Models, Data, Services and WebApi.

#### DGT.Models

#### DGT.Data

#### DGT.Services

#### DGT.WebApi

### appsettings.json
This file contains the database connection `DefaultConnection` which by default connects to the local DB server.

It also has the flag `InMemoryProvider` to enable an in-memory database, useful for testing purposes.

### Seed data
The project DGT.Data includes a file called `DgtDbInitializer.cs` which initialize the databse in case it's empty. If you want to disable this seeding, just comment the line `SeedDatabase();` from the same file.

## Postman
It can be found in this repository a Postman file with all the available operations. You can import this file in your Postman application, launch the .Net Core solution and start testing it.

## Operations

### Drivers

#### Add new driver

