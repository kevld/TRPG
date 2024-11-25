using System.ComponentModel.DataAnnotations;
using TRPG.Enums;

namespace TRPG.ViewModels
{
    public class WandVM
    {
        public WandHeartType WandHeartType { get; set; }
        public string Wood { get; set; }
        public string Rigidity { get; set; }
        public string Size { get; set; }
    }
}
