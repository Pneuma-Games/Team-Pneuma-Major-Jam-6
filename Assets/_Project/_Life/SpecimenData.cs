using System;
using UnityEngine;

namespace Life
{
    [CreateAssetMenu(menuName = "Create SpecimenDataBlueprint", fileName = "SpecimenDataBlueprint", order = 0)]
    public class SpecimenData : ScriptableObject
    {
        // NOTE: DO NOT mutate this data! It is shared for all instances. Mutable variables should go into SpecimenProgress.
        public string Name;
        // Fill in all correct parameters needed to complete station interactions, like drill time, DNA sequence etc.
        public bool RequiresDrilling;
        public bool RequiresDNA;
        public bool Dangerous;
        public bool DrillLube;
        public int DrillRpm;
    }

    [Serializable]
    public class SpecimenProgress
    {
        public bool Scanned;
        public bool Deposited;
        public bool DrillComplete;
        public bool DNAComplete;
        public bool QuantumComplete;
        public bool Stored;
        public bool Destroyed;
    }
}
