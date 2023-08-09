using System;
using UnityEngine;

namespace Life
{
    public class CurrentSubject : MonoBehaviour
    {
        public static Action OnSubjectChanged = delegate {  };
        public static CurrentSubject Instance;

        public Specimen Specimen
        {
            set
            {
                _specimen = value;
                OnSubjectChanged.Invoke();
            }
            get => _specimen;
        }

        public Specimen _specimen;

        private void Awake()
        {
            Instance = this;
        }

        public void SetNull()
        {
            Instance.Specimen = null;
        }
    }
}