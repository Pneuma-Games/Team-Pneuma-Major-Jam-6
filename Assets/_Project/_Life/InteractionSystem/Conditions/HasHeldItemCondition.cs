using UnityEngine;

namespace Life.InteractionSystem
{
    public class HasHeldItemCondition : MonoBehaviour, IInteractionCondition
    {
        public bool CanInteract()
        {
            return TransportSystem.TransportSystem.ItemStored;
        }

        public string GetErrorMessage()
        {
            return "Requires an item to be held.";
        }
    }
}
