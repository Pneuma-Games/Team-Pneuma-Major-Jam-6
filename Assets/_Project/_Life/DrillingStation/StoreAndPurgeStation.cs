using UnityEngine;

namespace Life
{
    public class StoreAndPurgeStation : MockStation
    {
        [SerializeField] private GameObject _animatedProp;
        [SerializeField] private GameObject _dropZone;
        [SerializeField] private GameObject _pickupZone;
        [SerializeField] private StoreAndPurgeStationUI _ui;
        
        private void Awake()
        {
            _animatedProp.SetActive(false);
            _dropZone.SetActive(true);
            _pickupZone.SetActive(false);
        }

        public override void AcceptItem()
        {
            base.AcceptItem();
            _animatedProp.SetActive(true);
            _dropZone.SetActive(false);
            _pickupZone.SetActive(false);
            _ui.SetSpecimenPresent(true);
        }
            
        
        public override void SpitOutItem()
        {
            TransportSystem.TransportSystem.StoreItem(_item);
            _item = null;
            _animatedProp.SetActive(false);
            _dropZone.SetActive(true);
            _pickupZone.SetActive(false);
            _ui.SetSpecimenPresent(false);
        }

        public void PurgeItem()
        {
            //todo this is where we can put the logic for destroying a specimen
            Debug.Log("Specimen Purged");
            _item = null;
            _animatedProp.SetActive(false);
            _dropZone.SetActive(true);
            _pickupZone.SetActive(false);
            _ui.SetSpecimenPresent(false);
        }
    }
}