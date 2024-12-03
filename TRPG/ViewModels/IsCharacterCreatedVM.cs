namespace TRPG.ViewModels
{
    /// <summary>
    /// Is character created
    /// </summary>
    public class IsCharacterCreatedVM
    {
        /// <summary>
        /// Is created
        /// </summary>
        public bool IsCreated { get; set; }
        
        /// <summary>
        /// Current step
        /// </summary>
        public int CurrentStep { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="isCreated"></param>
        /// <param name="currentStep"></param>
        public IsCharacterCreatedVM(bool isCreated, int currentStep)
        {
            IsCreated = isCreated;
            CurrentStep = currentStep;
        }
    }
}
