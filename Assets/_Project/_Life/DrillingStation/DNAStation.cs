namespace Life
{
    public class DNAStation : MockStation
    {
        public override void SpitOutItem()
        {
            _item.GameObject.GetComponent<Specimen>().SpecimenProgress.DNAComplete = true;
            base.SpitOutItem();
        }
    }
}