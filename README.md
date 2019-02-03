# DGT System
The main functionality of the DGT System is to manage the traffic violations and the driving license points. For this purpose, it handles the following entities: Vehicles, Drivers, Driver's vehicle (maximal allowed is 10), Infraction types and Driver's infractions.

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

## Documentation
Besides this readme file, when launching the solution, it can be read the online documentation under the route `/swagger`. It is using the swagger open-source documentation framework.

## Postman
It can be found in this repository a Postman file with all the available operations. You can import this file in your Postman application, launch the .Net Core solution and start testing it.

**Note**: the enpoint in the postman tests are defined like this: `{{dgt_endpoint}}/api/`, it has to be declared the variable `dgt_endpoint` in the Postman Environment to something like that (if launched locally): `https://localhost:44372`.  

## Operations

### Drivers
Some clarifications about the `Driver` model:

* The ID of the driver represents the fiscal document number (it is, the DNI).
* The property `NumVehicles` is the number of vehicles associated with this driver. We keep this information as a property to avoid using a query each time we want to consult this value.

#### Get all drivers
Returns all the registered drivers.

REQUEST:
``` 
GET {host}/api/drivers 
```

RESPONSE:
```[
    {
        "id": "123456789Z",
        "name": "John",
        "surname": "Smith",
        "points": 15,
        "num_vehicles": 0
    },
    {
        "id": "987654321X",
        "name": "Alice",
        "surname": "Conor",
        "points": 8,
        "num_vehicles": 0
    }
] 
```

#### Get driver by ID
Retrieve a driver entity by its ID (it is, the DNI)

REQUEST:
```
    GET {host}/api/drivers/123456789Z
```

RESPONSE:
```
{
    "id": "123456789Z",
    "name": "John",
    "surname": "Smith",
    "points": 15,
    "num_vehicles": 4
}
```

#### Add new driver
Creates a new driver.

REQUEST:
```
POST {host}/api/drivers 
Body:
    {
        "id": "4567KKD",
        "name": "Daniel",
        "surname": "García",
        "points": 15
    }
```

RESPONSE:
```
{
    "id": "4567KKD",
    "name": "Daniel",
    "surname": "García",
    "points": 15,
    "num_vehicles": 0
}
```

#### Get driver's vehicles
Retrieve the vehicles associated to a driver.

REQUEST:
```
GET {host}/api/drivers/123456789Z/vehicles
```

RESPONSE:
```
[
    {
        "id": "0001",
        "license_plate": "0000ABC",
        "brand": "Seat",
        "model": "León",
        "main_regular_driver_id": "123456789Z"
    },
    {
        "id": "0002",
        "license_plate": "1111XYZ",
        "brand": "Volkswagen",
        "model": "Golf",
        "main_regular_driver_id": "123456789Z"
    }
]
```


### Vehicles

#### Get all vehicles
Retrieve all the registered vehicles.

REQUEST:
```
GET {host}/api/vehicles
```

RESPONSE:
```
[
    {
        "id": "0001",
        "license_plate": "0000ABC",
        "brand": "Seat",
        "model": "León",
        "main_regular_driver_id": "123456789Z"
    },
    {
        "id": "0002",
        "license_plate": "1111XYZ",
        "brand": "Volkswagen",
        "model": "Golf",
        "main_regular_driver_id": "123456789Z"
    }
]
```


#### Get a vehicle by ID
Retrieve a a vehicle by its ID, it is, the license plate (matrícula).

REQUEST:
```
GET {host}/api/vehicles/1111XYZ
```

RESPONSE
```
{
    "id": "1111XYZ",
    "brand": "Volkswagen",
    "model": "Golf"
}
```

#### Add new vehicle
Creates a new vehicle. 

**Note**: the first regular driver in the list will be set as the main regular driver.

REQUEST:
```
POST {host}/api/vehicles
Body:
    {
        "id": "0006665",
        "license_plate": "5454535",
        "brand": "Ferrari",
        "model": "Testarosa",
        "regular_drivers" : ["123456789Z", "987654321X"]
    }
```

RESPONSE
```
{
    "id": "0006665",
    "license_plate": "5454535",
    "brand": "Ferrari",
    "model": "Testarosa",
    "main_regular_driver_id": "123456789Z"
}
```


#### Get vehicle's drivers
Retrieve the regular drivers associated to a vehicle

REQUEST:
```
GET {host}/api/vehicles/0001/drivers
```

RESPONSE
```
[
    {
        "id": "123456789Z",
        "name": "John",
        "surname": "Smith",
        "points": 10,
        "num_vehicles": 1
    },
    {
        "id": "987654321X",
        "name": "Alice",
        "surname": "Conor",
        "points": 8,
        "num_vehicles": 1
    }
]
```


### Infraction types

#### Get all infraction types
Retrieve all the registered infraction types.

REQUEST:
```
GET {host}/api/infractions
```

RESPONSE:
```
[
    {
        "id": "RECKLESS",
        "description": "Reckless driving.",
        "points_to_discount": 7
    },
    {
        "id": "RED_LIGHT",
        "description": "Failure to stop at a red light or traffic sign.",
        "points_to_discount": 5
    },
    {
        "id": "SPEEDING",
        "description": "The driver was driving faster than the maximum speed allowed.",
        "points_to_discount": 3
    }
]
```


#### Get an infraction types by ID
Retrieve the infraction type by its ID.

REQUEST:
```
GET {host}/api/infractions/SPEEDING
```

RESPONSE:
```
{
    "id": "SPEEDING",
    "description": "The driver was driving faster than the maximum speed allowed.",
    "points_to_discount": 3
}
```


#### Add new infraction types
Creates a new infraction type

REQUEST:
```
POST {host}/api/infractions
Body:
    {
        "id": "SPEEDING",
        "description": "The driver was driving faster than the maximum speed allowed.",
        "points_to_discount":  5
    }
```

RESPONSE:
```
{
    "id": "SPEEDING",
    "description": "The driver was driving faster than the maximum speed allowed.",
    "points_to_discount": 3
}
```


#### Get top infraction types
Retrieve the top infraction types (the most common).

By default, it returns the top 5 items:  

REQUEST:
```
GET {host}/api/infractions/top
```

RESPONSE:
```
[
    {
        "infraction_type": "DUI",
        "amount": 4
    },
    {
        "infraction_type": "DWI",
        "amount": 1
    },
    {
        "infraction_type": "SPEEDING",
        "amount": 1
    },
    {
        "infraction_type": "RED_LIGHT",
        "amount": 1
    },
    {
        "infraction_type": "RECKLESS",
        "amount": 1
    }
]
```

If we want another top number different than 5, we just have to append the desired value at the end of the route like this: 

REQUEST:
```
GET {host}/api/infractions/top/2
```

RESPONSE:
```
[
    {
        "infraction_type": "DUI",
        "amount": 4
    },
    {
        "infraction_type": "DWI",
        "amount": 1
    }
]
```

### Infraction types

### Vehicle infraction



