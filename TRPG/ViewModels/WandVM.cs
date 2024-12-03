using System.ComponentModel.DataAnnotations;
using TRPG.Enums;

namespace TRPG.ViewModels
{
    /// <summary>
    /// Wand
    /// </summary>
    public class WandVM
    {
        /// <summary>
        /// Type
        /// </summary>
        public WandHeartType WandHeartType { get; set; } = WandHeartType.None;

        /// <summary>
        /// Wood
        /// </summary>
        public string Wood { get; set; } = "";

        /// <summary>
        /// Rigidity
        /// </summary>
        public string Rigidity { get; set; } = "";

        /// <summary>
        /// Size
        /// </summary>
        public string Size { get; set; } = "";
    }
}
