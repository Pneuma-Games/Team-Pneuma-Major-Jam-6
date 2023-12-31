using System.Collections;
using Life.TransportSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Life
{
    public class DrillingStation : ProcessingStationBase
    {
        public UnityEvent onDrillStart;
        public UnityEvent onDrillStop;
        [SerializeField] private DrillUIController _ui;
        [SerializeField] private float _drillTime;
        
        private int _rpm = 1000;
        private bool _lube;
        private bool _working;

        private float _workTime;

        private void Awake()
        {
            if (!_ui)
                _ui = FindObjectOfType<DrillUIController>();
            
            _ui.SetRPM(_rpm);
            _ui.SetStatus("Ready");
            _ui.SetInput("None");
            _ui.SetLube(false);
            _ui.SetError(false);
        }

        public void HandleRpmUpButton()
        {
            if (_working) return;
            _rpm += 1000;
            if (_rpm > 14000) _rpm = 14000;
            _ui.SetRPM(_rpm);
        }
        
        public void HandleRpmDownButton()
        {
            if (_working) return;
            _rpm -= 1000;
            if (_rpm < 1000) _rpm = 1000;
            _ui.SetRPM(_rpm);
        }

        public void HandleGoButton()
        {
            if (_item == null || _working) return;
            StartCoroutine(DrillCoroutine());
        }

        public void HandleLubeButton()
        {
            if (_working) return;
            _lube = !_lube;
            _ui.SetLube(_lube);
        }

        private WaitForSeconds wait = new WaitForSeconds(.15f);
        private WaitForSeconds waitOne = new WaitForSeconds(1f);
        private IEnumerator DrillCoroutine()
        {
            onDrillStart.Invoke();
            var spec = _item.GameObject.GetComponent<Specimen>();
            if (spec.SpecimenData.Volatile)
            {
                FindObjectOfType<ExplosionSequence>().GoBoom();
                yield break;
            }
            
            _working = true;
            _workTime = 0f;
            _ui.SetStatus("Working");
            //var progress = ;
            var result = VerifyDrill();
            if (result)
            {
                spec.specimenProgress.DrillComplete = true;
            }
            else
            {
                spec.specimenProgress.Destroyed = true;
                SpecimenPanel.Instance.IncreaseStrikes();
            }

            while (_workTime < _drillTime)
            {
                yield return wait;
                _workTime += .15f;
                _ui.SetProgress(_workTime / _drillTime);
            }
            _ui.SetProgress(1.0f);

            SpitOutItem();
            if (!result)
            {
                _ui.SetError(true);
                yield return waitOne;
                yield return waitOne;
            }

            yield return waitOne;
            _ui.SetStatus("Ready");
            _ui.SetError(false);
            _ui.SetProgress(0f);
            _working = false;
            onDrillStop.Invoke();
        }
        
        private bool VerifyDrill()
        {
            var specimen = _item.GameObject.GetComponent<Specimen>().SpecimenData;
            return _rpm == specimen.DrillRpm && _lube == specimen.DrillLube && specimen.RequiresDrilling;
        }

        public override void SpitOutItem()
        {
            _ui.SetInput("None");
            base.SpitOutItem();
        }

        public override void AcceptItem()
        {
        }

        public void TakeOverItem(ITransportable item)
        {
            _item = item;
            _ui.SetInput("Present");
        }
    }
}
