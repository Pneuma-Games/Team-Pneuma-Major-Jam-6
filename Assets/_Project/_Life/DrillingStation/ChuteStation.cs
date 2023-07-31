using Life.TransportSystem;
using UnityEngine;

namespace Life
{
    public class ChuteStation : ProcessingStationBase
    {
        [SerializeField] private DrillingStation _drillStation;
        [SerializeField] private DispenserUIController _ui;

        public void HandleDrillButton()
        { 
            _drillStation.TakeOverItem(_item);
            _item = null;
        }

        public void HandleDispenseButton()
        {
            SpitOutItem();
        }
        
        public override void AcceptItem()
        {
        }

        public void TakeOverItem(ITransportable item)
        {
            _item = item;
            _ui.DisplayChoice();
        }

        public override void SpitOutItem()
        {
            var specimen = _item.GameObject.GetComponent<Specimen>();
            if (specimen.SpecimenData.Volatile)
            {
                FindObjectOfType<ExplosionSequence>().GoBoom();
                return;
            }
            
            if (specimen.SpecimenData.RequiresDrilling)
            {
                specimen.SpecimenProgress.Destroyed = true;
                SpecimenPanel.Instance.IncreaseStrikes();
            }
            base.SpitOutItem();
        }
    }
}