using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TRPG.Enums;
using TRPG.Models;
using TRPG.ViewModels;

namespace TRPG.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly Db _db;

        public CharacterController(Db db)
        {
            _db = db;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetCharacterByName(string name)
        {
            var character = await _db.Characters.SingleOrDefaultAsync(c => c.Name == name);

            return Ok(character);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCharacter([FromBody] Guid userId, [FromBody] string name)
        {
            User user = await _db.Users.SingleOrDefaultAsync(x => x.Id == userId);
            if (user == null)
                return NotFound(userId);

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
        public async Task<IActionResult> SetHouse(int characterId, [FromBody] HouseType houseType)
        {
            Character character = await _db.Characters.FindAsync(characterId);

            character.House = houseType;

            await _db.SaveChangesAsync();

            return Ok(character);
        }

        [HttpPut("{characterId}/init-stat/base")]
        public async Task<IActionResult> AddBaseStats(int characterId, AddBaseStatsVM statsVM)
        {
            Character character = await _db.Characters.FindAsync(characterId);

            character.Courage += statsVM.Courage;
            character.Loyalty += statsVM.Loyalty;
            character.Intelligence += statsVM.Intelligence;
            character.Tricking += statsVM.Tricking;

            await _db.SaveChangesAsync();

            return Ok(character);
        }

        [HttpPut("{characterId}/init-stat/magic")]
        public async Task<IActionResult> AddMagicStats(int characterId, AddMagicStatsVM statsVM)
        {
            Character character = await _db.Characters.FindAsync(characterId);

            character.PotionMagic += statsVM.PotionMagic;
            character.CharmsAndMetamorphosisMagic += statsVM.CharmsAndMetamorphosisMagic;
            character.AttackAndDefenseMagic += statsVM.AttackAndDefenseMagic;

            await _db.SaveChangesAsync();

            return Ok(character);
        }

        [HttpPut("{characterId}/unlock-spell")]
        public async Task<IActionResult> UnlockSpell(int characterId, [FromBody] int spellId)
        {
            Character character = await _db.Characters.FindAsync(characterId);
            Spell spell = await _db.Spells.FindAsync(spellId);

            character.UnlockedSpells.Add(spell);

            await _db.SaveChangesAsync();

            return Ok(character);
        }

        [HttpPut("{characterId}/add-stat")]
        public async Task<IActionResult> AddStatOn(int characterId, string statName)
        {
            Character character = await _db.Characters.FindAsync(characterId);

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
        public async Task<IActionResult> AddStatOn(int characterId, [FromBody] int amount)
        {
            Character character = await _db.Characters.FindAsync(characterId);

            character.LifePoints += amount;

            await _db.SaveChangesAsync();

            return Ok(character);
        }

        [HttpPut("{characterId}/object/add")]
        public async Task<IActionResult> AddObjectToInventory(int characterId, [FromBody] string objectName)
        {
            Character character = await _db.Characters.FindAsync(characterId);

            character.Objects.Add(objectName);

            await _db.SaveChangesAsync();

            return Ok(character);
        }

        [HttpPut("{characterId}/object/use")]
        public async Task<IActionResult> UseObjectFromInventory(int characterId, [FromBody] string objectName)
        {
            Character character = await _db.Characters.FindAsync(characterId);

            character.Objects.Remove(objectName);

            await _db.SaveChangesAsync();

            return Ok(character);
        }
    }
}
