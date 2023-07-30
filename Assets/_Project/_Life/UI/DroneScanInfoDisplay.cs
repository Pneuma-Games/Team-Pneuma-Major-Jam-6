using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Life
{
    public class DroneScanInfoDisplay : MonoBehaviour
    {
        [SerializeField] private GameObject _displayGroup;
        [SerializeField] private TextMeshProUGUI _scanId;
        [SerializeField] private Image _color;

        private void Awake()
        {
            CurrentSubject.OnSubjectChanged += OnSubjectChanged;
        }

        private void OnDestroy()
        {
            CurrentSubject.OnSubjectChanged -= OnSubjectChanged;
        }

        private void OnSubjectChanged()
        {
            if (CurrentSubject.Instance.Specimen == null)
            {
                _displayGroup.SetActive(false);
                return;
            }
            _displayGroup.SetActive(true);
            _scanId.SetText(CurrentSubject.Instance.Specimen.SpecimenData.SpecimenId.ToString());
            switch (CurrentSubject.Instance.Specimen.SpecimenData.Color)
            {
                case SpecimenColor.Red:
                    _color.color = Color.red;
                    break;
                case SpecimenColor.Yellow:
                    _color.color = Color.yellow;
                    break;
                case SpecimenColor.Blue:
                    _color.color = Color.blue;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}