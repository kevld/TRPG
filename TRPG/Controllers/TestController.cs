using Microsoft.AspNetCore.Mvc;
using TRPG.Models;
using TRPG.System;

namespace TRPG.Controllers
{
    /// <summary>
    /// Testing features
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly Db _context;

        /// <summary>
        /// DB Context
        /// </summary>
        /// <param name="context"></param>
        public TestController(Db context)
        {
            _context = context;
        }

        /// <summary>
        /// Import spells
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
        public IActionResult ImportCsv()
        {
            var csvSpells = new ImportSpellCsv(_context);
            csvSpells.ImportCsvFile("./spells.csv");

            return Ok(_context.Spells.ToList());
        }
    }
}
