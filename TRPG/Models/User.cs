using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TRPG.Models
{
    /// <summary>
    /// User Entity
    /// </summary>
    public class User
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [Required]
        public string Name { get; set; } = "";

        /// <summary>
        /// Character
        /// </summary>
        public Character? Character { get; set; }
    }

    /// <summary>
    /// Settings
    /// </summary>
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        /// <summary>
        /// Settings
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();

            builder.HasIndex(x => x.Name).IsUnique();

            builder
            .HasKey(x => new { x.Id });
        }
    }
}
