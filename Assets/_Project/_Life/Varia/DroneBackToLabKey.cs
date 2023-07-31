using Life.MovementControllers;
using UnityEngine;
using UnityEngine.Events;

namespace Life
{
    public class DroneBackToLabKey : MonoBehaviour
    {
        public UnityEvent OnLeaveDrone;
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                var player = FindObjectOfType<PlayerMovementController>(true);
                player.gameObject.SetActive(true);
                this.gameObject.SetActive(false);
                OnLeaveDrone.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                TransportSystem.TransportSystem.DropItem();
            }
        }
    }
}
