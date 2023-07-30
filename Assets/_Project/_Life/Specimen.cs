using UnityEngine;

namespace Life
{
    public class Specimen : MonoBehaviour
    {
        public bool DisplayDroneIndicator => !SpecimenProgress.Deposited;
        public SpecimenData SpecimenData => _specimenData;
        [SerializeField] private SpecimenData _specimenData;
        [SerializeField] private GameObject _scanInteraction;
        [SerializeField] private GameObject _pickupInteraction;
        
        [HideInInspector] public SpecimenProgress SpecimenProgress;

        private void Awake()
        {
            SpecimenProgress = new SpecimenProgress();
        }

        public void HandleScanned()
        {
            SpecimenProgress.Scanned = true;
            _scanInteraction.SetActive(false);
            _pickupInteraction.SetActive(true);
        }
    }
}