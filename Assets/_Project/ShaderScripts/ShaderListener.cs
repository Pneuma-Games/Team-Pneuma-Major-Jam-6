using NodeCanvas.Tasks.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderListener : MonoBehaviour
{
    private ShaderSwapScript[] shadersInScene;
    private void Awake()
    {
        shadersInScene = FindObjectsOfType<ShaderSwapScript>();
    }


    public void CallShaderSwap(bool isToon)
    {
        foreach (ShaderSwapScript s in shadersInScene)
        {
            if (s != null)
            {
                s.SwapToToon(isToon);
            }
        }
    }
}
