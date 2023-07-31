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
            OnPlayerSpawned.Invoke();
            MobileCamera = GetComponentInChildren<Camera>();
        }

        private void OnEnable()
        {
            Current = this;
        }
    }
}