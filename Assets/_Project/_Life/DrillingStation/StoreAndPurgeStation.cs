using Life.Managers;
using UnityEngine;
using Life.InteractionSystem;

namespace Life
{
    public class StoreAndPurgeStation : MockStation
    {
        [SerializeField] private GameObject _animatedProp;
        [SerializeField] private GameObject _dropZone;
        [SerializeField] private GameObject _pickupZone;
        [SerializeField] private StoreAndPurgeStationUI _ui;
        [SerializeField] private CurrentSubject currentSubject;
        private GameManager _gameManager;

        private void Awake()
        {
            _gameManager = GetComponent<GameManager>();
            _animatedProp.SetActive(false);
            _dropZone.SetActive(true);
            _pickupZone.SetActive(false);
        }

        public override void AcceptItem()
        {
            base.AcceptItem();

            _animatedProp.SetActive(true);
            _dropZone.SetActive(false);
            _pickupZone.SetActive(true);
            _ui.SetSpecimenPresent(true);
            if (currentSubject._specimen.specimenProgress.Complete == true && currentSubject._specimen.SpecimenData.MandatorySpecimen == true)
            {
                _gameManager.numberOfStoredMandatorySpecimens++;
            }
        }
            
        
        public override void SpitOutItem()
        {
            if (currentSubject._specimen.specimenProgress.Complete == true && currentSubject._specimen.SpecimenData.MandatorySpecimen == true)
            {
                _gameManager.numberOfStoredMandatorySpecimens--;
            }
            TransportSystem.TransportSystem.StoreItem(_item);
            _item = null;
            _animatedProp.SetActive(false);
            _dropZone.SetActive(true);
            _pickupZone.SetActive(false);
            _ui.SetSpecimenPresent(false);
        }

        public void PurgeItem()
        {
            if (currentSubject._specimen.SpecimenData.MandatorySpecimen == true)
            {
                FindObjectOfType<FiredSequence>().PlayFired();
            }
            ControlTransferTransition controlTransferTransition = gameObject.GetComponentInChildren<ControlTransferTransition>();
            controlTransferTransition.TransferToPlayer();
            Debug.Log("Specimen Purged");
            _item = null;
            _animatedProp.SetActive(false);
            _dropZone.SetActive(true);
            _pickupZone.SetActive(false);
            _ui.SetSpecimenPresent(false);
            currentSubject.SetNull();
        }
    }
}