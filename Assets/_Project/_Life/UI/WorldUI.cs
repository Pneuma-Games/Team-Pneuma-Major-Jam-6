using System;
using UnityEngine;

namespace Life
{
    [DefaultExecutionOrder(-500)]
    public class WorldUI : MonoBehaviour
    {
        public static WorldUI Instance;
        public Canvas Canvas;

        private void Awake()
        {
            Instance = this;
        }
   }
}
