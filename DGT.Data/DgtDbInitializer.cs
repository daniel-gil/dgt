using DGT.Models;
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
                    Id = "0000ABC",
                    Brand = "Seat",
                    Model = "León"
                };

                Vehicle vehicle_02 = new Vehicle
                {
                    Id = "1111XYZ",
                    Brand = "Volkswagen",
                    Model = "Golf"
                };

                context.Vehicles.Add(vehicle_01);
                context.Vehicles.Add(vehicle_02);
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

                context.Infractions.Add(infraction_01);
                context.Infractions.Add(infraction_02);
            }

            context.SaveChanges();
        }
    }
}
