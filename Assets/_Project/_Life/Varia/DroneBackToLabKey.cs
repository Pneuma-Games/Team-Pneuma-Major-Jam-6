using Life.MovementControllers;
using UnityEngine;

namespace Life
{
    public class DroneBackToLabKey : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                var player = FindObjectOfType<PlayerMovementController>(true);
                player.gameObject.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }

        private void OnEnable()
        {
            var audio = AudioManager.Instance;
            if (!audio) return;
            audio.PlayDroneAmbient();
            audio.EnterDroneFeed();
        }

        private void OnDisable()
        {
            var audio = AudioManager.Instance;
            if (!audio) return;
            audio.StopDroneAmbient();
            audio.ExitDroneFeed();
        }
    }
}
