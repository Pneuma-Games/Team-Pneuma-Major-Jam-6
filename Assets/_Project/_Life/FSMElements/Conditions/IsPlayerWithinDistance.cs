using Life.MovementControllers;
using NodeCanvas.Framework;

namespace Life
{
    public class IsPlayerWithinDistance : ConditionTask<ReceptacleStation>
    {
        public BBParameter<float> Distance;
        protected override bool OnCheck()
        {
            var avatar = MobilePlayerAvatar.Current;
            if (!avatar) return false;
            return (agent.DoorReference.position - avatar.transform.position).sqrMagnitude <= Distance.value * Distance.value;
        }
    }
}
