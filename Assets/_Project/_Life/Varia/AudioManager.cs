using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace Life
{
    [DefaultExecutionOrder(-500)]
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        EventInstance drone_ambient;
        EventInstance drone_scan;
        EventInstance drone_music;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            drone_ambient = RuntimeManager.CreateInstance("event:/drone/drone_ambient");
            drone_scan = RuntimeManager.CreateInstance("event:/drone/drone_scanspecimen");
            drone_music = RuntimeManager.CreateInstance("event:/drone/drone_musicloop");
        }

        // Plays the drone propeller sound after entering the feed and music loop
        public void PlayDroneAmbient()
        {
            drone_ambient.start();
            // still need that drone speed var for controlling the pitch automation
            drone_music.start();
        }

        public void UpdateDroneSpeed(float spd)
        {

        }

        // Stops the drone propeller sound after exiting the feed and music loop
        public void StopDroneAmbient()
        {
            drone_ambient.stop(STOP_MODE.IMMEDIATE);
            drone_music.stop(STOP_MODE.ALLOWFADEOUT);
        }

        // Enter drone feed one-shot sfx
        public void EnterDroneFeed()
        {
            RuntimeManager.PlayOneShot("event:/drone/drone_enterfeed");
        }

        // Exit drone feed one-shot sfx
        public void ExitDroneFeed()
        {
            RuntimeManager.PlayOneShot("event:/drone/drone_exitfeed");
        }

        // Select specimen
        public void DroneSelect()
        {
            RuntimeManager.PlayOneShot("event:/drone/drone_selectspecimen");
        }

        //Fail sound for drone
        public void DroneFail()
        {
            RuntimeManager.PlayOneShot("event:/drone/drone_fail");
        }

        //SUCCess sound for drone
        public void DroneSuccess()
        {
            RuntimeManager.PlayOneShot("event:/drone/drone_success");
        }

        //Scan start
        public void ScanStart()
        {
            drone_scan.start();
        }

        //Scan end
        public void ScanEnd()
        {
            drone_scan.stop(STOP_MODE.ALLOWFADEOUT);
        }

    }

}