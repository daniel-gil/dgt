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


        // POST: api/vehicles/{vehicleId}/infraction/{infractionId}
        [HttpPost("{vehicleId}/infraction/{infractionId}")]
        public ActionResult<Vehicle> RegisterVehicleInfraction(string vehicleId, string infractionId)
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

            vehicleInfractionService.RegisterInfraction(vehicle, infraction);
            return Ok();
        }
    }
}
