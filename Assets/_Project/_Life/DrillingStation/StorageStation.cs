using System;
using System.Linq;
using UnityEngine;

namespace Life
{
    public class StorageStation : MonoBehaviour
    {
        public static Action OnCollectionComplete = delegate { };
        private Specimen[] _specimens = new Specimen[24];
        [SerializeField] private SpecimenData[] _requiredSpecimens;

        public void StoreItem(int idx, Specimen s)
        {
            Debug.Log("Storing item in slot " + idx + ".");
            s.specimenProgress.Stored = true;
            _specimens[idx] = s;
            CurrentSubject.Instance.Specimen = null;
            CheckVictoryCondition();
        }

        private void CheckVictoryCondition()
        {
            var required = _requiredSpecimens.ToList();
            for (int i = 0; i < _specimens.Length; i++)
            {
                if (!_specimens[i]) continue;
                if (required.Contains(_specimens[i].SpecimenData))
                {
                    required.Remove(_specimens[i].SpecimenData);
                }
            }
            
            if (required.Count == 0) OnCollectionComplete?.Invoke();
        }
    }
}