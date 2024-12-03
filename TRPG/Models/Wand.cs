using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TRPG.Enums;
using System.Text.Json.Serialization;

namespace TRPG.Models
{
    /// <summary>
    /// Wand entity
    /// </summary>
    public class Wand
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        [Required]
        public WandHeartType WandHeartType { get; set; }

        /// <summary>
        /// Wood
        /// </summary>
        [Required]
        public string Wood { get; set; } = "";

        /// <summary>
        /// Rigidity
        /// </summary>
        [Required]
        public string Rigidity { get; set; } = "";

        /// <summary>
        /// Size
        /// </summary>
        [Required]
        public string Size { get; set; } = "";

        /// <summary>
        /// Character
        /// </summary>
        [JsonIgnore]
        public Character? Character { get; set; }

        /// <summary>
        /// Character id
        /// </summary>
        [ForeignKey("Character")]
        public int CharacterId { get; set; }
    }

    /// <summary>
    /// Config
    /// </summary>
    public class WandEntityTypeConfiguration : IEntityTypeConfiguration<Wand>
    {
        /// <summary>
        /// Settings
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Wand> builder)
        {
            builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();
            builder.HasOne(x => x.Character);

            builder
            .HasKey(x => new { x.Id });
        }
    }
}
