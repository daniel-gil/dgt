using System.Collections.Generic;
using System.Linq;
using DGT.Models;
using DGT.Services;
using DGT.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DGT.WebApi.Controllers
{
    [Route("api/vehicles")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService vehicleService;
        private readonly IDriverService driverService;
        private readonly IVehicleDriverService vehicleDriverService;

        public VehicleController(
            IVehicleService vehicleService,
            IDriverService driverService,
            IVehicleDriverService vehicleDriverService)
        {
            this.vehicleService = vehicleService;
            this.driverService = driverService;
            this.vehicleDriverService = vehicleDriverService;
        }

        // GET: api/vehicles
        [HttpGet]
        public ActionResult<IEnumerable<Models.Vehicle>> GetVehicles()
        {
            var list = vehicleService.GetVehicles();
            return list?.ToList();
        }

        // GET: api/vehicles/5
        [HttpGet("{id}")]
        public ActionResult<Models.Vehicle> GetVehicle(string id)
        {
            var vehicle = vehicleService.GetVehicle(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return vehicle;
        }

        // POST: api/vehicles
        [HttpPost]
        public ActionResult<Models.Vehicle> CreateVehicle(ViewModels.VehicleViewModel vehicleViewModel)
        {
            var vehicle = vehicleService.GetVehicle(vehicleViewModel.Id);
            if (vehicle != null)
            {
                // here means that the vehicle already exists, return an error
                return Conflict("the vehicle already exists");
            }

            var driver = driverService.GetDriver(vehicleViewModel.DriverId);
            if (driver == null)
            {
                return BadRequest("the driver does not exists");
            }

            // first create the vehicle
            vehicle = new Vehicle
            {
                Brand= vehicleViewModel.Brand,
                Model = vehicleViewModel.Model,
                Id = vehicleViewModel.Id,
                LicensePlate = vehicleViewModel.LicensePlate,
            };
            vehicleService.CreateVehicle(vehicle);

            // then create the relationship between the driver and the vehicle
            var error = vehicleDriverService.CreateVehicleDriver(new VehicleDriver
            {
                Driver = driver,
                DriverId = driver.Id,
                Vehicle = vehicle,
                VehicleId = vehicle.Id,
            });

            if (error != "") {
                return UnprocessableEntity(error);
            }

            return CreatedAtAction(nameof(GetVehicle), new { id = vehicle.Id }, vehicle);
        }
    }
}
