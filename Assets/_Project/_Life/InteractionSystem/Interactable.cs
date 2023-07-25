using System;
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
        
        [SerializeField] private string _name;
        [SerializeField] private string _flavourText;

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
            OnInteractableTriggeredEvent.Invoke();
        }
    }
}
