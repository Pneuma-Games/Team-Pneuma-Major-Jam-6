using UnityEngine;

namespace Life.MovementControllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private CharacterController _cc;
        
        [SerializeField] private float _accel;
        [SerializeField] private float _maxForwardSpeed;
        [SerializeField] private float _gravityFactor;
        [SerializeField] private float _decayRate;
        [SerializeField] private float _decayRateMidair;
        [SerializeField] private float _midairAccelFactor;

        [SerializeField] private SurfaceDetector _surfaceDetector;
        [SerializeField] private PlayerCameraController _playerCam;

        private bool _boostUsed;

        private Vector2 _flatVelocity;
        private float _verticalVelocity;

        private bool _groundedLastFrame;
        private float _timeAirborne;

        private bool _onSurface => _cc.isGrounded;

        void Start()
        {
            _playerCam.GroundNormal = Vector3.up;
        }
        
        private const string HORIZONTAL = "Horizontal";
        private const string VERTICAL = "Vertical";


        void Update()
        {
            UpdateCamLock();
            var x = Input.GetAxis(HORIZONTAL);
            var y = Input.GetAxis(VERTICAL);
            
            UpdateHorizontalVelocity(x, y);
            UpdateVerticalVelocity();

            var movDelta = new Vector3(_flatVelocity.x, _verticalVelocity, _flatVelocity.y) * Time.deltaTime;
            _cc.Move(movDelta);
        }

        private void UpdateHorizontalVelocity(float x, float y)
        {
            var accelFactor = 1.0f;
            if (!_onSurface)
            {
                accelFactor = _midairAccelFactor;
            }

            var inputVec = Vector2.ClampMagnitude(new Vector2(x, y), 1.0f);
            var input3 = _playerCam.FlatRotation * new Vector3(inputVec.x, 0, inputVec.y);
            inputVec.x = input3.x;
            inputVec.y = input3.z;
            _flatVelocity += inputVec * (_accel * accelFactor * Time.deltaTime);
            
            _flatVelocity = Vector2.ClampMagnitude(_flatVelocity, _maxForwardSpeed);
        }

        private void FixedUpdate()
        {
            var decayRate = _decayRate;
            if (!_onSurface)
            {
                decayRate = _decayRateMidair;
            }
            
            _flatVelocity *= decayRate;
        }

        private void UpdateVerticalVelocity()
        {
            if (_cc.isGrounded)
            {
                if (!_groundedLastFrame && _timeAirborne > 0.3f)
                {
                    // Landing sound here
                }
                
                _verticalVelocity = -1;
                _boostUsed = false;
                /*
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (_verticalVelocity < 0)
                    {
                        _verticalVelocity = 0;
                    }
                    _verticalVelocity += 7.4f;
                    // Jump sound here
                }*/
            }
            else
            {
                /*
                if (!_boostUsed && Input.GetKeyDown(KeyCode.Space))
                {
                    if (_verticalVelocity < 0)
                    {
                        _verticalVelocity = 0;
                    }
                    _verticalVelocity += 4.2f;
                    if (_verticalVelocity > 7.4f) _verticalVelocity = 8.5f;
                    _boostUsed = true;
                    // double jump sound here
                }*/

                if (_groundedLastFrame)
                {
                    _timeAirborne = 0;
                }
                _timeAirborne += Time.deltaTime;
            }

            if (_surfaceDetector.Above)
            {
                _verticalVelocity = -0.33f;
            }
            
            _verticalVelocity = Mathf.Clamp(_verticalVelocity - _gravityFactor * Time.deltaTime, -35, 15);

            _groundedLastFrame = _cc.isGrounded;
        }
        
        #region Editor mouse pointer handling
        private bool _camLocked;

        private void LockCam()
        {
            _camLocked = true;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }

        private void UnlockCam()
        {
            _camLocked = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void UpdateCamLock()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UnlockCam();
            } else if (Input.GetKeyDown(KeyCode.T))
            {
                LockCam();
            }
        }
        #endregion
        
    }
}
