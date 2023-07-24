using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ProximityDetector : MonoBehaviour
{
    [SerializeField]
    public GameObject player;
    [SerializeField]
    public float detectionDistance = 3f;
    [SerializeField]
    public GameObject promptUI;
    [SerializeField]
    public GameObject dronePlayer;

    private bool isInProximity = false;

    void Update()
    {
        Transform playerTransform = player.GetComponent<Transform>();
        float distance = Vector3.Distance(transform.position, playerTransform.position);


        if (distance <= detectionDistance && player.activeInHierarchy)
        {
            if (!isInProximity)
            {
                isInProximity = true;
                promptUI.SetActive(true);
            }
        }
        else
        {
            if (isInProximity)
            {
                isInProximity = false;
                promptUI.SetActive(false);
            }
        }

        if (isInProximity)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.SetActive(false);
                dronePlayer.SetActive(true);
            }
        }
    }
}
