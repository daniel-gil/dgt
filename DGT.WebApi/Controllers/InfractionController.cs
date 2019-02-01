using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DGT.Data;
using DGT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DGT.WebApi.Controllers
{
    [Route("api/infractions")]
    [ApiController]
    public class InfractionController : ControllerBase
    {
        private readonly DgtDbContext _context;

        public InfractionController(DgtDbContext context)
        {
            _context = context;
        }

        // GET: api/infractions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Infraction>>> GetInfractions()
        {
            return await _context.Infractions.ToListAsync();
        }

        // GET: api/infractions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Infraction>> GetInfraction(string id)
        {
            var infraction = await _context.Infractions.FindAsync(id);
            if (infraction == null)
            {
                return NotFound();
            }
            return infraction;
        }

        // POST: api/infractions
        [HttpPost]
        public async Task<ActionResult<Infraction>> CreateInfraction(Infraction item)
        {
            _context.Infractions.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInfraction), new { id = item.Id }, item);
        }
    }
}
