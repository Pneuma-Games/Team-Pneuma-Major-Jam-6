using UnityEngine;
using UnityEngine.Events;

namespace Life.TransportSystem
{
    /// <summary>
    /// This is an agent to the TransportSystem. Only the system should interact with this class.
    /// For gameplay actions call TransportSystem directly.
    /// </summary>
    public class Transporter : MonoBehaviour
    {
        // Used for spawning dropped items.
        [SerializeField] private Transform _dropPoint;
        [SerializeField] public UnityEvent DronePickup;
        [SerializeField] public UnityEvent DroneDropoff;

        public ITransportable HeldItem { get; private set; }
        
        
        public void Store(ITransportable item)
        {
            Debug.Log(gameObject.tag + " " + (item != null));
            if (gameObject.tag == "Drone" && item != null)
            {
                DronePickup.Invoke();
            } else if (gameObject.tag == "Drone" && item == null)
            {
                //Debug.Log("INvoked drop off");
                DroneDropoff.Invoke();
            }
            HeldItem = item;
            if (item != null) item.GameObject.SetActive(false);
        }
        
        public ITransportable Retrieve()
        {
            var item = HeldItem;
            HeldItem = null;
            return item;
        }

        public void Drop()
        {
            if (HeldItem == null) return;
            var item = Retrieve();
            item.GameObject.transform.position = _dropPoint ? _dropPoint.position : transform.position;
            item.GameObject.SetActive(true);
        }

        private void OnEnable()
        {
            TransportSystem.SetTransporterAsCurrent(this);
        }

        private void OnDisable()
        {
            TransportSystem.ReportTransporterDeactivation(this);
        }
    }
}