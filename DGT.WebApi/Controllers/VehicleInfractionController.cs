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
    public class VehicleInfractionController : ControllerBase
    {
        private readonly IVehicleService vehicleService;
        private readonly IInfractionService infractionService;
        private readonly IVehicleInfractionService vehicleInfractionService;

        public VehicleInfractionController(
            IVehicleService vehicleService,
            IInfractionService infractionService,
            IVehicleInfractionService vehicleInfractionService)
        {
            this.vehicleService = vehicleService;
            this.infractionService = infractionService;
            this.vehicleInfractionService = vehicleInfractionService;
        }


        /// <summary>
        ///  Get all the infractions of a vehicle
        /// </summary>
        [HttpGet]
        [Route("api/vehicles/{vehicleId}/infractions")]
        public ActionResult<IEnumerable<VehicleInfraction>> GetVehicleInfractions(string vehicleId)
        {
            var list = vehicleInfractionService.GetInfractionsByVehicle(vehicleId);
            return list?.ToList();
        }

        /// <summary>
        ///  Get all the infractions
        /// </summary>
        [HttpGet]
        [Route("api/vehicles/infractions")]
        public ActionResult<IEnumerable<VehicleInfraction>> GetInfractions()
        {
            var vehicleInfractions = vehicleInfractionService.GetVehicleInfractions();
            return vehicleInfractions?.ToList();
        }

        /// <summary>
        ///  Get an infractions by its ID
        /// </summary>
        [HttpGet]
        [Route("api/vehicles/infractions/{vehicleInfractionId}")]
        public ActionResult<VehicleInfraction> GetVehicleInfraction(string vehicleInfractionId)
        {
            var vehicleInfraction = vehicleInfractionService.GetVehicleInfraction(vehicleInfractionId);
            if (vehicleInfraction == null)
            {
                return NotFound();
            }
            return vehicleInfraction;
        }

        /// <summary>
        ///  Register a new infraction of a vehicle
        /// </summary>
        [HttpPost]
        [Route("api/vehicles/{vehicleId}/infractions/{infractionId}")]
        public ActionResult<Vehicle> RegisterVehicleInfraction(string vehicleId, string infractionId, InfractionViewModel request)
        {
            var vehicle = vehicleService.GetVehicle(vehicleId);
            if (vehicle == null)
            {
                return NotFound("vehicle not found");
            }

            var infraction = infractionService.GetInfraction(infractionId);
            if (infraction == null)
            {
                return NotFound("infraction not found");
            }

            // in case that the driver is not specified in the request, we assume that the infraction shall be assigned to the main regular driver from this vehicle
            var driverId = request.DriverId;
            if (string.IsNullOrEmpty(driverId))
            {
                driverId = vehicle.MainRegularDriverId;
            }

            VehicleInfraction vehicleInfraction = new VehicleInfraction
            {
                Infraction = infraction,
                InfractionId = infraction.Id,
                Vehicle = vehicle,
                VehicleId = vehicle.Id,
                InfractionDate = request.InfractionDate,
                DriverId = driverId,
            };
            var remainingDriverPoints = vehicleInfractionService.RegisterInfraction(vehicleInfraction);
            return Ok("Remaining driver points: " + remainingDriverPoints);
        }

        /// <summary>
        ///  Get the top Infraction types
        /// </summary>
        [HttpGet]
        [Route("api/infractions/top/{top}")]
        public ActionResult<IEnumerable<TopInfraction>> GetTopInfractions(int top)
        {
            var list = vehicleInfractionService.GetTopInfractions(top);
            return list?.ToList();
        }

        /// <summary>
        ///  Get the top 5 Infraction types
        /// </summary>
        [HttpGet]
        [Route("api/infractions/top")]
        public ActionResult<IEnumerable<TopInfraction>> GetTopFiveInfractions()
        {
            var list = vehicleInfractionService.GetTopInfractions(5);
            return list?.ToList();
        }


        /// <summary>
        ///  Get the top Drivers with more infractions
        /// </summary>
        [HttpGet]
        [Route("api/drivers/top/{top}/infractions")]
        public ActionResult<IEnumerable<TopDriver>> GetTopDrivers(int top)
        {
            var list = vehicleInfractionService.GetTopDrivers(top);
            return list?.ToList();
        }
    }
}
