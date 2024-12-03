using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TRPG.Enums;
using System.Text.Json.Serialization;

namespace TRPG.Models
{
    public class Wand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public WandHeartType WandHeartType { get; set; }

        [Required]
        public string Wood { get; set; }

        [Required]
        public string Rigidity { get; set; }

        [Required]
        public string Size { get; set; }

        [JsonIgnore]
        public Character Character { get; set; }

        [ForeignKey("Character")]
        public int CharacterId { get; set; }
    }

    public class WandEntityTypeConfiguration : IEntityTypeConfiguration<Wand>
    {
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
