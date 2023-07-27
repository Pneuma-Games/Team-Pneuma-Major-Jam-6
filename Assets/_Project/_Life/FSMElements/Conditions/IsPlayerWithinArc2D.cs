using Life.MovementControllers;
using NodeCanvas.Framework;
using UnityEngine;

namespace Life
{
    public class IsPlayerWithinArc2D : ConditionTask
    {
        public BBParameter<Transform> Reference;
        public BBParameter<float> DotThreshold;
        protected override bool OnCheck()
        {
            var avatar = MobilePlayerAvatar.Current;
            if (!avatar) return false;
            var toPlayer = avatar.transform.position - agent.transform.position;
            toPlayer.y = 0;
            return Vector3.Dot(toPlayer.normalized, Reference.value.forward) >= DotThreshold.value;
        }
    }
}