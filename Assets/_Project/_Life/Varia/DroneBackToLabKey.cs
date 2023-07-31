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
                OnLeaveDrone.Invoke();
                var player = FindObjectOfType<PlayerMovementController>(true);
                player.gameObject.SetActive(true);
                this.gameObject.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                TransportSystem.TransportSystem.DropItem();
            }
        }
    }
}
