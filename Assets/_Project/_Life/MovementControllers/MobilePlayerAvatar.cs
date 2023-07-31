using UnityEngine;
using UnityEngine.Events;

namespace Life.MovementControllers
{
    public class MobilePlayerAvatar : MonoBehaviour
    {
        public static MobilePlayerAvatar Current { private set; get; }
        public UnityEvent OnPlayerSpawned;

        public Camera MobileCamera { private set; get; }

        private void Awake()
        {
            
            MobileCamera = GetComponentInChildren<Camera>();
        }

        private void OnEnable()
        {
            OnPlayerSpawned.Invoke();
            Current = this;
        }
    }
}