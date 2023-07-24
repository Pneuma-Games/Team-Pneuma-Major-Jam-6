using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Test Specimen", menuName = "Specimen")]
public class SpecimenScriptableObject : ScriptableObject
{
    [TextArea(1, 5)]
    public string description;

    public SpecimenType type;
    public float modifier;

    public GameObject prefab;
}

public enum SpecimenType
{
    Organic,
    Inorganic,
    Hybrid
}