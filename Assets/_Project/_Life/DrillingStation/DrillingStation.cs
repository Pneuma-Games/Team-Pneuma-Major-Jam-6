namespace Life
{
    public class DrillingStation : MockStation
    {
        public override void SpitOutItem()
        {
            _item.GameObject.GetComponent<Specimen>().SpecimenProgress.DrillComplete = true;
            base.SpitOutItem();
        }
    }
}
