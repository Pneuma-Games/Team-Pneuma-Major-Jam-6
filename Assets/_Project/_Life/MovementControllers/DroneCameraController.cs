using System;
using UnityEngine;

namespace Life.MovementControllers
{
    public class DroneCameraController : MonoBehaviour
    {
        [SerializeField] private float _lookSens;
        [SerializeField] private Transform _camTransform;
        [SerializeField] private Vector2 _verticalClamp;
        [SerializeField] private Vector2 _horizontalClamp;
        
        public float CamVerticalAngle => _cameraVerticalAngle;
        public float CamHorizontalAngle => _cameraHorizontalAngle;
        public Vector2 VerticalClamp => _verticalClamp;
        public Vector2 HorizontalClamp => _horizontalClamp;

        private float _cameraVerticalAngle;
        private float _cameraHorizontalAngle;
        private Quaternion _composedRotation;
        
        private bool _lerpBaseRotation;
        private float _lerpProgress;
        private float _sensMod = 1.0f;

        private const string MOUSE_X = "Mouse X";
        private const string MOUSE_Y = "Mouse Y";
        
        private void Update()
        {
            var yInput = Input.GetAxis(MOUSE_Y);
            var xInput = Input.GetAxis(MOUSE_X);
            UpdateCameraAngles(xInput, yInput);
            var gN = Vector3.up;
            _composedRotation = ComposeCameraRotation(gN);
            
            if (Input.GetKeyDown(KeyCode.Alpha9)) AdjustSens(false);
            if (Input.GetKeyDown(KeyCode.Alpha0)) AdjustSens(true);
        }

        private void LateUpdate()
        {
            _camTransform.SetPositionAndRotation(transform.position, _composedRotation);
        }

        private void UpdateCameraAngles(float xInput, float yInput)
        {
            _cameraVerticalAngle += -yInput * _lookSens * _sensMod;
            _cameraHorizontalAngle += xInput * _lookSens * _sensMod;
            
            _cameraVerticalAngle = Mathf.Clamp(_cameraVerticalAngle, _verticalClamp.x, _verticalClamp.y);
            _cameraHorizontalAngle = Mathf.Clamp(_cameraHorizontalAngle, _horizontalClamp.x, _horizontalClamp.y);
        }

        private Quaternion ComposeCameraRotation(Vector3 surfaceNormal)
        {
            var baseRotation = _camTransform.parent.rotation;
            var sideways = Quaternion.AngleAxis(_cameraHorizontalAngle, surfaceNormal);
            var upward = Quaternion.AngleAxis(_cameraVerticalAngle, sideways * Vector3.right);
            var rotation = baseRotation * upward * sideways;

            return rotation;
        }

        private void AdjustSens(bool up)
        {
            if (up) _sensMod += 0.1f;
            else _sensMod -= 0.1f;

            _sensMod = Mathf.Clamp(_sensMod, 0.5f, 1.5f);
        }

        private void OnEnable()
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }
    }
}