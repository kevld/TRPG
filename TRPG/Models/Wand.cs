using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TRPG.Enums;

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

        public Character Character { get; set; }
        public int CharacterId { get; set; }
    }
}
