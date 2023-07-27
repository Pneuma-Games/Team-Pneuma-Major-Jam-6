using Life.MovementControllers;
using UnityEngine;

namespace Life.InteractionSystem
{
    public interface IInPlaceInteraction
    {
        Vector3 TargetCameraPosition { get; }
        Quaternion TargetCameraRotation { get; }
        float TargetCameraFOV { get; }
        Camera StaticCamera { get; }
        public void TransferControl();
        public void ReturnControl();
    }
}