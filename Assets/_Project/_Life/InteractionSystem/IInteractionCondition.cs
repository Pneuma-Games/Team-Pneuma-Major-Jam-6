namespace Life.InteractionSystem
{
    public interface IInteractionCondition
    {
        bool CanInteract();
        string GetErrorMessage();
    }
}