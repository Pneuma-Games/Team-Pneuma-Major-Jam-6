using UnityEngine;

namespace Life
{
    public class QuantumStation : MockStation
    {
        [SerializeField] private GameObject _animatedProp;
        [SerializeField] private GameObject _dropZone;
        [SerializeField] private GameObject _pickupZone;
        [SerializeField] private QuantumStationUI _ui;
        
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
            _ui.ShowInput();
            _ui.ResetInput();
        }

        public void ProcessItem()
        {
            if (!_item.GameObject) return;
            var spc = _item.GameObject.GetComponent<Specimen>();
            var dnaOk = !spc.SpecimenData.RequiresDNA || (spc.SpecimenData.RequiresDNA && spc.specimenProgress.DNAComplete);
            var drillOk = !spc.SpecimenData.RequiresDrilling || (spc.SpecimenData.RequiresDrilling && spc.specimenProgress.DrillComplete);
            var ok = _ui.Code == spc.SpecimenData.QuantumKey.ToString();
            if (ok && dnaOk && drillOk)
            {
                _ui.ShowSuccess();
                spc.specimenProgress.QuantumComplete = true;
            }
            else
            {
                _ui.ShowError();
                spc.specimenProgress.Destroyed = true;
                SpecimenPanel.Instance.IncreaseStrikes();
            }
            _pickupZone.SetActive(true);
        }
    }
}