using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{


    [SerializeField]
    public Transform realCameraTransform;

    // Update is called once per frame
    void Update()
    {
        transform.position = realCameraTransform.position;
    }
}
