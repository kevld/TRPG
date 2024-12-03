using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TRPG.Models;

namespace TRPG.Controllers
{
    /// <summary>
    /// User controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly Db _db;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db">DB Context</param>
        public UserController(Db db)
        {
            _db = db;
        }

        /// <summary>
        /// Create an user
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<IActionResult> CreateUser([FromBody] string name)
        {
            try
            {
                User? user;

                user = await _db.Users.FirstOrDefaultAsync(x => x.Name == name);
                if (user != null)
                {
                    return Ok(user);
                }

                user = new User()
                {
                    Name = name
                };

                _db.Users.Add(user);
                await _db.SaveChangesAsync();

                return Ok(user);
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }

        /// <summary>
        /// Get character by its user's id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId}/character")]
        public async Task<IActionResult> GetCharacterIdByUserId(string userId)
        {
            Guid guid = Guid.Parse(userId);
            Character? character = await _db.Characters
                .Include(x => x.Wand)
                .SingleOrDefaultAsync(x => x.UserId == guid);

            if(character == null) return NotFound(userId);

            return Ok(character);
        }
    }
}
