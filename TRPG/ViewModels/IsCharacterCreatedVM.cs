namespace TRPG.ViewModels
{
    public class IsCharacterCreatedVM
    {
        public bool IsCreated { get; set; }
        public int CurrentStep { get; set; }

        public IsCharacterCreatedVM(bool isCreated, int currentStep)
        {
            IsCreated = isCreated;
            CurrentStep = currentStep;
        }
    }
}
