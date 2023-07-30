using System;
using UnityEngine;

namespace Life.Scanning
{
    public class ScannableItem : MonoBehaviour
    {
        public static Action<ScannableItem> OnFocus;
        public static Action<ScannableItem> OnFocusLost;
        
        [SerializeField] private Specimen _specimen;

        public void ScanComplete()
        {
            _specimen.HandleScanned();
        }
        
        public void Focus()
        {
            OnFocus?.Invoke(this);
        }
        
        public void FocusLost()
        {
            OnFocusLost?.Invoke(this);
        }
    }
}