using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Life
{
    public class ExplosionSequence : MonoBehaviour
    {
        public UnityEvent OnDeath;
        public GameObject _cam1;
        public GameObject _cam2;
        public GameObject _cam3;
        public GameObject _player;
        public GameObject _drone;
        public GameObject _boom;

        public CanvasGroup _cGroup;
        
        private Camera[] _cameras;

        private void Awake()
        {
            _cameras = FindObjectsOfType<Camera>();
        }

        public void GoBoom()
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
            yield return new WaitForSeconds(2.0f);
            _cam2.SetActive(false);
            _cam3.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            _boom.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            OnDeath.Invoke();
            _cGroup.DOFade(1.0f, 2.0f);
        }
    }
}
