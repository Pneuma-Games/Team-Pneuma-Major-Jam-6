using System;
using System.Collections;
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
        [SerializeField] private IInPlaceInteraction _interaction;

        public void TransferFromPlayer()
        {
            //MobilePlayerAvatar.Current
        }

        public void TransferToPlayer()
        {
            
        }
        
    }

    public interface IInPlaceInteraction
    {
        Vector3 CameraPosition { get; }
        Quaternion CameraRotation { get; }
        Camera StaticCamera { get; }
        public void TransferControl(MobilePlayerAvatar avatar);
        public void ReturnControl();
    }

    /// <summary>
    /// An interaction that "glues" the player in place.
    /// </summary>
    public class InPlaceInteractionExample : MonoBehaviour, IInPlaceInteraction
    {
        public Vector3 CameraPosition { get; private set; }
        public Quaternion CameraRotation { get; private set; }
        public Camera StaticCamera { get; private set; }
        private MobilePlayerAvatar _currentlyInteractingAvatar;
        
        public void TransferControl(MobilePlayerAvatar avatar)
        {
            _currentlyInteractingAvatar = avatar;
            avatar.gameObject.SetActive(false);
            StaticCamera.gameObject.SetActive(true);
        }
        
        public void ReturnControl()
        {
            _currentlyInteractingAvatar.gameObject.SetActive(true);
            StaticCamera.gameObject.SetActive(false);
            _currentlyInteractingAvatar = null;
        }

        private void Awake()
        {
            StaticCamera = GetComponentInChildren<Camera>();
            CameraPosition = StaticCamera.transform.position;
            CameraRotation = StaticCamera.transform.rotation;
        }
    }
}