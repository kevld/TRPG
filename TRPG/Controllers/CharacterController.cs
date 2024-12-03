using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TRPG.Enums;
using TRPG.Models;
using TRPG.ViewModels;

namespace TRPG.Controllers
{
    /// <summary>
    /// COntroller characet
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly Db _db;

        public CharacterController(Db db)
        {
            _db = db;
        }

        /// <summary>
        /// perso par nom
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetCharacterByUserId(string userId)
        {
            Guid id = Guid.Parse(userId);
            var character = await _db.Characters.Include(x => x.Wand).SingleOrDefaultAsync(c => c.UserId == id);

            return Ok(character);
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> CreateCharacter(string userId, [FromBody] string name)
        {
            User user = await _db.Users
                .Include(x => x.Character)
                .FirstAsync(x => x.Id == Guid.Parse(userId));
            if (user == null)
                return NotFound(userId);
            if(user.Character != null)
            {
                return Ok(user.Character);
            }

            Character character = new()
            {
                AttackAndDefenseMagic = 6,
                CharmsAndMetamorphosisMagic = 6,
                Courage = 6,
                Intelligence = 6,
                Loyalty = 6,
                Name = name,
                PotionMagic = 6,
                Tricking = 6,
                Year = 1,
                User = user
            };

            _db.Characters.Add(character);
            await _db.SaveChangesAsync();
            return Ok(character);
        }

        [HttpPut("{characterId}/set-house")]
        public async Task<IActionResult> SetHouse(int characterId, [FromBody] int houseType)
        {
            Character character = await _db.Characters
                .Include(x => x.Wand)
                .SingleOrDefaultAsync(x => x.Id == characterId);

            character.House = (HouseType)houseType;

            await _db.SaveChangesAsync();

            return Ok(character);
        }

        [HttpPut("{characterId}/init-stat/base")]
        public async Task<IActionResult> AddBaseStats(int characterId, AddBaseStatsVM statsVM)
        {
            Character character = await _db.Characters
                .Include(x => x.Wand)
                .SingleOrDefaultAsync(x => x.Id == characterId);

            character.Courage += statsVM.Courage;
            character.Loyalty += statsVM.Loyalty;
            character.Intelligence += statsVM.Intelligence;
            character.Tricking += statsVM.Tricking;

            character.LifePoints = character.MaxLifePoints = character.Courage + character.Loyalty + character.Intelligence + character.Tricking;

            await _db.SaveChangesAsync();

            return Ok(character);
        }

        [HttpPut("{characterId}/init-stat/magic")]
        public async Task<IActionResult> AddMagicStats(int characterId, AddMagicStatsVM statsVM)
        {
            Character character = await _db.Characters
                .Include(x => x.Wand)
                .SingleOrDefaultAsync(x => x.Id == characterId);

            character.PotionMagic += statsVM.PotionMagic;
            character.CharmsAndMetamorphosisMagic += statsVM.CharmsAndMetamorphosisMagic;
            character.AttackAndDefenseMagic += statsVM.AttackAndDefenseMagic;

            await _db.SaveChangesAsync();

            return Ok(character);
        }

        [HttpPut("{characterId}/unlock-spell")]
        public async Task<IActionResult> UnlockSpell(int characterId, [FromBody] int spellId)
        {
            Character character = await _db.Characters
                .Include(x => x.Wand)
                .SingleOrDefaultAsync(x => x.Id == characterId);
            Spell spell = await _db.Spells.FindAsync(spellId);

            character.UnlockedSpells.Add(spell);

            await _db.SaveChangesAsync();

            return Ok(character);
        }

        [HttpPut("{characterId}/add-stat")]
        public async Task<IActionResult> AddStatOn(int characterId, string statName)
        {
            Character character = await _db.Characters
                .Include(x => x.Wand)
                .SingleOrDefaultAsync(x => x.Id == characterId);

            PropertyInfo propertyInfo = character.GetType().GetProperty(statName);

            if (propertyInfo != null && propertyInfo.CanWrite)
            {
                int currentValue = (int)propertyInfo.GetValue(character);
                propertyInfo.SetValue(character, currentValue + 1);
            }
            else
            {
                throw new Exception("Property not found");
            }

            await _db.SaveChangesAsync();

            return Ok(character);
        }

        [HttpPut("{characterId}/add-lp")]
        public async Task<IActionResult> AddLifePoints(int characterId, [FromBody] int amount)
        {
            Character character = await _db.Characters
                .Include(x => x.Wand)
                .SingleOrDefaultAsync(x => x.Id == characterId);

            character.LifePoints += amount;

            await _db.SaveChangesAsync();

            return Ok(character);
        }

        [HttpPut("{characterId}/object/add")]
        public async Task<IActionResult> AddObjectToInventory(int characterId, [FromBody] string objectName)
        {
            Character character = await _db.Characters
                .Include(x => x.Wand)
                .SingleOrDefaultAsync(x => x.Id == characterId);

            character.Objects.Add(objectName);

            await _db.SaveChangesAsync();

            return Ok(character);
        }

        [HttpPut("{characterId}/object/use")]
        public async Task<IActionResult> UseObjectFromInventory(int characterId, [FromBody] string objectName)
        {
            Character character = await _db.Characters
                .Include(x => x.Wand)
                .SingleOrDefaultAsync(x => x.Id == characterId);

            character.Objects.Remove(objectName);

            await _db.SaveChangesAsync();

            return Ok(character);
        }

        [HttpGet("{id}/created")]
        public async Task<IActionResult> IsCharacterCreated(int id)
        {
            Character character = await _db.Characters.Include(x => x.Wand).FirstOrDefaultAsync(x => x.Id == id);
            if (character == null)
                return Ok(new IsCharacterCreatedVM(false, 0));

            return Ok(character.IsCharacterCreated());
        }

        [HttpPost("{id}/wand/add")]
        public async Task<IActionResult> AddWand(int id, WandVM wand)
        {
            Character character = await _db.Characters.FindAsync(id);

            Wand wandModel = new Wand()
            {
                Rigidity = wand.Rigidity,
                Size = wand.Size,
                WandHeartType = wand.WandHeartType,
                Wood = wand.Wood,
                Character = character
            };

            _db.Wands.Add(wandModel);

            await _db.SaveChangesAsync();

            return Ok(character);
        }
    }
}
