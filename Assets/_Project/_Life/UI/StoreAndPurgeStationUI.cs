using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Life
{
    public class StoreAndPurgeStationUI : MonoBehaviour
    {
        [SerializeField] public bool AcceptsInput;

        [SerializeField] private TextMeshProUGUI _specimenOutput;
        //[SerializeField] private TextMeshProUGUI _input;
        //[SerializeField] private TextMeshProUGUI _specimenStatus;
        [SerializeField] private StoreAndPurgeStation _station;
        [SerializeField] public CurrentSubject currentSubject;

        public void SetSpecimenPresent(bool yes)
        {
            _specimenOutput.SetText(yes ? currentSubject._specimen.specimenName: "Empty");
        }
        
        public void HandleButtonClick()
        {
            _station.PurgeItem();
        }

        public void SetAcceptInput(bool yes)
        {
            AcceptsInput = yes;
        }

        
        // Update is called once per frame
        void Update()
        {
            if (!AcceptsInput) return;
        }
    }
}