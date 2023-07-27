using UnityEngine;

namespace Life.Varia
{
    public class TransportablePlacementPoint : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        
        public void PlaceTransportedItem()
        {
            var item = TransportSystem.TransportSystem.RetrieveItem();
            item.GameObject.SetActive(true);
            item.GameObject.transform.position = _spawnPoint.position;
        }
    }
}
