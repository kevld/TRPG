using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TRPG.Models;

namespace TRPG.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpellController : ControllerBase
    {
        private readonly Db _context;

        public SpellController(Db context)
        {
            _context = context;
        }

        [HttpGet(Name = "All")]
        public async Task<IEnumerable<Spell>> GetSpells()
        {
            return await _context.Spells
                .OrderBy(x => x.Type)
                .ThenBy(x => x.Name)
                .ToListAsync();
        }

    }
}
