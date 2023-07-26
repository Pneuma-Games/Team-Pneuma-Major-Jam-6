namespace Life.TransportSystem
{
    public static class TransportSystem
    {
        public static bool TransporterAvailable => _activeTransporter;
        public static bool ItemStored => _activeTransporter.HeldItem != null;

        private static Transporter _activeTransporter;

        public static void SetTransporterAsCurrent(Transporter transporter)
        {
            _activeTransporter = transporter;
        }

        public static void ReportTransporterDeactivation(Transporter transporter)
        {
            if (_activeTransporter == transporter) _activeTransporter = null;
        }
        
        public static void StoreItem(ITransportable item)
        {
            if (!TransporterAvailable && ItemStored) return;
            _activeTransporter.Store(item);
        }
        
        public static ITransportable RetrieveItem()
        {
            if (!TransporterAvailable || !ItemStored) return null;
            var item = _activeTransporter.HeldItem;
            _activeTransporter.Store(null);
            return item;
        }
        
        public static void DropItem()
        {
            if (!TransporterAvailable || !ItemStored) return;
            _activeTransporter.Drop();
        }
        
    }
}