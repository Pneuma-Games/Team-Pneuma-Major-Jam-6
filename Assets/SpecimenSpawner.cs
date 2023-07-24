using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecimenSpawner : MonoBehaviour
{


    [SerializeField]
    public SpecimenScriptableObject[] specimenSriptableObjectArray;


    // Start is called before the first frame update
    void Start()
    {
        int randomIndex = Random.Range(0, specimenSriptableObjectArray.Length);

        SpecimenScriptableObject randomSpecimen = specimenSriptableObjectArray[randomIndex];

        GameObject instantiatedSpecimen = Instantiate(randomSpecimen.prefab);
        Specimen specimenScript = instantiatedSpecimen.GetComponent<Specimen>();
        specimenScript.specimenType = randomSpecimen.type.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
