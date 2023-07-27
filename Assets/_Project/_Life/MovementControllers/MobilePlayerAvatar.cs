using UnityEngine;

namespace Life.MovementControllers
{
    public class MobilePlayerAvatar : MonoBehaviour
    {
        public static MobilePlayerAvatar Current { private set; get; }

        public Camera MobileCamera { private set; get; }

        private void Awake()
        {
            MobileCamera = GetComponentInChildren<Camera>();
        }

        private void OnEnable()
        {
            Current = this;
        }
    }
}