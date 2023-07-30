using UnityEngine;

namespace Life.InteractionSystem
{
    public class StationHasItemCondition : MonoBehaviour, IInteractionCondition
    {
        [SerializeField] private ProcessingStationBase _station;
        [SerializeField] private bool _negate;

        public bool CanInteract()
        {
            if (_negate) return !_station.ProcessingItem;
            return _station.ProcessingItem;
        }

        public string GetErrorMessage()
        {
            if (_negate) return "Requires no item to be in the station.";
            return "Requires an item to be in the station.";
        }
    }
}