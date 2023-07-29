using UnityEngine;

namespace Life
{
    public class Specimen : MonoBehaviour
    {
        public SpecimenData SpecimenData => _specimenData;
        [SerializeField] private SpecimenData _specimenData;
        public SpecimenProgress SpecimenProgress;

        private void Awake()
        {
        }



    }
}