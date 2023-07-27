using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fmodTest : MonoBehaviour
{
    FMOD.Studio.EventInstance test;
    // Start is called before the first frame update
    void Start()
    {
        test = FMODUnity.RuntimeManager.CreateInstance("event:/ambient_cockpit");
        test.start(FMOD.Studio.S
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
