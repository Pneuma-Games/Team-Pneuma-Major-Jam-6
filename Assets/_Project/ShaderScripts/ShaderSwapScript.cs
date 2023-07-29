using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderSwapScript : MonoBehaviour
{

    public Material materialOriginal;
    public Material materialToon;

    public void SwapToToon(bool isToon)
    {
        if (materialToon != null)
        {
            if(isToon)
            {
                GetComponent<Renderer>().material = materialToon;
            }
            else
            {
                GetComponent<Renderer>().material = materialOriginal;
            }
        }
    }


    //private void OnDrawGizmosSelected()
    //{
    //    Color tmp = Color.red;
    //    tmp.a = .5F;
    //    Gizmos.color = tmp;
    //    Gizmos.DrawCube(GetComponent<BoxCollider>().bounds.center, GetComponent<BoxCollider>().bounds.size);
    //}






}
