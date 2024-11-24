using Microsoft.AspNetCore.Mvc;
using TRPG.Models;

namespace TRPG.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly Db _db;

        public UserController(Db db)
        {
            _db = db;
        }

        public async Task<IActionResult> CreateUser([FromBody] string name)
        {
            try
            {
                var user = new User()
                {
                    Name = name
                };

                _db.Users.Add(user);
                await _db.SaveChangesAsync();

                return Ok(user);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
