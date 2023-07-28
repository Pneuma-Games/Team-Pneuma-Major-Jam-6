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
    private FMOD.Studio.EventInstance propEvent;
    private bool isInProximity = false;

    private void Start()
    {
        propEvent = FMODUnity.RuntimeManager.CreateInstance("event:/drone/drone_ambient");
    }

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
                propEvent.start();
                FMODUnity.RuntimeManager.PlayOneShot("event:/drone/drone_enterfeed");
                player.SetActive(false);
                dronePlayer.SetActive(true);
            }
        }
    }
}