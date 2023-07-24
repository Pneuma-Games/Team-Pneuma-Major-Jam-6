using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerSelo
{
    private static Dictionary<Type, MonoBehaviour> _services = new();

    public static T Get<T>() where T : MonoBehaviour
    {
        var key = typeof(T);
        _services.TryGetValue(key, out var service);
        if (service == null)
        {
            if (_services.ContainsKey(key))
            {
                _services.Remove(key);
            }

            service = UnityEngine.Object.FindObjectOfType<T>();
            _services.Add(key, service);
        }

        if (service == null)
        {
            Debug.LogError($"There's no service of type {key} in the scene!");
            return null;
        }

        return service as T;
    }
}
