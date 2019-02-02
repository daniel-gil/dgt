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
    public class VehicleDriverController : ControllerBase
    {
       // private readonly IVehicleService vehicleService;
       // private readonly IInfractionService infractionService;
        private readonly IVehicleDriverService vehicleDriverService;

        public VehicleDriverController(
            // IVehicleService vehicleService,
            //IInfractionService infractionService,
            IVehicleDriverService vehicleDriverService)
        {
           // this.vehicleService = vehicleService;
            //this.infractionService = infractionService;
            this.vehicleDriverService = vehicleDriverService;
        }


        [HttpGet]
        [Route("api/drivers/{driverId}/vehicles")]
        public ActionResult<IEnumerable<Vehicle>> GetVehicleInfractions(string driverId)
        {
            var list = vehicleDriverService.GetVehiclesByDriver(driverId);
            return list?.ToList();
        }
    }
}
