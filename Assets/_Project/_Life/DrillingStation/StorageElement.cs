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
        //I think it would be best to make this a binary if statement, if there is a specimen in the slot we can just purge it. Reason being, we can forgo the button logic that way.
        //Otherwise we need to set it up like other stations, wher you view it from the front after there has been a specimen placed.
        //which may not be too dificult the more I think about it. I'm just out of time for today. - Olin
        public void DoStore()
        {
            _sampleMesh.SetActive(true);
            var item = TransportSystem.TransportSystem.RetrieveItem().GameObject.GetComponent<Specimen>();
            _station.StoreItem(_index, item);
        }
    }
}