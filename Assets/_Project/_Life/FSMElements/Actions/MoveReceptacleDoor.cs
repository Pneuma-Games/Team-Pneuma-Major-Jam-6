using NodeCanvas.Framework;

namespace Life
{
    public class MoveReceptacleDoor : ActionTask<ReceptacleStation>
    {
        public BBParameter<bool> Open;

        protected override void OnExecute()
        {
            if (Open.value)
            {
                agent.OpenDoor();
            }
            else
            {
                agent.CloseDoor();
            }
        }

        protected override void OnUpdate()
        {
            if (!agent.DoorMoving)
                EndAction();
        }
    }
}
