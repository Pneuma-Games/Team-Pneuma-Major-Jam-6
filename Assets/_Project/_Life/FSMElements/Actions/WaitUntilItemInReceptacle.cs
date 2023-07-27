using NodeCanvas.Framework;

namespace Life
{
    public class WaitUntilItemInReceptacle : ActionTask<ReceptacleStation>
    {
        protected override void OnUpdate()
        {
            if (agent.ProcessingItem) EndAction();
        }
    }
}