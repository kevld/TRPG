using Microsoft.AspNetCore.Components.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TRPG.Enums;
using TRPG.ViewModels;

namespace TRPG.Models
{
    public class Character
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; } = "";
        public int Year { get; set; }

        public int LifePoints { get; set; }

        public int MaxLifePoints { get; set; }

        public HouseType House { get; set; }

        public int Courage { get; set; }

        public int Intelligence { get; set; }
        public int Loyalty { get; set; }
        public int Tricking { get; set; }

        public int PotionMagic { get; set; }
        public int CharmsAndMetamorphosisMagic { get; set; }
        public int AttackAndDefenseMagic { get; set; }

        public List<string> Objects { get; set; } = [];

        public List<Spell> UnlockedSpells { get; } = [];

        public Wand? Wand { get; set; }

        public User User { get; set; } = null!;
        public Guid UserId { get; set; }

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

    public class CharacterEntityTypeConfiguration : IEntityTypeConfiguration<Character>
    {
        public void Configure(EntityTypeBuilder<Character> builder)
        {
            builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();

            builder
            .HasKey(x => new { x.Name });

            builder
                .HasMany(x => x.UnlockedSpells).WithMany();
        }
    }
}
