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
//    [Route("api/vehicles")]
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


        [HttpGet]
        [Route("api/vehicles/{vehicleId}/infractions")]
        public ActionResult<IEnumerable<VehicleInfraction>> GetVehicleInfractions(string vehicleId)
        {
            var list = vehicleInfractionService.GetInfractionsByVehicle(vehicleId);
            return list?.ToList();
        }


        [HttpGet("{id}")]
        [Route("api/vehicles/infractions")]
        public ActionResult<IEnumerable<VehicleInfraction>> GetInfractions()
        {
            var vehicleInfractions = vehicleInfractionService.GetVehicleInfractions();
            return vehicleInfractions?.ToList();
        }


        [HttpGet("{id}")]
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



        // POST: api/vehicles/{vehicleId}/infraction/{infractionId}
        [HttpPost]
        [Route("api/vehicles/{vehicleId}/infractions/{infractionId}")]
        public ActionResult<Vehicle> RegisterVehicleInfraction(string vehicleId, string infractionId, RegisterInfractionRequest request)
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
            VehicleInfraction vehicleInfraction = new VehicleInfraction
            {
                Infraction = infraction,
                InfractionId = infraction.Id,
                Vehicle = vehicle,
                VehicleId = vehicle.Id,
                InfractionDate = request.InfractionDate,
            };
            vehicleInfractionService.RegisterInfraction(vehicleInfraction);
            return Ok();
        }


    }
}
