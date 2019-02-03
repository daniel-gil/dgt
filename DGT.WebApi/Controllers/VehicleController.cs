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

        /// <summary>
        ///  Get all Vehicles
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Models.Vehicle>> GetVehicles()
        {
            var list = vehicleService.GetVehicles();
            return list?.ToList();
        }

        /// <summary>
        ///  Get a Vehicles by its ID
        /// </summary>
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

        /// <summary>
        ///  Create a new Vehicles
        /// </summary>
        [HttpPost]
        public ActionResult<Models.Vehicle> CreateVehicle(VehicleViewModel vehicleViewModel)
        {
            var vehicle = vehicleService.GetVehicle(vehicleViewModel.Id);
            if (vehicle != null)
            {
                // here means that the vehicle already exists, return an error
                return Conflict("the vehicle already exists");
            }

            // first loop through all drivers from the request checking that exists, and storing the first one as the main regular driver
            string mainRegularDriverId = "";
            foreach (string regularDriverId in vehicleViewModel.RegularDrivers)
            {
                var driver = driverService.GetDriver(regularDriverId);
                if (driver == null)
                {
                    return BadRequest("the driver '"+ regularDriverId + "' does not exists");
                }

                if (mainRegularDriverId == "")
                {
                    mainRegularDriverId = regularDriverId;
                }
            }

            // second create the vehicle
            vehicle = new Vehicle
            {
                Brand = vehicleViewModel.Brand,
                Model = vehicleViewModel.Model,
                Id = vehicleViewModel.Id,
                LicensePlate = vehicleViewModel.LicensePlate,
                MainRegularDriverId = mainRegularDriverId,
            };
            vehicleService.CreateVehicle(vehicle);

            // finally create the relationship between the drivers and the vehicle
            foreach (string regularDriverId in vehicleViewModel.RegularDrivers)
            {
                var error = vehicleDriverService.CreateVehicleDriver(new VehicleDriver
                {
                    DriverId = regularDriverId,
                    VehicleId = vehicle.Id,
                });

                if (error != "")
                {
                    return UnprocessableEntity(error);
                }
            }
            return CreatedAtAction(nameof(GetVehicle), new { id = vehicle.Id }, vehicle);
        }
    }
}
