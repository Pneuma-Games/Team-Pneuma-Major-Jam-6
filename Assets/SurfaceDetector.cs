using UnityEngine;

namespace Monster.Player
{
    [DefaultExecutionOrder(100)]
    public class SurfaceDetector : MonoBehaviour
    {
        public bool Above;
        public bool Below;
        
        public RaycastHit AboveHit;
        public RaycastHit BelowHit;

        public float DetectionDistance = 0.01f;
        public float SphereRadius = 0.33f;

        private void Update()
        {
            var tPos = transform.position;
            Above = Physics.SphereCast(tPos, SphereRadius, Vector3.up, out AboveHit, DetectionDistance);
            Below = Physics.SphereCast(tPos, SphereRadius, Vector3.down, out BelowHit, DetectionDistance);
        }

        /*
        private void OnGUI()
        {
            var xPos = Screen.width - 110;
            GUI.TextArea(new Rect(xPos, 10, 100, 30), $"Left: {Left}");
            GUI.TextArea(new Rect(xPos, 40, 100, 30), $"Right: {Right}");
            GUI.TextArea(new Rect(xPos, 70, 100, 30), $"Above: {Above}");
        }
        */
    }
}