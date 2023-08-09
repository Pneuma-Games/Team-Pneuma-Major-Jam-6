using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Life
{
    public class PoisonSequence : MonoBehaviour
    {

        private float timerDuration = 180f;
        private float currentTimerValue;
        private bool isTimerRunning = false;
        public UnityEvent OnPoison;
        public UnityEvent OnDeath;
        public GameObject _cam1;
        public GameObject _playerCam;
        public GameObject _player;
        public GameObject _drone;
        public GameObject _poison;

        public CanvasGroup _cGroup;
        private Camera[] _cameras;
        [SerializeField]
        public Vector3 rotationalForce = new Vector3(0f, 0f, 3f);
        private Rigidbody _camRigidBody;


        private void Update()
        {

            //if (Input.GetKey(KeyCode.Space)) PoisonDeath();
            if (isTimerRunning)
            {
                currentTimerValue -= Time.deltaTime;
                Debug.Log("Timer: " + currentTimerValue);

                if (currentTimerValue <= 0f)
                {
                    isTimerRunning = false;
                    PoisonDeath();
                }
            }
        }
        private void Awake()
        {
            _cameras = FindObjectsOfType<Camera>();
            _camRigidBody = _cam1.GetComponent<Rigidbody>();
        }

        private void PoisonDeath(){
            _cam1.transform.position = _playerCam.transform.position;
            _cam1.transform.rotation = _playerCam.transform.rotation;
            foreach (var c in _cameras)
            {
                c.gameObject.SetActive(false);
            }
            _player.SetActive(false);
            _drone.SetActive(false);
            StartCoroutine(PoisonSequenceCoroutine());
        }


        private IEnumerator PoisonSequenceCoroutine()
        {
            OnPoison.Invoke();
            _poison.SetActive(true);
            _cam1.SetActive(true);
            _camRigidBody.useGravity = false;
            yield return new WaitForSeconds(2.0f);
            _camRigidBody.useGravity = true;
            _camRigidBody.AddTorque(rotationalForce);
            yield return new WaitForSeconds(1.5f);
            _cGroup.DOFade(1.0f, 2.0f);
            OnDeath.Invoke();
            yield return new WaitForSeconds(6f);
            _camRigidBody.velocity = Vector3.zero;
            _camRigidBody.angularVelocity = Vector3.zero;
        }

        public void StartTimer()
        {
            currentTimerValue = timerDuration;
            isTimerRunning = true;
        }

        private void StopTimer()
        {
            isTimerRunning = false;
        }
    }
}
