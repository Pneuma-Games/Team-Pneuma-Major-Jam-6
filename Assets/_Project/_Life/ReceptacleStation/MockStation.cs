namespace Life
{
    public class MockStation : ProcessingStationBase
    {
        public void MockProcessItem()
        {
            if (!ProcessingItem) return;
            SpitOutItem();
        }
    }
}