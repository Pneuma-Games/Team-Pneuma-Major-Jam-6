using System;
using UnityEngine;

namespace Life.InteractionSystem
{
    /// <summary>
    /// An example interaction that "glues" the player in place.
    /// </summary>
    public class InPlaceInteractionExample : MonoBehaviour, IInPlaceInteraction
    {
        public Vector3 TargetCameraPosition { get; private set; }
        public Quaternion TargetCameraRotation { get; private set; }
        public float TargetCameraFOV { get; private set; }
        public Camera StaticCamera { get; private set; }

        private bool _inControl;
        
        
        /// <summary>
        /// This method is called when camera transition is complete. Activate the station here.
        /// </summary>
        public void TransferControl()
        {
            _inControl = true;
            WorldUI.Instance.Canvas.worldCamera = StaticCamera;
        }
        
        /// <summary>
        /// This method is called to trigger the start of the transition back to player.
        /// It can be called externally or from within.
        /// You should disable the station in this method.
        /// </summary>
        public void ReturnControl()
        {
            GetComponent<ControlTransferTransition>().TransferToPlayer();
            _inControl = false;
            WorldUI.Instance.Canvas.worldCamera = null;
        }

        private void Update()
        {
            if (_inControl && Input.GetKeyDown(KeyCode.F))
            {
                ReturnControl();
            }
        }

        private void Awake()
        {
            StaticCamera = GetComponentInChildren<Camera>(true);
            StaticCamera.gameObject.SetActive(false);
            TargetCameraPosition = StaticCamera.transform.position;
            TargetCameraRotation = StaticCamera.transform.rotation;
            TargetCameraFOV = StaticCamera.fieldOfView;
        }
    }
}