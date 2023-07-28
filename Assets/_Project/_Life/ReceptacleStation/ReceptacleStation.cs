using DG.Tweening;
using UnityEngine;

namespace Life
{
    public class ReceptacleStation : ProcessingStationBase
    {
        public Transform DoorReference;
        public Transform Door;
        [SerializeField] private float _yOpen;
        [SerializeField] private float _yClosed;

        public bool DoorMoving => DOTween.IsTweening(Door);

        public void OpenDoor()
        {
            DOTween.Kill(Door, true);
            Door.DOLocalMoveY(_yOpen, .35f).SetEase(Ease.InOutCubic);
        }

        public void CloseDoor()
        {
            DOTween.Kill(Door, true);
            Door.DOLocalMoveY(_yClosed, .35f).SetEase(Ease.InOutCubic);
        }
    }
}
