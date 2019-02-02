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

            context.SaveChanges();
        }
    }
}
