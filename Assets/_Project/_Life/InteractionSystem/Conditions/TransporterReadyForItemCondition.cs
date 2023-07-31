using UnityEngine;

namespace Life.InteractionSystem
{
    public class TransporterReadyForItemCondition : MonoBehaviour, IInteractionCondition
    {
        public bool CanInteract()
        {
            return !TransportSystem.TransportSystem.ItemStored && TransportSystem.TransportSystem.TransporterAvailable;
        }

        public string GetErrorMessage()
        {
            return "Requires no item to be held.";
        }
    }
}