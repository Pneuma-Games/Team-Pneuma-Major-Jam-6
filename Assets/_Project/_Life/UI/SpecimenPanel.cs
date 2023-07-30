using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpecimenPanel : MonoBehaviour
{
    [SerializeField] private GameObject[] strikes;
    [SerializeField] private GameObject specimenImage;
    [SerializeField] private GameObject specimenIndicator;
    [SerializeField] private GameObject specimenID;

    [SerializeField] private float minIDDisplayTime = 0.3f;
    [SerializeField] private float maxIDDisplayTime = 0.7f;

    //For Testing
    //[SerializeField] private Sprite image;
    private void Start()
    {
        //SetSpecimenImage(image);
        //SetSpecimenIndicatorColor(Color.blue);
        //StartCoroutine(SetSpecimenID("test string"));
        //SetStrikeCount(2);
    }

    public void SetSpecimenImage(Sprite newImage)
    {
        specimenImage.GetComponent<Image>().sprite = newImage;
        specimenImage.SetActive(newImage != null);
    }
    
    public void SetSpecimenIndicatorColor(Color newColor)
    {
        specimenIndicator.GetComponent<Image>().color = newColor;
    }
    
    public IEnumerator SetSpecimenID(string newID)
    {
        var newString = "";

        foreach (var c in newID)
        {
            newString += c;
            specimenID.GetComponent<TextMeshProUGUI>().text = $"ID: {newString}";

            yield return new WaitForSeconds(Random.Range(minIDDisplayTime, maxIDDisplayTime));
        }

        specimenImage.SetActive(!string.IsNullOrEmpty(newID));
    }
    
    public void SetStrikeCount(int count)
    {
        foreach (var s in strikes)
        {
            s.SetActive(false);
        }

        for (var i = 0; i < (count <= strikes.Length ? count : strikes.Length); i++)
        {
            strikes[i].SetActive(true);
        }
    }
}
