using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Life
{
    public class DrillUIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _inputTMP;
        [SerializeField] private TextMeshProUGUI _statusTMP;
        [SerializeField] private TextMeshProUGUI _rpmTMP;
        [SerializeField] private TextMeshProUGUI _lubeTMP;
        [SerializeField] private TextMeshProUGUI _errorTMP;

        private int _currentRpm;
        private int _targetRpm;
        
        public void SetInput(string input)
        {
            _inputTMP.SetText(input);
            BlinkText(_inputTMP);
        }
        
        public void SetStatus(string status)
        {
            _statusTMP.SetText(status);
            BlinkText(_statusTMP);
        }
        
        public void SetRPM(int rpm)
        {
            _targetRpm = rpm;
            //BlinkText(_rpmTMP);
        }
        
        public void SetLube(bool active)
        {
            _lubeTMP.SetText(active ? "Applied" : "Off");
            BlinkText(_lubeTMP);
        }

        public void SetError(bool active)
        {
            _errorTMP.gameObject.SetActive(active);
        }
        
        private void BlinkText(TextMeshProUGUI text)
        {
            DOTween.Kill(text, true);
            text.DOFade(0f, 0.1f).SetLoops(4, LoopType.Yoyo).SetEase(Ease.InOutQuint).OnComplete(() =>
            {
                text.DOFade(1.0f, 0.001f);
            });
        }

        private void Update()
        {
            if (_currentRpm != _targetRpm)
            {
                _currentRpm = Mathf.RoundToInt(Mathf.Lerp(_currentRpm, _targetRpm, 0.023f));
                if (Mathf.Abs(_currentRpm - _targetRpm) <= 30) _currentRpm = _targetRpm;
                _rpmTMP.SetText(_currentRpm.ToString());
            }
        }
    }
}
