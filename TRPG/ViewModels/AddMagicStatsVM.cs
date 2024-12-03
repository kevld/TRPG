namespace TRPG.ViewModels
{
    /// <summary>
    /// Magic stats
    /// </summary>
    public class AddMagicStatsVM
    {
        /// <summary>
        /// Potions
        /// </summary>
        public int PotionMagic { get; set; } = 0;

        /// <summary>
        /// Charms
        /// </summary>
        public int CharmsAndMetamorphosisMagic { get; set; } = 0;

        /// <summary>
        /// Atk/def
        /// </summary>
        public int AttackAndDefenseMagic { get; set; } = 0;
    }
}
