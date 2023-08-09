using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Life
{
    public class VictorySequence : MonoBehaviour
    {
        public UnityEvent OnVictory;
        public GameObject _cam1;
        public GameObject _player;
        public GameObject _drone;

        public CanvasGroup _cGroup;
        private Camera[] _cameras;


        private void Awake()
        {
            _cameras = FindObjectsOfType<Camera>();
        }

        public void PlayVictory(){
            foreach (var c in _cameras)
            {
                c.gameObject.SetActive(false);
            }
            _player.SetActive(false);
            _drone.SetActive(false);
            StartCoroutine(VictorySequenceCoroutine());
        }


        private IEnumerator VictorySequenceCoroutine()
        {
            _cam1.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            OnVictory.Invoke();
            _cGroup.DOFade(1.0f, 2.0f);
        }
    }
}
