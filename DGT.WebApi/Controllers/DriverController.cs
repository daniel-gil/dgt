using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DGT.Data;
using DGT.Models;
using DGT.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        // GET: api/drivers
        [HttpGet]
        public ActionResult<IEnumerable<Driver>> GetDrivers()
        {
            var list = driverService.GetDrivers();
            return list.ToList();
        }

        // GET: api/drivers/5
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
        public ActionResult<Driver> CreateDriver(Driver item)
        {
            driverService.CreateDriver(item);
            return CreatedAtAction(nameof(GetDriver), new { id = item.Id }, item);
        }
    }
}
