using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Life
{
    public class FiredSequence : MonoBehaviour
    {
        public UnityEvent OnFired;
        public GameObject _cam1;
        public GameObject _player;
        public GameObject _drone;
        public GameObject _firedText;
        public GameObject _screen;

        public CanvasGroup _cGroup;
        private Camera[] _cameras;


        private void Awake()
        {
            _cameras = FindObjectsOfType<Camera>();
        }

        public void PlayFired(){
            foreach (var c in _cameras)
            {
                c.gameObject.SetActive(false);
                _firedText.SetActive(true);
                _screen.SetActive(false);
            }
            _player.SetActive(false);
            _drone.SetActive(false);
            StartCoroutine(FiredSequenceCoroutine());
        }


        private IEnumerator FiredSequenceCoroutine()
        {
            _cam1.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            OnFired.Invoke();
            _cGroup.DOFade(1.0f, 2.0f);
        }
    }
}
