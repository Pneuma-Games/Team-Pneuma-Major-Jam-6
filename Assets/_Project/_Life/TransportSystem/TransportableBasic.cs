using UnityEngine;

namespace Life.TransportSystem
{
    public class TransportableBasic : MonoBehaviour, ITransportable
    {
        public GameObject GameObject => gameObject;
        public bool StoreItem()
        {
            if (TransportSystem.TransporterAvailable && !TransportSystem.ItemStored)
            {
                TransportSystem.StoreItem(this);
                return true;
            }

            return false;
        }
    }
}