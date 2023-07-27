using UnityEngine;

namespace Life.InteractionSystem
{
    public class TransporterReadyForItem : MonoBehaviour, IInteractionCondition
    {
        public bool CanInteract()
        {
            return !TransportSystem.TransportSystem.ItemStored && TransportSystem.TransportSystem.TransporterAvailable;
        }
    }
}