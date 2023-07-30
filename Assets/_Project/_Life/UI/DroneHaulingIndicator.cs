using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Life
{
    public class DroneHaulingIndicator : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _tmp;

        private void OnEnable()
        {
            _tmp.DOFade(0.0f, 0.25f).SetEase(Ease.InOutQuint).SetLoops(-1, LoopType.Yoyo);
            _tmp.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (TransportSystem.TransportSystem.ItemStored)
            {
                _tmp.gameObject.SetActive(true);
            }
            else
            {
                _tmp.gameObject.SetActive(false);
            }
        }
    }
}