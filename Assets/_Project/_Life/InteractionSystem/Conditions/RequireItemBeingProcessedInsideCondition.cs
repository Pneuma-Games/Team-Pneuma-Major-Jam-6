using UnityEngine;

namespace Life.InteractionSystem
{
    public class RequireItemBeingProcessedInsideCondition : MonoBehaviour, IInteractionCondition
    {
        [SerializeField] private bool _negate;
        
        public bool CanInteract()
        {
            var specimen = CurrentSubject.Instance.Specimen;
            if (!specimen)
            {
                return _negate;
            }
            
            if (specimen.SpecimenProgress.Deposited)
            {
                if (specimen.SpecimenProgress.Stored)
                {
                    return _negate;
                }

                return !_negate;
            }
            else
            {
                return _negate;
            }
        }

        public string GetErrorMessage()
        {
            
            if (_negate)
            {
                return "Cannot use when lab is processing an item.";
            }
            else
            {
                return "Requires an item to be processed in the lab.";
            }
        }
    }
}