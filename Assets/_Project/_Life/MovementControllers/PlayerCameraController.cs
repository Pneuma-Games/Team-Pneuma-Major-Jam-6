using System;
using UnityEngine;

namespace Life.MovementControllers
{
    public class PlayerCameraController : MonoBehaviour
    {
        public Quaternion Rotation => _composedRotation;
        public Quaternion FlatRotation => Quaternion.Inverse(_lastUpwardRotation) * Rotation;

        [SerializeField] private float _lookSens;
        [SerializeField] private Transform _camTransform;

        private float _cameraVerticalAngle;
        private float _cameraHorizontalAngle;
        private Quaternion _composedRotation;
        private Quaternion _lastUpwardRotation;
        
        private Vector3 _currentGroundNormal;
        private Vector3 _lastGroundNormal;
        private bool _lerpBaseRotation;
        private float _lerpProgress;
        private float _sensMod = 1.0f;

        private const string MOUSE_X = "Mouse X";
        private const string MOUSE_Y = "Mouse Y";

        
        public Vector3 GroundNormal
        {
            set => _currentGroundNormal = value;
        }

        private void Start()
        {
            _currentGroundNormal = Vector3.up;
            _lastGroundNormal = Vector3.up;
        }

        // private void OnEnable()
        // {
        //     _camTransform.SetParent(null);
        // }
        //
        // private void OnDisable()
        // {
        //     _camTransform.SetParent(this.transform);
        // }

        private void Update()
        {
            // if (global::Player.Alive == false) return;
            var yInput = Input.GetAxis(MOUSE_Y);
            var xInput = Input.GetAxis(MOUSE_X);
            UpdateCameraAngles(xInput, yInput);
            var gN = ProcessGroundNormal();
            _composedRotation = ComposeCameraRotation(gN);
            
            if (Input.GetKeyDown(KeyCode.Alpha9)) AdjustSens(false);
            if (Input.GetKeyDown(KeyCode.Alpha0)) AdjustSens(true);
        }

        private void LateUpdate()
        {
            // if (global::Player.Alive == false) return;
            _camTransform.SetPositionAndRotation(transform.position, _composedRotation);
        }

        private void UpdateCameraAngles(float xInput, float yInput)
        {
            _cameraVerticalAngle += -yInput * _lookSens * _sensMod;
            _cameraHorizontalAngle += xInput * _lookSens * _sensMod;
            
            _cameraVerticalAngle = Mathf.Clamp(_cameraVerticalAngle, -85f, 85f);
        }

        private Quaternion ComposeCameraRotation(Vector3 surfaceNormal)
        {
            var baseRotation = Quaternion.FromToRotation(Vector3.up, surfaceNormal);
            var sideways = Quaternion.AngleAxis(_cameraHorizontalAngle, surfaceNormal);
            var upward = Quaternion.AngleAxis(_cameraVerticalAngle, sideways * Vector3.right);
            var rotation = upward * sideways * baseRotation;
            _lastUpwardRotation = upward;

            return rotation;
        }
        
                
        private Vector3 ProcessGroundNormal()
        {
            var result = Vector3.Lerp(_lastGroundNormal, _currentGroundNormal, 0.12f).normalized;
            _lastGroundNormal = result;

            return result;
        }

        private void AdjustSens(bool up)
        {
            if (up) _sensMod += 0.1f;
            else _sensMod -= 0.1f;

            _sensMod = Mathf.Clamp(_sensMod, 0.5f, 1.5f);
        }
    }
}