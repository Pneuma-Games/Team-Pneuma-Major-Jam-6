//This script should be on a prefab with the same name
//Before doing anything with it, remove all the FMOD audio listeners components and add them again in the correct order: 0- player; 1-drone; 2-game over explosion
//IMPORTANT!! All the banks must be loaded asynchronously, OTHERWISE WHOLE AUDIO SYSTEM MIGHT BE BROKEN. More info here: https://www.fmod.com/docs/2.02/unity/platform-specifics.html
//To prevent audio stuttering issues when tabbing out of the game, make a script that detects this action and utilize function OnApplicationFocus made in this script (modify it if needed)
//Audio Manager contains all the functions with events that need to be invoked in the correct places. I've attached some instructions that may help with this process
//Please work, please work, please work, Please work, please work, please work, Please work, please work, please work, Please work, please work, please work, Please work, please work, please work, Please work, please work, please work, 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Life
{
    [DefaultExecutionOrder(-500)]
    public class AudioManager : MonoBehaviour
    {
        [HideInInspector]
        public FMOD.Studio.EventInstance cockpit_ambient;
        [HideInInspector]
        public FMOD.Studio.EventInstance cockpit_music;
        [HideInInspector]
        public FMOD.Studio.EventInstance drone_ambient;
        [HideInInspector]
        public FMOD.Studio.EventInstance drone_ambient_screams;
        [HideInInspector]
        public FMOD.Studio.EventInstance drone_scan;
        [HideInInspector]
        public FMOD.Studio.EventInstance drone_music;
        [HideInInspector]
        public FMOD.Studio.EventInstance drill_start;
        [HideInInspector]
        public FMOD.Studio.EventInstance drill_penetrating;
        [HideInInspector]
        public FMOD.Studio.EventInstance drill_stop;
        [HideInInspector]
        public FMOD.Studio.EventInstance dna_ambient;
        [HideInInspector]
        public FMOD.Studio.EventInstance quantum_ambient;
        [HideInInspector]
        public int position_dronemusic = 0;
        [HideInInspector]
        public int position_cockpitambient = 0;
        [HideInInspector]
        public int position_cockpitmusic = 0;

        //Mutes audio when tabbed out of the game
        public void OnApplicationFocus(bool focus)
        {
            if (FMODUnity.RuntimeManager.StudioSystem.isValid())
            {
                FMODUnity.RuntimeManager.PauseAllEvents(!focus); 

                if (!focus) //also save some CPU usage
                {
                    FMODUnity.RuntimeManager.CoreSystem.mixerSuspend();
                }
                else
                {
                    FMODUnity.RuntimeManager.CoreSystem.mixerResume();
                }
            }
        }


        // Start is called before the first frame update
        void Start()
        {
            FMODUnity.RuntimeManager.StudioSystem.setListenerWeight(1, 0f); //Temporarily disables drone audio listener on start
            FMODUnity.RuntimeManager.StudioSystem.setListenerWeight(2, 0f); //Temporarily disables game over explosion audio listener on start
            cockpit_ambient = FMODUnity.RuntimeManager.CreateInstance("event:/ambient_cockpit");
            cockpit_music = FMODUnity.RuntimeManager.CreateInstance("event:/music_cockpit");
            drone_ambient = FMODUnity.RuntimeManager.CreateInstance("event:/drone/drone_ambient");
            drone_ambient_screams = FMODUnity.RuntimeManager.CreateInstance("event:/drone/drone_ambient_screams");
            drone_scan = FMODUnity.RuntimeManager.CreateInstance("event:/drone/drone_scanspecimen");
            drone_music = FMODUnity.RuntimeManager.CreateInstance("event:/drone/drone_musicloop");
            drill_start = FMODUnity.RuntimeManager.CreateInstance("event:/drill/drill_start");
            drill_penetrating = FMODUnity.RuntimeManager.CreateInstance("event:/drill/drill_penetrating");
            drill_stop = FMODUnity.RuntimeManager.CreateInstance("event:/drill/drill_stop");
            dna_ambient = FMODUnity.RuntimeManager.CreateInstance("event:/dna_ambient");
            quantum_ambient = FMODUnity.RuntimeManager.CreateInstance("event:/quantum/quantum_ambient");


            //Couldn't find where this was being triggered, putting it here lol. - Cholfy
            PlayCockpitAmbient();
        }


        //COCKPIT EVENTS

        //Play cockpit ambience and music loop (on start and after exiting drone)
        public void PlayCockpitAmbient()
        {
            cockpit_ambient.setTimelinePosition(position_cockpitambient);
            cockpit_ambient.start();
            cockpit_music.setTimelinePosition(position_cockpitmusic);
            cockpit_music.start();
        }

        //Stop cockpit ambience and music loop
        public void StopCockpitAmbient()
        {
            cockpit_ambient.getTimelinePosition(out position_cockpitambient);
            cockpit_ambient.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            cockpit_music.getTimelinePosition(out position_cockpitmusic);
            cockpit_music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        //DRONE EVENTS

        // Plays the drone propeller and ambience sounds after entering the feed and music loop
        public void PlayDroneAmbient()
        {
            FMODUnity.RuntimeManager.StudioSystem.setListenerWeight(1, 1f);
            FMODUnity.RuntimeManager.StudioSystem.setListenerWeight(0, 0f);
            drone_ambient.start();
            drone_music.setTimelinePosition(position_dronemusic);
            drone_music.start();
            drone_ambient_screams.start();
        }
        //Get the drone speed value and use it as a parameter
        public void UpdateDroneSpeed(float spd)
        {
            
            float interpolatedValue = Mathf.Lerp(0, 1, spd);
            Debug.Log(interpolatedValue*8);
            drone_ambient.setParameterByName("drone_speed", interpolatedValue * 8);
        }

        // Stops the drone propeller sound after exiting the feed and music loop
        public void StopDroneAmbient()
        {
            drone_music.getTimelinePosition(out position_dronemusic);
            drone_music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            drone_ambient.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            drone_ambient_screams.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

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
        //Pause the scan (interrupted by looking away)
        public void ScanPause()
        {
            drone_scan.setPaused(true);
        }

        //Resume scan
        public void ScanResume()
        {
            drone_scan.setPaused(false);
        }

        //Scan end (both success and fail)
        public void ScanEnd()
        {
            drone_scan.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }

        //Collect specimen/sample with drone
        public void CollectSample()
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/drone/drone_collectsample");
        }

        //Release specimen/sample into the bin/receptacle - THIS MUST BE INVOKED ON THE SCRIPT ATTACHED TO THE BIN
        public void ReleaseSample()
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/drone/drone_releasesample", transform.position);
        }

        //DRILL EVENTS - THOSE ARE 3D SFX'S SO INVOKE ON THE SCRIPT ATTACHED TO A DRILL

        //Drill start
        public void DrillStart()
        {
            drill_start.start();
            drill_penetrating.start();
        }

        //Drill stop
        public void DrillStop()
        {
            drill_start.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            drill_penetrating.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            drill_stop.start();
        }

        //INTERACTABLES

        //Pick up sample (use this for example on input bin when retrieving a sample from the drone)
        public void Pickup()
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/int/int_pickup");
        }

        //Put down sample (use this for example when storing a sample)

        public void PutDown()
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/int/int_putdown");
        }

        //UI - beeps and boops for the control panels (drill, receptacle)

        //UI Browse - play this when highlighting a button with a mouse cursor
        public void UIbrowse()
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/ui/ui_browse");
        }

        //UI Confirm - play this when confirming an action (start drilling, destroy/pass the sample to the drill on the receptacle panel)
        public void UIconfirm()
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/ui/ui_confirm");
        }

        //UI Start - start drilling (it's a 2d SFX so it can't be placed in the DrillStart function since it plays 3d sfx's)
        public void UIstartdrill()
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/ui/ui_drillstart");
        }

        //UI Press - pressing an arrow/lubrication on (it's not the same as Uiconfirm)
        public void UIpress()
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/ui/ui_press");
        }
        //UI Pass sample to station - this should play after passing the sample to the drill station or after ejecting it
        public void UIpass()
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/ui/ui_passtodrill");
        }

        //DNA

        //DNA ambient start
        public void DnaAmbientStart()
        {
            dna_ambient.start();
        }

        //DNA ambient stop
        public void DnaAmbientStop()
        {
            dna_ambient.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }

        //QUANTUM - might be used for sth else

        //Start quantum ambient - basically on start
        public void QuantumAmbientPlay()
        {
            quantum_ambient.start();
        }

        //Stop quantum ambient - rather useless, but just in case
        public void QuantumAmbientStop()
        {
            quantum_ambient.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }

        //Quantum error
        public void QuantumError()
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/quantum/quantum_error");
        }

        //Quantum success
        public void QuantumSuccess()
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/quantum/quantum_success");
        }
        //OTHERS


        //Play footstep
        public void PlayFootstep()
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/player_footsteps", transform.position);
        }



        //MAIN MENU

        //Browse - highlighting option with a cursor
        public void MMbrowse()
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/mainmenu/mainmenu_browse");
        }

        //Select an option (clicking it)
        public void MMselect()
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/mainmenu/mainmenu_select");
        }

        //Launch game from menu
        public void MMlaunchgame()
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/mainmenu/mainmenu_startgame");
        }

        //STRIKES AND GAME OVERS

        //Strike
        public void StrikeSound()
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/gameover/strike");
        }

        //Victory
        public void VictorySound()
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/gameover/victory");
        }

        //Defeat - poison
        public void DefeatPoison()
        {

            FMODUnity.RuntimeManager.PlayOneShot("event:/gameover/defeat");
            FMODUnity.RuntimeManager.PlayOneShot("event:/gameover/poison");
        }

        //Defeat - explosion
        public void DefeatExplosion()
        {
            FMODUnity.RuntimeManager.StudioSystem.setListenerWeight(2, 1f);
            FMODUnity.RuntimeManager.StudioSystem.setListenerWeight(1, 0f);
            FMODUnity.RuntimeManager.StudioSystem.setListenerWeight(0, 0f);
            FMODUnity.RuntimeManager.PlayOneShot("event:/gameover/explosion");
        }
        //Defeat - three strikes
        public void DefeatStrikes()
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/gameover/defeat");
            //Would be cool to check if it is the last strike. If true, set the reverb send level to max
        }


            //R E S E A R C H E R voice-over only

            public void ResearcherVO()
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/vo_researcher");
        }
    }
}
