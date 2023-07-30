using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Life
{
    public class ScanUIController : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _cGroup;        
        [SerializeField] private TextMeshProUGUI _percentage;
        [SerializeField] private Image _progressIndicator;
        [SerializeField] private TextMeshProUGUI _lostWarning;

        public void EnableScan()
        {
            DOTween.Kill(_cGroup, false);
            _cGroup.DOFade(1.0f, .33f);
        }
        
        public void DisableScan()
        {
            DOTween.Kill(_cGroup, false);
            _cGroup.DOFade(0.0f, .33f);
            SetLostWarning(false);
        }

        public void SetProgress(float progress)
        {
            progress = Mathf.Clamp01(progress);
            _progressIndicator.fillAmount = progress;
            _percentage.SetText(Mathf.FloorToInt(progress * 100).ToString()+ " %");
        }
        
        public void SetLostWarning(bool active)
        {
            _lostWarning.gameObject.SetActive(active);
        }
    }
}
