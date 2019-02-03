using DGT.Data;
using DGT.Services;
using DGT.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using System.Linq;

namespace DGT.UnitTests
{
    public class TestDriverService
    {
        [Fact]
        public void TestGetDrivers()
        {
            var options = new DbContextOptionsBuilder<DgtDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options;
            var context = new DgtDbContext(options);
            context.Database.EnsureCreated();
            DgtDbInitializer.Initialize(context);

            DriverRepository driverRepository = new DriverRepository(context);
            DriverService driverService = new DriverService(driverRepository);
            var drivers = driverService.GetDrivers();
            Assert.True(drivers.Count() == 2);
        }
    }
}
