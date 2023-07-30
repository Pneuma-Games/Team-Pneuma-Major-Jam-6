using UnityEngine;

namespace Life.TransportSystem
{
    public class TransportableComplex : MonoBehaviour, ITransportable
    {
        [SerializeField] private GameObject _rootGameObject;
        
        public GameObject GameObject => _rootGameObject;
        public bool StoreItem()
        {
            if (TransportSystem.TransporterAvailable && !TransportSystem.ItemStored)
            {
                TransportSystem.StoreItem(this);
                return true;
            }

            return false;
        }

        // Exists for the purpose of referencing to UnityEvents in inspector
        // - the engine doesn't serialize non-void return types.
        public void StoreItemViaEvent()
        {
            StoreItem();
        }
    }
}