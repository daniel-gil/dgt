using System.Collections.Generic;
using System.Linq;
using DGT.Models;
using DGT.Services;
using Microsoft.AspNetCore.Mvc;

namespace DGT.WebApi.Controllers
{
    [Route("api/drivers")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService driverService;

        public DriverController(IDriverService driverService)
        {
            this.driverService = driverService;
        }

        /// <summary>
        /// Get all the drivers.
        /// </summary>   
        [HttpGet]
        public ActionResult<IEnumerable<Driver>> GetDrivers()
        {
            var list = driverService.GetDrivers();
            return list?.ToList();
        }

        /// <summary>
        /// Get a driver by it's ID (it is, the DNI)
        /// </summary>
        /// <param name="id"></param>      
        [HttpGet("{id}")]
        public ActionResult<Driver> GetDriver(string id)
        {
            var driver = driverService.GetDriver(id);
            if (driver == null)
            {
                return NotFound();
            }
            return driver;
        }

        // POST: api/drivers
        [HttpPost]
        public ActionResult<Driver> CreateDriver(Driver driver)
        {
            var d = driverService.GetDriver(driver.Id);
            if (d != null)
            {
                return UnprocessableEntity("the driver already exists");
            }

            driverService.CreateDriver(driver);
            return CreatedAtAction(nameof(GetDriver), new { id = driver.Id }, driver);
        }
    }
}
