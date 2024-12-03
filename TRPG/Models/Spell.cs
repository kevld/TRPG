using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TRPG.Enums;

namespace TRPG.Models
{
    public class Spell
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Description { get; set; } = "";

        public int Year { get; set; }

        [Required]
        public SpellType Type { get; set; }

        [Required]
        public string Name { get; set; } = "";
    }

    public class SpellEntityTypeConfiguration : IEntityTypeConfiguration<Spell>
    {
        public void Configure(EntityTypeBuilder<Spell> builder)
        {
            builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();

            builder
            .HasKey(x => new { x.Name });
        }
    }
}
