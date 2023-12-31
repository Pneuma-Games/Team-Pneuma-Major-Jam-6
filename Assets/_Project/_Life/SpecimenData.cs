using System;
using UnityEngine;

namespace Life
{
    [CreateAssetMenu(menuName = "Create SpecimenDataBlueprint", fileName = "SpecimenDataBlueprint", order = 0)]
    public class SpecimenData : ScriptableObject
    {
        // NOTE: DO NOT mutate this data! It is shared for all instances. Mutable variables should go into SpecimenProgress.
        public string Name;
        public int SpecimenId;

        public SpecimenColor Color;
        // Fill in all correct parameters needed to complete station interactions, like drill time, DNA sequence etc.
        public bool RequiresDrilling;
        public bool RequiresDNA;
        public bool RequiresQuantum;
        public bool Toxic;
        public bool Volatile;
        public bool DrillLube;
        public int DrillRpm;
        public int DbKey;
        public string BinarySequence;
        public int SequenceDecoded;
        public string QuantumKey;
        public bool MandatorySpecimen;
        public Sprite Image;
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
        public bool Complete;
    }

    public enum SpecimenColor
    {
        Red,
        Yellow,
        Blue
    }
}
