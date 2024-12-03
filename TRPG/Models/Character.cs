using Microsoft.AspNetCore.Components.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TRPG.Enums;
using TRPG.ViewModels;

namespace TRPG.Models
{
    /// <summary>
    /// Character entity
    /// </summary>
    public class Character
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Character Name
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// Character current school year
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// LP
        /// </summary>
        public int LifePoints { get; set; }

        /// <summary>
        /// Max LP
        /// </summary>
        public int MaxLifePoints { get; set; }

        /// <summary>
        /// House
        /// </summary>
        public HouseType House { get; set; }

        /// <summary>
        /// Courage
        /// </summary>
        public int Courage { get; set; }

        /// <summary>
        /// Intelligence
        /// </summary>
        public int Intelligence { get; set; }

        /// <summary>
        /// Loyalty
        /// </summary>
        public int Loyalty { get; set; }

        /// <summary>
        /// Tricking
        /// </summary>
        public int Tricking { get; set; }

        /// <summary>
        /// Potions
        /// </summary>
        public int PotionMagic { get; set; }

        /// <summary>
        /// Charms
        /// </summary>
        public int CharmsAndMetamorphosisMagic { get; set; }

        /// <summary>
        /// Attack
        /// </summary>
        public int AttackAndDefenseMagic { get; set; }

        /// <summary>
        /// Objects
        /// </summary>
        public List<string> Objects { get; set; } = [];

        /// <summary>
        /// Spells
        /// </summary>
        public List<Spell> UnlockedSpells { get; } = [];

        /// <summary>
        /// Wand
        /// </summary>
        public Wand? Wand { get; set; }

        /// <summary>
        /// Owner
        /// </summary>
        [JsonIgnore]
        public User User { get; set; } = null!;

        /// <summary>
        /// Owner Id
        /// </summary>
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        /// <summary>
        /// Determine if character is created, partially created, or not
        /// </summary>
        /// <returns></returns>
        public IsCharacterCreatedVM IsCharacterCreated()
        {
            if (!IsNameSet()) return new(false, 0);
            if(!IsBaseStatsSet()) return new(false, 1);
            if(!IsMagicStatsSet()) return new(false, 2);
            if(!IsWandSet()) return new(false, 3);

            return new(true, 0);
        }

        private bool IsNameSet()
        {
            return !string.IsNullOrEmpty(Name);
        }

        private bool IsBaseStatsSet()
        {
            return !(new[] { Courage, Intelligence, Loyalty, Tricking }.Contains(6));
        }

        private bool IsMagicStatsSet()
        {
            return !(new[] { AttackAndDefenseMagic, CharmsAndMetamorphosisMagic, PotionMagic }.Contains(6));
        }

        private bool IsWandSet()
        {
            return Wand != null;
        }
    }

    /// <summary>
    /// DB settings
    /// </summary>
    public class CharacterEntityTypeConfiguration : IEntityTypeConfiguration<Character>
    {
        /// <summary>
        /// Builder
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Character> builder)
        {
            builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();
            builder.HasOne(x => x.User);

            builder
            .HasKey(x => new { x.Id });

            builder
                .HasMany(x => x.UnlockedSpells).WithMany();
        }
    }
}
