using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    FMOD.Studio.EventInstance drone_ambient;
    FMOD.Studio.EventInstance drone_scan;
    FMOD.Studio.EventInstance drone_music;

    // Start is called before the first frame update
    void Start()
    {
        drone_ambient = FMODUnity.RuntimeManager.CreateInstance("event:/drone/drone_ambient");
        drone_scan = FMODUnity.RuntimeManager.CreateInstance("event:/drone/drone_scanspecimen");
        drone_music = FMODUnity.RuntimeManager.CreateInstance("event:/drone/drone_musicloop");
    }

    // Plays the drone propeller sound after entering the feed and music loop
    public void PlayDroneAmbient()
    {
        drone_ambient.start();
        // still need that drone speed var for controlling the pitch automation
        drone_music.start();
    }

    // Stops the drone propeller sound after exiting the feed and music loop
    public void StopDroneAmbient()
    {
        drone_ambient.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        drone_music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    // Enter drone feed one-shot sfx
    public void EnterDroneFeed()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/drone/drone_enterfeed");
    }

    // Exit drone feed one-shot sfx
    public void ExitDroneFeed()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/drone/drone_exitfeed");
    }

    // Select specimen
    public void DroneSelect()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/drone/drone_selectspecimen");
    }

    //Fail sound for drone
    public void DroneFail()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/drone/drone_fail");
    }

    //SUCCess sound for drone
    public void DroneSuccess()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/drone/drone_success");
    }

    //Scan start
    public void ScanStart()
    {
        drone_scan.start();
    }

    //Scan end
    public void ScanEnd()
    {
        drone_scan.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

}
