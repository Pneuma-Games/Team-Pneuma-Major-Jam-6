using Life.MovementControllers;
using UnityEngine;

namespace Life
{
    public class SwitchControlToDrone : MonoBehaviour
    {
        public void Switch()
        {
            var player = FindObjectOfType<PlayerMovementController>(true);
            var drone = FindObjectOfType<DroneMovement>(true);
            drone.gameObject.SetActive(true);
            player.gameObject.SetActive(false);
        }
    }
}