using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DGT.Data;
using DGT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DGT.WebApi.Controllers
{
    [Route("api/vehicles")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly DgtDbContext _context;

        public VehicleController(DgtDbContext context)
        {
            _context = context;
        }

        // GET: api/vehicles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
        {
            return await _context.Vehicles.ToListAsync();
        }

        // GET: api/vehicles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicle(string id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return vehicle;
        }

        // POST: api/vehicles
        [HttpPost]
        public async Task<ActionResult<Vehicle>> CreateVehicle(Vehicle item)
        {
            _context.Vehicles.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVehicle), new { id = item.Id }, item);
        }
    }
}
