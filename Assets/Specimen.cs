using Monster.Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Specimen : MonoBehaviour
{
    [SerializeField]
    public GameObject player;
    [SerializeField]
    public float detectionDistance = 3f;
    [SerializeField]
    public GameObject promptUI;

    public TextMeshProUGUI promptUIText;
    Transform droneTransform;
    DroneMovementController droneController;

    [SerializeField]
    public string specimenType;
    [SerializeField]
    public Image fill;
    [SerializeField]
    public float scanDuration = 5f;

    private bool isInProximity = false;
    private bool isTargeted = false;
    private void Start()
    {
        droneTransform = player.GetComponent<Transform>();
        droneController = player.GetComponent<DroneMovementController>();
        promptUIText = promptUI.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        
        float distance = Vector3.Distance(transform.position, droneTransform.position);

        //Vector3 directionToTarget = playerTransform.position - transform.position;


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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (droneController.isDroneFrozen) {
                    droneController.UnfreezeDrone();
                    promptUIText.SetText("Press (Space) to select specimen");
                    isTargeted = false;
                } else
                {
                    droneController.FreezeDrone();
                    droneTransform.LookAt(transform);
                    promptUIText.SetText("Press (E) to scan specimen");
                    isTargeted = true;
                }
                
            }
        }
        if (isTargeted && Input.GetKeyDown(KeyCode.E))
        {
            //do a scan
            StartCoroutine(ScanSpecimen());
            //coroutine for scan time
            //indicator of scan progress
        }
    }

    IEnumerator ScanSpecimen()
    {
        float elapsedTime = 0f;
        float startValue = 0f;
        float completeValue = 375f;
        while (elapsedTime < scanDuration)
        {
            elapsedTime += Time.deltaTime;

            float currentProgress = Mathf.Lerp(startValue, completeValue, elapsedTime);

            fill.transform.localScale = new Vector3 (currentProgress, transform.localScale.y, transform.localScale.z);

            yield return null;
        }
    }




}
