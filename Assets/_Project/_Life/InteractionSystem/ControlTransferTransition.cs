using DG.Tweening;
using Life.MovementControllers;
using UnityEngine;

namespace Life.InteractionSystem
{
    
    /// <summary>
    /// This class handles the transfer of control from a moving player controller to an in-place interaction.
    /// This is mostly meant for minigames.
    /// </summary>
    public class ControlTransferTransition : MonoBehaviour
    {
        private IInPlaceInteraction _interaction;
        [SerializeField] private float _transitionTime = 0.5f;

        private MobilePlayerAvatar _interactingAvatar;

        public void TransferFromPlayer()
        {
            _interactingAvatar = MobilePlayerAvatar.Current;
            var camObjTForm = _interaction.StaticCamera.transform;
            var playerCamTForm = MobilePlayerAvatar.Current.MobileCamera.transform;
            _interaction.StaticCamera.fieldOfView = _interactingAvatar.MobileCamera.fieldOfView;
            camObjTForm.SetPositionAndRotation(playerCamTForm.position, playerCamTForm.rotation);
            _interactingAvatar.gameObject.SetActive(false);
            camObjTForm.gameObject.SetActive(true);
            camObjTForm.DOMove(_interaction.TargetCameraPosition, _transitionTime).OnComplete(() =>
            {
                _interaction.TransferControl();
            });
            camObjTForm.DORotateQuaternion(_interaction.TargetCameraRotation, _transitionTime);
            _interaction.StaticCamera.DOFieldOfView(_interaction.TargetCameraFOV, _transitionTime);
        }

        public void TransferToPlayer()
        {
            var camObjTForm = _interaction.StaticCamera.transform;
            var playerCamTForm = _interactingAvatar.MobileCamera.transform; 
            camObjTForm.DOMove(playerCamTForm.position, _transitionTime).OnComplete(() =>
            {
                _interactingAvatar.gameObject.SetActive(true);
                camObjTForm.gameObject.SetActive(false);
            });
            camObjTForm.DORotateQuaternion(playerCamTForm.rotation, _transitionTime);
            _interaction.StaticCamera.DOFieldOfView(_interactingAvatar.MobileCamera.fieldOfView, _transitionTime);
        }

        private void Awake()
        {
            _interaction = GetComponent<IInPlaceInteraction>();
            if (_interaction == null)
            {
                Debug.LogError($"Expected an implementation of IInPlaceInteraction on {gameObject.name}!");
                Destroy(this);
            }
        }
    }
}