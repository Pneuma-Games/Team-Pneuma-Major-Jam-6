using System;
using UnityEngine;

namespace Life.MovementControllers
{
    [RequireComponent(typeof(Rigidbody))]
    public class DroneMovement : MonoBehaviour
    {
        [SerializeField] private float _maxFlatVel = 10f;
        [SerializeField] private float _maxVertVel = 5f;
        [SerializeField] private float _maxAngularVel = 10f;
        [SerializeField] private float _maxAltitude = 15;
        [SerializeField] private float _boostMultiplier = 1.5f;
        
        
        [SerializeField] private float _accel = 5.0f;
        [SerializeField] private float _haltDecay = 0.85f;
        
        private Rigidbody _rb;
        private Vector2 _flatVelocity;
        private float _vertVelocity;
        private Vector3 _angularVel;
        private float _xInput, _yInput, _zInput;
        private bool _boostInput, _haltInput;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.useGravity = false;
            _rb.interpolation = RigidbodyInterpolation.Interpolate;
        }

        private void Update()
        {
            _xInput = Input.GetAxis("Horizontal");
            _yInput = Input.GetAxis("Vertical");

            _zInput = 0f;
            if (Input.GetKey(KeyCode.Q))
            {
                _zInput = -1f;
            } 
            else if (Input.GetKey(KeyCode.E))
            {
                _zInput = 1f;
            }
            
            _boostInput = Input.GetKey(KeyCode.LeftShift);
            _haltInput = Input.GetKey(KeyCode.Space);
        }

        private void FixedUpdate()
        {
            var vel = _rb.velocity;
            var angular = _rb.angularVelocity;
            var boost = _boostInput ? _boostMultiplier : 1f;
            _flatVelocity.x = vel.x;
            _flatVelocity.y = vel.z;
            _vertVelocity = vel.y;
            
            if (_haltInput)
            {
                _flatVelocity *= _haltDecay;
            }
            else
            {
                var accel = new Vector3(0,0, _yInput) * (_accel * boost * Time.fixedDeltaTime);
                accel = transform.rotation * accel;
                _flatVelocity += new Vector2(accel.x, accel.z);
            }

            if (_haltInput)
            {
                angular *= _haltDecay;
            }
            else
            {
                angular += new Vector3(0, _xInput, 0) * _accel * Time.fixedDeltaTime;
            }

            if (_haltInput)
            {
                _vertVelocity *= _haltDecay;
            }
            else
            {
                _vertVelocity += _zInput * _accel * boost * Time.fixedDeltaTime;
            }
                
            _flatVelocity = Vector2.ClampMagnitude(_flatVelocity, _maxFlatVel);
            _vertVelocity = Mathf.Clamp(_vertVelocity, -_maxVertVel, _maxVertVel);
            _angularVel = Vector3.ClampMagnitude(angular, _maxAngularVel);
            
            _rb.velocity =  new Vector3(_flatVelocity.x, _vertVelocity, _flatVelocity.y);
            _rb.angularVelocity = _angularVel;
        }
        
    }
}