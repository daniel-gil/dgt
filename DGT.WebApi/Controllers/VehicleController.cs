using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DGT.Models;
using DGT.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DGT.WebApi.Controllers
{
    [Route("api/vehicles")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        // GET: api/vehicles
        [HttpGet]
        public ActionResult<IEnumerable<Vehicle>> GetVehicles()
        {
            var list = vehicleService.GetVehicles();
            return list.ToList();
        }

        // GET: api/vehicles/5
        [HttpGet("{id}")]
        public ActionResult<Vehicle> GetVehicle(string id)
        {
            var driver = vehicleService.GetVehicle(id);
            if (driver == null)
            {
                return NotFound();
            }
            return driver;
        }

        // POST: api/vehicles
        [HttpPost]
        public ActionResult<Vehicle> CreateVehicle(Vehicle item)
        {
            vehicleService.CreateVehicle(item);
            return CreatedAtAction(nameof(GetVehicle), new { id = item.Id }, item);
        }
    }
}
