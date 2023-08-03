using System;
using System.Collections;
using Life;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SpecimenPanel : MonoBehaviour
{
    //public Action OnThreeStrikes = delegate {  }; 
    
    public static SpecimenPanel Instance;
    
    [SerializeField] private GameObject[] strikes;
    [SerializeField] private GameObject specimenImage;
    [SerializeField] private GameObject specimenIndicator;
    [SerializeField] private GameObject specimenID;

    [SerializeField] private float minIDDisplayTime = 0.3f;
    [SerializeField] private float maxIDDisplayTime = 0.7f;

    private int _strikes;

    //For Testing
    //[SerializeField] private Sprite image;
    private void Start()
    {
        Instance = this;
        CurrentSubject.OnSubjectChanged += HandleNewSubject;
    }

    private void OnDestroy()
    {
        CurrentSubject.OnSubjectChanged -= HandleNewSubject;
    }

    private void HandleNewSubject()
    {
        Debug.Log("Specimen changed" + CurrentSubject.Instance.Specimen);
        if (CurrentSubject.Instance.Specimen == null)
        {
            SetSpecimenImage(null);
            SetSpecimenIndicatorColor(Color.white);
            StartCoroutine(SetSpecimenID(""));
            return;
        }

        Color color;
        switch (CurrentSubject.Instance.Specimen.SpecimenData.Color)
        {
            case SpecimenColor.Red:
                color = Color.red;
                break;
            case SpecimenColor.Yellow:
                color = Color.yellow;
                break;
            case SpecimenColor.Blue:
                color = Color.blue;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        SetSpecimenImage(CurrentSubject.Instance.Specimen.SpecimenData.Image);
        SetSpecimenIndicatorColor(color);
        StartCoroutine(SetSpecimenID(CurrentSubject.Instance.Specimen.SpecimenData.SpecimenId.ToString()));
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
        if (string.IsNullOrEmpty(newID)) {
            specimenID.GetComponent<TextMeshProUGUI>().text = $"ID: ";
        }
        specimenImage.SetActive(!string.IsNullOrEmpty(newID));
    }
    
    public void IncreaseStrikes()
    {
        _strikes++;
        foreach (var s in strikes)
        {
            s.SetActive(false);
        }

        for (var i = 0; i < (_strikes <= strikes.Length ? _strikes : strikes.Length); i++)
        {
            strikes[i].SetActive(true);
        }
        
        if (_strikes == 3) FindObjectOfType<ThreeStrikesSequence>().StrikeThree();
    }
}
