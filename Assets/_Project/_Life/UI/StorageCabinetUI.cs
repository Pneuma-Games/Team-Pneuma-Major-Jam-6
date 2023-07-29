using TMPro;
using UnityEngine;

public class StorageCabinetUI : MonoBehaviour
{
    [SerializeField] GameObject[] labels;

    public void SetLabel(string labelText, int labelIndex)
    {
        if (labels.Length > labelIndex)
        {
            labels[labelIndex].GetComponent<TextMeshProUGUI>().text = labelText;
        }
    }
}
