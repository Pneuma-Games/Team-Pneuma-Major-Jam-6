using UnityEngine;

namespace Life
{
    public class DNAStation : MockStation
    {
        [SerializeField] private GameObject _animatedProp;
        [SerializeField] private GameObject _dropZone;
        [SerializeField] private GameObject _pickupZone;
        [SerializeField] private DNAStationUI _ui;
        
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
            _ui.SetSpecimenOutput(_item.GameObject.GetComponent<Specimen>().SpecimenData.DbKey.ToString());
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
            var required = spc.SpecimenData.RequiresDNA;
            var ok = _ui.Code == spc.SpecimenData.SequenceDecoded.ToString();
            var drillOK = !spc.SpecimenData.RequiresDrilling || (spc.SpecimenData.RequiresDrilling && spc.specimenProgress.DrillComplete);
            if (ok && required && drillOK)
            {
                _ui.ShowSuccess();
                spc.specimenProgress.DNAComplete = true;
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