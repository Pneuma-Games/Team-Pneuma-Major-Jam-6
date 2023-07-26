using UnityEngine;

namespace Life.TransportSystem
{
    public interface ITransportable
    {
        GameObject GameObject { get; }
        bool StoreItem();
    }
}