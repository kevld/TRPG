using Microsoft.AspNetCore.Mvc;
using TRPG.Models;
using TRPG.System;

namespace TRPG.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController: ControllerBase
    {
        private readonly Db _context;
        public TestController(Db context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ImportCsv()
        {
            var csvSpells = new ImportSpellCsv(_context);
            csvSpells.ImportCsvFile("./spells.csv");

            return Ok(_context.Spells.ToList());
        }
    }
}
