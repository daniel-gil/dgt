using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DGT.Models;
using DGT.Services;
using DGT.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DGT.WebApi.Controllers
{
    [ApiController]
    public class VehicleDriverController : ControllerBase
    {
        private readonly IVehicleDriverService vehicleDriverService;

        public VehicleDriverController(
            IVehicleDriverService vehicleDriverService)
        {
            this.vehicleDriverService = vehicleDriverService;
        }

        /// <summary>
        ///  Get all the vehicles of a driver
        /// </summary>
        [HttpGet]
        [Route("api/drivers/{driverId}/vehicles")]
        public ActionResult<IEnumerable<Vehicle>> GetVehiclesByDriver(string driverId)
        {
            var list = vehicleDriverService.GetVehiclesByDriver(driverId);
            return list?.ToList();
        }

        /// <summary>
        ///  Get all the drivers associated to a vehicle
        /// </summary>
        [HttpGet]
        [Route("api/vehicles/{vehicleId}/drivers")]
        public ActionResult<IEnumerable<Driver>> GetDriversByVehicle(string vehicleId)
        {
            var list = vehicleDriverService.GetDriversByVehicle(vehicleId);
            return list?.ToList();
        }
    }
}
