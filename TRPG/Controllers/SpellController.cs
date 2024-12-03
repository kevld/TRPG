using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TRPG.Models;

namespace TRPG.Controllers
{
    /// <summary>
    /// Spell controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class SpellController : ControllerBase
    {
        private readonly Db _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context">DB Context</param>
        public SpellController(Db context)
        {
            _context = context;
        }

        /// <summary>
        /// Get the full list of spells
        /// </summary>
        /// <returns></returns>
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
