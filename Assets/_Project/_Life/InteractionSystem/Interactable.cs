using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Life.InteractionSystem
{
    public class Interactable : MonoBehaviour
    {
        public UnityEvent OnInteractableSelectedEvent;
        public UnityEvent OnInteractableDeselectedEvent;
        public UnityEvent OnInteractableTriggeredEvent;
        
        public string Name => _name;
        public string FlavourText => _flavourText;
        public bool ConditionsMet => _conditions.Length == 0 || _conditions.All(condition => condition.CanInteract());
        
        [SerializeField] private string _name;
        [SerializeField] private string _flavourText;

        private IInteractionCondition[] _conditions;

        public void Select()
        {
            OnInteractableSelectedEvent.Invoke();
        }

        public void Deselect()
        {
            OnInteractableDeselectedEvent.Invoke();
        }

        public void Trigger()
        {
            if (!ConditionsMet) return;
            OnInteractableTriggeredEvent.Invoke();
        }

        private void Awake()
        {
            _conditions = GetComponents<IInteractionCondition>();
        }

        public string GetErrorMessage()
        {
            foreach(var condition in _conditions)
            {
                if (!condition.CanInteract())
                {
                    return condition.GetErrorMessage();
                }
            }
            return string.Empty;
        }
    }
}
