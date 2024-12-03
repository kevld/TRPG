using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TRPG.Enums;

namespace TRPG.Models
{
    /// <summary>
    /// Spell entity
    /// </summary>
    public class Spell
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; } = "";

        /// <summary>
        /// Year of learning
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        [Required]
        public SpellType Type { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [Required]
        public string Name { get; set; } = "";
    }

    /// <summary>
    /// Config
    /// </summary>
    public class SpellEntityTypeConfiguration : IEntityTypeConfiguration<Spell>
    {
        /// <summary>
        /// Settings
        /// </summary>
        /// <param name="builder"></param>
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
