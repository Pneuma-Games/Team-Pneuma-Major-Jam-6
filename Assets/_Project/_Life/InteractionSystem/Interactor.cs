using System;
using UnityEngine;

namespace Life.InteractionSystem
{
    public class Interactor : MonoBehaviour
    {
        // Hooks for UI to display interactable name and flavour text
        public static Action<string, string> OnInteractableSelectedEvent;
        public static Action OnInteractableDeselectedEvent;
        
        [SerializeField] private LayerMask _blockMask;
        [SerializeField] private float _interactionRange = 1.25f;
        [SerializeField] private float _castRadius = 0.15f;
        [SerializeField] private float _cooldown = .25f;
        [SerializeField] private KeyCode _activationKey = KeyCode.F;
        [SerializeField] private QueryTriggerInteraction _queryTriggers = QueryTriggerInteraction.Ignore;
        [SerializeField] private bool _drawDebugUI;
        
        private Interactable _currentInteractable;
        private RaycastHit _hitBuffer;
        private float _timer;
        
        void Update()
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
                return;
            }
            
            var t = transform;
            var tPos = t.position;
            var hit = Physics.SphereCast(tPos, _castRadius, t.forward, out _hitBuffer, _interactionRange, _blockMask, _queryTriggers);
            if (hit)
            {
                var interactable = _hitBuffer.transform.gameObject.GetComponent<Interactable>();
                if (!interactable && _currentInteractable)
                {
                    HandleDeselect();
                }
                else if (interactable && _currentInteractable == interactable)
                {
                    // maintain current interactable
                } else if (interactable && _currentInteractable && _currentInteractable != interactable)
                {
                    HandleDeselect();
                    HandleSelect(interactable);
                } else if (!_currentInteractable && interactable)
                {
                    HandleSelect(interactable);
                }
            }
            else
            {
                if (_currentInteractable) HandleDeselect();
            }
            
            if (_currentInteractable && _currentInteractable.gameObject.activeInHierarchy == false)
            {
                HandleDeselect();
            }
            
            if (Input.GetKeyDown(_activationKey) && _currentInteractable && _currentInteractable.ConditionsMet)
            {
                Debug.Log($"Interacting with {_currentInteractable.gameObject.name}");
                _currentInteractable.Trigger();
                HandleDeselect();
                _timer = _cooldown;
            }
        }

        private void OnDisable()
        {
            if (_currentInteractable)
            {
                HandleDeselect();
            }

            _timer = 0;
        }
        
        private void HandleDeselect()
        {
            OnInteractableDeselectedEvent?.Invoke();
            _currentInteractable.Deselect();
            _currentInteractable = null;
        }

        private void HandleSelect(Interactable itr)
        {
            _currentInteractable = itr;
            OnInteractableSelectedEvent?.Invoke(_currentInteractable.Name, _currentInteractable.FlavourText);
            _currentInteractable.Select();
        }

        
        private void OnDrawGizmosSelected()
        {
            var t = transform;
            Gizmos.color = Color.green;
            Gizmos.DrawLine(t.position, t.position + t.forward * _interactionRange);
        }

        private void OnGUI()
        {
            if (!_drawDebugUI) return;
            var width = 400;
            var height = 120;
            var w = Screen.width / 2f - width / 2f;
            var h = Screen.height * 0.85f;
            var rect = new Rect(w, h, width, height);

            var info = _currentInteractable ? $"Selected: {_currentInteractable}" : "No interactable selected";
            var name = _currentInteractable ? _currentInteractable.Name : "---";
            var flavourText = _currentInteractable ? _currentInteractable.FlavourText : "---";
            var available = _currentInteractable ? _currentInteractable.ConditionsMet.ToString() : "false";
            GUI.TextArea(rect, $"{info}\nName: {name}\nFlavour: {flavourText}\nAvailable: {available}", GUI.skin.box);
        }
    }
}
