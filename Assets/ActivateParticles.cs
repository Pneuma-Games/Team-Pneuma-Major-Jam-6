using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateParticles : MonoBehaviour
{
    public void DeactivateParticles()
    {
        //Considering we only have a few objects, just deleting the object feels like the thing to do.
        //Here is a script that just deletes the object once this is called from the ActivateParticles script.
        Destroy(gameObject);
    }

}
