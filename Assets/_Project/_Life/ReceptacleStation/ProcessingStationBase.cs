using Life.TransportSystem;
using UnityEngine;

namespace Life
{
    public class ProcessingStationBase : MonoBehaviour 
    {
        public Transform ItemSpawnPoint;
        public bool ProcessingItem => _item != null;
        protected ITransportable _item;
        
        public virtual void AcceptItem()
        {
            if (TransportSystem.TransportSystem.ItemStored)
            {
                _item = TransportSystem.TransportSystem.RetrieveItem();
            }
        }

        public virtual void SpitOutItem()
        {
            ParticleSystem particleEmitter = _item.GameObject.GetComponentInChildren<ParticleSystem>();
            Debug.Log("Particle Emitter: " + particleEmitter.name);
            particleEmitter.gameObject.SetActive(false);
            _item.GameObject.transform.position = ItemSpawnPoint.position;
            _item.GameObject.SetActive(true);
            _item = null;
        }

    }
}