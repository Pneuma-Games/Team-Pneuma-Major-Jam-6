using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//If you reference to this script you can trigger the anim state using  TriggerAnimScript.OnPushActivate();
//This will alternate the state from True and False.
[RequireComponent(typeof(Animator))]
public class TriggerAnimScript : MonoBehaviour
{
    Animator canisterAnim;


    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            OnPushActivate();
        }
    }

    private void Awake()
    {
        canisterAnim = GetComponent<Animator>();
    }
    public void OnPushActivate()
    {
        canisterAnim.SetBool("Animated", !canisterAnim.GetBool("Animated"));
    }
}
