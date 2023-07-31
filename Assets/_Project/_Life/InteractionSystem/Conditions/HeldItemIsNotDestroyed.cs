using UnityEngine;

namespace Life.InteractionSystem
{
    public class HeldItemIsNotDestroyed : MonoBehaviour, IInteractionCondition
    {
        public bool CanInteract()
        {
            return TransportSystem.TransportSystem.ItemStored && !TransportSystem.TransportSystem.GetStoredSpecimenProgress().Destroyed;
        }

        public string GetErrorMessage()
        {
            return "The sample you are holding is destroyed.";
        }
    }
}