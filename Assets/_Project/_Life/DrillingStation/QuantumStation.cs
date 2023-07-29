namespace Life
{
    public class QuantumStation : MockStation
    {
        public override void SpitOutItem()
        {
            _item.GameObject.GetComponent<Specimen>().SpecimenProgress.QuantumComplete = true;
            base.SpitOutItem();
        }
    }
}