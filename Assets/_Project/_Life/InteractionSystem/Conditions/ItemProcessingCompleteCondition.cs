using UnityEngine;

namespace Life.InteractionSystem
{
    public class ItemProcessingCompleteCondition : MonoBehaviour, IInteractionCondition
    {
        public bool CanInteract()
        {
            var progress = TransportSystem.TransportSystem.GetStoredSpecimenProgress();
            var data = TransportSystem.TransportSystem.GetStoredSpecimenData();
            var drillOk = !data.RequiresDrilling || progress.DrillComplete;
            var dnaOk = !data.RequiresDNA || progress.DNAComplete;
            var quantumOk = progress.QuantumComplete;
            return drillOk && dnaOk && quantumOk;
        }

        public string GetErrorMessage()
        {
            return "Held sample is not fully processed.";
        }
    }
}