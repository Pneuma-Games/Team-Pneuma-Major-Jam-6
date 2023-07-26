using UnityEngine;

namespace Life
{
    public class DroneTilt : MonoBehaviour
    {
        [SerializeField] private float _maxTilt;
        [SerializeField] private Rigidbody _rb;
        
        // Update is called once per frame
        void Update()
        {
            var vel = _rb.velocity;
            var fwd = _rb.transform.forward;
            var dot = Vector3.Dot(vel, fwd);
            var tilt = _maxTilt * dot;
            transform.localRotation = Quaternion.Euler(tilt, 0f, 0f);
        }
    }
}
