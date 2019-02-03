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
It can be found in this repository the Postman file (DGT.postman_collection.json)[https://github.com/daniel-gil/dgt/blob/master/DGT.postman_collection.json] with all the available operations. You can import this file in your Postman application, launch the .Net Core solution and start testing it.

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

### Vehicle infraction

#### Get all infraction
Retrieve all the infractions (of any vehicles).

REQUEST:
```
GET {host}/api/vehicles/infractions
```

RESPONSE:
```
[
    {
        "id": "7bd4e5e6-2cf5-4470-848b-c0f22f2d9b6c",
        "infraction_date": "2019-02-03T09:29:47.9877115",
        "vehicle_id": "0004",
        "driver_id": "123456789Z",
        "infraction_id": "RECKLESS"
    },
    {
        "id": "9ecf6a3e-7742-4f77-bf74-1db65950779e",
        "infraction_date": "2019-02-03T09:29:47.8622201",
        "vehicle_id": "0001",
        "driver_id": "123456789Z",
        "infraction_id": "SPEEDING"
    },
    {
        "id": "c29b7d4e-deca-471e-8144-92137cf08fe7",
        "infraction_date": "2019-02-03T09:29:47.8604376",
        "vehicle_id": "0001",
        "driver_id": "123456789Z",
        "infraction_id": "RED_LIGHT"
    }
]
```


#### Get infractions by vehicle
Retrieve all the infractions for a given vehicle.

REQUEST:
```
GET {host}/api/vehicles/0001/infractions
```

RESPONSE:
```
[
    {
        "id": "e60db0e6-6e3d-43e6-aa7f-cefd814f49b2",
        "infraction_date": "2019-02-03T15:18:30.2041449",
        "vehicle_id": "0001",
        "driver_id": "123456789Z",
        "infraction_id": "SPEEDING"
    },
    {
        "id": "eec60776-db13-4587-bdfe-8e851f113e24",
        "infraction_date": "2019-02-03T15:18:30.2040403",
        "vehicle_id": "0001",
        "driver_id": "123456789Z",
        "infraction_id": "RED_LIGHT"
    }
]
```

#### Get vehicle infraction by ID
Retrieve the vehicle infraction by its ID.

REQUEST:
```
GET {host}/api/vehicles/infractions/349f1dc3-f57d-4cc3-8bca-e712c0cd813e
```

RESPONSE:
```
{
    "id": "349f1dc3-f57d-4cc3-8bca-e712c0cd813e",
    "infraction_date": "2019-02-03T15:18:30.2507586",
    "vehicle_id": "0004",
    "driver_id": "123456789Z",
    "infraction_id": "DUI"
}
```



#### Get top drivers (infractions)
Retrieve the N drivers with the most infractions. 

REQUEST:
```
GET {host}/api/drivers/top/2/infractions
```

RESPONSE:
```
[
    {
        "driver_id": "123456789Z",
        "amount": 7
    },
    {
        "driver_id": "987654321X",
        "amount": 2
    }
]
```




#### Add new infraction
Register a new vehicle infraction. The response returns the remaining driver points once applied the infraction.

We can specify in the body request the driver ID to which to associate the infraction like this:

REQUEST:
```
POST {host}/api/vehicles/0001/infractions/RED_LIGHT
Body:
    {
        "infraction_date": "2019-01-23 18:35:00",
        "driver_id" : "987654321X"
    }
```

RESPONSE:
```
Remaining driver points: 10
```

Or avoid passing the driver ID (because maybe we do not have this information), and in this case, the infraction will be associated to the main regular driver (stored in the Driver entity):

REQUEST:
```
POST {host}/api/vehicles/0001/infractions/RED_LIGHT
Body:
    {
        "infraction_date": "2019-01-23 18:35:00"
    }
```

RESPONSE:
```
Remaining driver points: 10
```



