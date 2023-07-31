using DG.Tweening;
using UnityEngine;

namespace Life
{
    public class ReceptacleStation : ProcessingStationBase
    {
        [SerializeField] private ChuteStation _chute;
        
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

        public void PassItemToChute()
        {
            _item.GameObject.GetComponent<Specimen>().SpecimenProgress.Deposited = true;
            _chute.TakeOverItem(_item);
            _item = null;
        }
    }
}
