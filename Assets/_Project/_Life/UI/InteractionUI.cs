using Life.InteractionSystem;
using TMPro;
using UnityEngine;

namespace Life
{
    public class InteractionUI : MonoBehaviour
    {
        [SerializeField] private GameObject _uiGroup;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _message;

        private void OnEnable()
        {
            Interactor.OnInteractableSelectedEvent += OnInteractableSelected;
            Interactor.OnInteractableDeselectedEvent += OnInteractableDeselected;
        }


        private void OnDisable()
        {
            Interactor.OnInteractableSelectedEvent -= OnInteractableSelected;
            Interactor.OnInteractableDeselectedEvent -= OnInteractableDeselected;
        }

        private void OnInteractableDeselected()
        {
            _name.SetText(string.Empty);
            _message.SetText(string.Empty);
            _uiGroup.SetActive(false);
        }
        
        private void OnInteractableSelected(string name, string desc, string error)
        {
            _uiGroup.SetActive(true);
            _name.SetText(name);
            if (error != string.Empty)
            {
                _message.SetText(error);
            }
            else
            {
                _message.SetText(desc);
            }
        }
    }
}