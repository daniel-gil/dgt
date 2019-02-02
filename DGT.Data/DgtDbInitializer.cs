using DGT.Models;
using System;
using System.Linq;

namespace DGT.Data
{
    public class DgtDbInitializer
    {
        private static DgtDbContext context;

        public static void Initialize(DgtDbContext ctx)
        {
            context = ctx;
            SeedDatabase();
        }

        private static void SeedDatabase()
        {
            if (!context.Drivers.Any())
            {
                Driver driver_01 = new Driver
                {
                    Id = "123456789Z",
                    Name = "John",
                    Surname = "Smith",
                    Points = 15
                };

                Driver driver_02 = new Driver
                {
                    Id = "987654321X",
                    Name = "Alice",
                    Surname = "Conor",
                    Points = 8
                };

                context.Drivers.Add(driver_01);
                context.Drivers.Add(driver_02);
            }

            if (!context.Vehicles.Any())
            {
                Vehicle vehicle_01 = new Vehicle
                {
                    Id = "0001",
                    LicensePlate = "0000ABC",
                    Brand = "Seat",
                    Model = "León"
                };

                Vehicle vehicle_02 = new Vehicle
                {
                    Id = "0002",
                    LicensePlate = "1111XYZ",
                    Brand = "Volkswagen",
                    Model = "Golf"
                };

                Vehicle vehicle_03 = new Vehicle
                {
                    Id = "0003",
                    LicensePlate = "3333XYZ",
                    Brand = "Volkswagen",
                    Model = "Touran"
                };

                Vehicle vehicle_04 = new Vehicle
                {
                    Id = "0004",
                    LicensePlate = "5555XXX",
                    Brand = "Ferrari",
                    Model = "Testarrosa"
                };

                context.Vehicles.Add(vehicle_01);
                context.Vehicles.Add(vehicle_02);
                context.Vehicles.Add(vehicle_03);
                context.Vehicles.Add(vehicle_04);
            }

            if (!context.Infractions.Any())
            {
                Infraction infraction_01 = new Infraction
                {
                    Id = "SPEEDING",
                    Description = "The driver was driving faster than the maximum speed allowed.",
                    PointsToDiscount = 3
                };

                Infraction infraction_02 = new Infraction
                {
                    Id = "RED_LIGHT",
                    Description = "Failure to stop at a red light or traffic sign.",
                    PointsToDiscount = 5
                };

                Infraction infraction_03 = new Infraction
                {
                    Id = "RECKLESS",
                    Description = "Reckless driving.",
                    PointsToDiscount = 7
                };
                
                context.Infractions.Add(infraction_01);
                context.Infractions.Add(infraction_02);
                context.Infractions.Add(infraction_03);
            }
            context.SaveChanges();


            if (!context.VehicleInfractions.Any())
            {
                if (context.Vehicles.Any() && context.Infractions.Any())
                {
                    var vIndex = 0;
                    foreach (Vehicle vehicle in context.Vehicles)
                    {

                        var iIndex = 0;
                        foreach (Infraction infraction in context.Infractions)
                        {
                            if (vIndex > 0 && iIndex > 0)
                            {
                                // only register all the infractions for the first vehicle, for the rest, just register the first infraction
                                break;
                            }
                            VehicleInfraction vehicle_infraction = new VehicleInfraction
                            {
                                Infraction = infraction,
                                InfractionId = infraction.Id,
                                Vehicle = vehicle,
                                VehicleId = vehicle.Id,
                                InfractionDate = DateTime.Now,
                            };

                            context.VehicleInfractions.Add(vehicle_infraction);
                            iIndex++;
                        }
                        
                        vIndex++;
                    }

                }
            }
            context.SaveChanges();
        }
    }
}
