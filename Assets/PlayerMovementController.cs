using UnityEngine;

namespace Monster.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private CharacterController _cc;
        
        [SerializeField] private float _accel;
        [SerializeField] private float _climbSpeed;
        [SerializeField] private float _maxForwardSpeed;
        [SerializeField] private float _maxClimbSpeed;
        [SerializeField] private float _decayRate;
        [SerializeField] private SurfaceDetector _surfaceDetector;
        [SerializeField] private PlayerCamera _playerCam;


        private Vector2 _flatVelocity;
        private float _verticalVelocity;
        private bool _climbing = false;
        private bool _falling = false;

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

            var inputVec = Vector2.ClampMagnitude(new Vector2(x, y), 1.0f);
            var input3 = _playerCam.FlatRotation * new Vector3(inputVec.x, 0, inputVec.y);
            inputVec.x = input3.x;
            inputVec.y = input3.z;
            _flatVelocity += inputVec * (_accel * accelFactor * Time.deltaTime);
            
            _flatVelocity = Vector2.ClampMagnitude(_flatVelocity, _maxForwardSpeed);
            _verticalVelocity = Mathf.Clamp(_verticalVelocity, -_maxClimbSpeed, _maxClimbSpeed);
        }

        private void FixedUpdate()
        {
            var decayRate = _decayRate;

            
            _flatVelocity *= decayRate;
            _verticalVelocity *= decayRate;
        }

        

        private void UpdateVerticalVelocity()
        {
            
            //new logic for flying
            if (Input.GetKeyDown(KeyCode.E))
            {
                //go up
                _climbing = true;
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                //go up
                _climbing = false;
            }

            

            if (Input.GetKeyDown(KeyCode.Q))
            {
                //go up
                _falling = true;
            }

            if (Input.GetKeyUp(KeyCode.Q))
            {
                //go up
                _falling = false;
            }

            if (_climbing)
            {
                _verticalVelocity += _climbSpeed;
            } else if (_falling)
            {
                _verticalVelocity -= _climbSpeed;
            }

           

            if (_surfaceDetector.Above)
            {
                _verticalVelocity = -0.33f;
            }

            //_groundedLastFrame = _cc.isGrounded;
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
