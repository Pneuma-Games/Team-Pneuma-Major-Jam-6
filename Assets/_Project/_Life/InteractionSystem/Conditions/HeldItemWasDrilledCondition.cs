using UnityEngine;

namespace Life.InteractionSystem
{
    public class HeldItemWasDrilledCondition : MonoBehaviour, IInteractionCondition
    {
        public bool CanInteract()
        {
            return TransportSystem.TransportSystem.ItemStored &&
                   TransportSystem.TransportSystem.GetStoredSpecimenProgress().DrillComplete;
        }

        public string GetErrorMessage()
        {
            return "Requires a drilled specimen to be held.";
        }
    }
}