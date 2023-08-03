using UnityEngine;

namespace Life
{
    public class Specimen : MonoBehaviour
    {
        public bool DisplayDroneIndicator => !specimenProgress.Deposited;
        public SpecimenData SpecimenData => _specimenData;
        [SerializeField] private SpecimenData _specimenData;
        [SerializeField] private GameObject _scanInteraction;
        [SerializeField] private GameObject _pickupInteraction;
        
        [SerializeField] public SpecimenProgress specimenProgress;

        [SerializeField] public string specimenName;

        private void Awake()
        {
            specimenProgress = new SpecimenProgress();
            specimenName = _specimenData.Name;
            if (!_specimenData.RequiresDrilling)
            {
                specimenProgress.DrillComplete = true;
            }
            if (!_specimenData.RequiresDNA)
            {
                specimenProgress.DNAComplete = true;
            }
            if (!_specimenData.RequiresQuantum)
            {
                specimenProgress.QuantumComplete = true;
            }
        }

        public void HandleScanned()
        {
            specimenProgress.Scanned = true;
            _scanInteraction.SetActive(false);
            _pickupInteraction.SetActive(true);
        }
        private void Update()
        {
            specimenProgress = new SpecimenProgress();
            specimenName = _specimenData.Name;
            if (!specimenProgress.Complete && specimenProgress.QuantumComplete && specimenProgress.DNAComplete && specimenProgress.DrillComplete && !specimenProgress.Destroyed)
            {
                specimenProgress.Complete = true;
            }
        }
    }
}