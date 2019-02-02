using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DGT.Models;
using DGT.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DGT.WebApi.Controllers
{
    [Route("api/infractions")]
    [ApiController]
    public class InfractionController : ControllerBase
    {
        private readonly IInfractionService infractionService;

        public InfractionController(IInfractionService infractionService)
        {
            this.infractionService = infractionService;
        }

        // GET: api/infractions
        [HttpGet]
        public ActionResult<IEnumerable<Infraction>> GetInfractions()
        {
            var list = infractionService.GetInfractions();
            return list?.ToList();
        }

        // GET: api/infractions/5
        [HttpGet("{id}")]
        public ActionResult<Infraction> GetInfraction(string id)
        {
            var infraction = infractionService.GetInfraction(id);
            if (infraction == null)
            {
                return NotFound();
            }
            return infraction;
        }

        // POST: api/infractions
        [HttpPost]
        public ActionResult<Infraction> CreateInfraction(Infraction item)
        {
            infractionService.CreateInfraction(item);
            return CreatedAtAction(nameof(GetInfraction), new { id = item.Id }, item);
        }
    }
}
