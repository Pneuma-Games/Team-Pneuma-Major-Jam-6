using NodeCanvas.Framework;

namespace Life
{
    public class MoveReceptacleItemInside : ActionTask<ReceptacleStation>
    {
        protected override void OnExecute()
        {
            agent.SpitOutItem();
            EndAction();
        }
    }
}