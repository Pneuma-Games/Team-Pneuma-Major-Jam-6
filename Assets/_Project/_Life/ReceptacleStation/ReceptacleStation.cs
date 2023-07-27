using DG.Tweening;
using Life.TransportSystem;
using UnityEngine;

namespace Life
{
    public class ReceptacleStation : MonoBehaviour
    {
        public Transform ItemSpawnPoint;
        public Transform DoorReference;
        public Transform Door;
        [SerializeField] private float _yOpen;
        [SerializeField] private float _yClosed;

        public bool DoorMoving => DOTween.IsTweening(Door);
        
        public bool ProcessingItem => _item != null;

        private ITransportable _item;

        public void AcceptItem()
        {
            if (TransportSystem.TransportSystem.ItemStored)
            {
                _item = TransportSystem.TransportSystem.RetrieveItem();
            }
        }

        public void MoveItemToInside()
        {
            _item.GameObject.transform.position = ItemSpawnPoint.position;
            _item.GameObject.SetActive(true);
            _item = null;
        }

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
