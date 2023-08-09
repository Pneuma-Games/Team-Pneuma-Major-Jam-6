using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Life
{
    public class ThreeStrikesSequence : MonoBehaviour
    {
        public UnityEvent OnThreeStrikes;
        public GameObject _cam1;
        public GameObject _cam2;
        public GameObject _player;
        public GameObject _drone;
        public GameObject _text;
        public GameObject _screen;

        public CanvasGroup _cGroup;
        
        private Camera[] _cameras;

        private void Awake()
        {
            _cameras = FindObjectsOfType<Camera>();
        }

        public void StrikeThree()
        {
            foreach(var c in _cameras)
            {
                c.gameObject.SetActive(false);
            }
            _player.SetActive(false);
            _drone.SetActive(false);
            
            StartCoroutine(ExplosionSequenceCoroutine());
        }

        private IEnumerator ExplosionSequenceCoroutine()
        {
            _cam1.SetActive(true);
            yield return new WaitForSeconds(2.0f);
            _cam1.SetActive(false);
            _cam2.SetActive(true);
            _screen.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            _text.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            OnThreeStrikes.Invoke();
            _cGroup.DOFade(1.0f, 2.0f);
        }
    }
}
