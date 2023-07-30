using System.Collections;
using TMPro;
using UnityEngine;

public class BlinkCursor : MonoBehaviour
{
    public float blinkInterval = 0.5f; // Time in seconds for the blink interval

    [SerializeField]
    public TMP_Text textMeshPro;

    private void Start()
    {
       
        StartCoroutine(BlinkCoroutine());
    }

    private IEnumerator BlinkCoroutine()
    {
        while (true)
        {
            // Toggle the TMP_Text component on and off by enabling and disabling it
            textMeshPro.enabled = !textMeshPro.enabled;

            // Wait for the blink interval
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}