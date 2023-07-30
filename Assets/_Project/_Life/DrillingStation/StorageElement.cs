using UnityEngine;

namespace Life
{
    public class StorageElement : MonoBehaviour
    {
        private StorageStation _station;
        [SerializeField] private int _index;
        [SerializeField] private GameObject _sampleMesh;
        
        
        private void Awake()
        {
            _station = GetComponentInParent<StorageStation>();
            _sampleMesh.SetActive(false);
        }

        public void DoStore()
        {
            _sampleMesh.SetActive(true);
            var item = TransportSystem.TransportSystem.RetrieveItem().GameObject.GetComponent<Specimen>();
            _station.StoreItem(_index, item);
        }
    }
}