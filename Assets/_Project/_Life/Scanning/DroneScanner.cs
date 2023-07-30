using UnityEngine;

namespace Life.Scanning
{
    public class DroneScanner : MonoBehaviour
    {
        [SerializeField] private float _focusLostTimeToReset = 2.5f;
        [SerializeField] private float _scanPerSecond = 0.28f;
        [SerializeField] private ScanUIController _ui;
        
        private ScannableItem _currentFocus;
        private float _progress;
        private float _focusLostTimer;
        private bool _inProgress;
        
        private void OnEnable()
        {
            ScannableItem.OnFocus += ItemFocusHandler;
            ScannableItem.OnFocusLost += ItemFocusLostHandler;
            _ui.SetLostWarning(false);
        }

        private void OnDisable()
        {
            ScannableItem.OnFocus -= ItemFocusHandler;
            ScannableItem.OnFocusLost -= ItemFocusLostHandler;
        }

        private void ItemFocusLostHandler(ScannableItem obj)
        {
            if (_currentFocus == null) return;
            _focusLostTimer = _focusLostTimeToReset;
            _inProgress = false;
            _ui.SetLostWarning(true);
        }

        private void ItemFocusHandler(ScannableItem obj)
        {
            if (_focusLostTimer > 0)
            {
                if (obj != _currentFocus)
                {
                    _currentFocus = obj;
                }
                _focusLostTimer = -1;
                _inProgress = true;
                _ui.SetLostWarning(false);
                return;
            }

            if (_currentFocus == null)
            {
                _ui.EnableScan();
            }

            _currentFocus = obj;
            _inProgress = true;
        }

        private void Update()
        {
            if (_inProgress)
            {
                _progress += _scanPerSecond * Time.deltaTime;
                _ui.SetProgress(_progress);
                if (_progress >= 1.0f)
                {
                    _inProgress = false;
                    _progress = 0f;
                    _ui.DisableScan();
                    _currentFocus.ScanComplete();
                    _currentFocus = null;
                }
            }

            if (_focusLostTimer > 0)
            {
                _focusLostTimer -= Time.deltaTime;
                if (_focusLostTimer <= 0)
                {
                    _progress = 0f;
                    _ui.SetProgress(0);
                    _ui.DisableScan();
                    _currentFocus = null;
                }
            }
        }
    }
}