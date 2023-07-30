using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Life
{
    public class DNAStationUI : MonoBehaviour
    {
        [SerializeField] public bool AcceptsInput;

        public string Code
        {
            get
            {
                var sb = new StringBuilder();
                sb.Append(_buffer.ToArray());
                return sb.ToString();
            }
        }

        [SerializeField] private GameObject _errorGroup;
        [SerializeField] private GameObject _successGroup;
        [SerializeField] private GameObject _inputGroup;

        [SerializeField] private TextMeshProUGUI _specimenOutput;
        [SerializeField] private TextMeshProUGUI _input;
        [SerializeField] private TextMeshProUGUI _specimenStatus;
        [SerializeField] private DNAStation _station;
        private bool _caretVisible;
        private int _maxBufferLen = 8;
        
        public void SetSpecimenPresent(bool yes)
        {
            _specimenStatus.SetText(yes ? "Present" : "Empty");
        }

        public void ResetInput()
        {
            _buffer.Clear();
        }

        public void ShowError()
        {
            _errorGroup.SetActive(true);
            _successGroup.SetActive(false);
            _inputGroup.SetActive(false);
        }

        public void ShowSuccess()
        {
            _successGroup.SetActive(true);
            _errorGroup.SetActive(false);
            _inputGroup.SetActive(false);
        }
        
        public void ShowInput()
        {
            _inputGroup.SetActive(true);
            _errorGroup.SetActive(false);
            _successGroup.SetActive(false);
        }

        public void SetSpecimenOutput(string code)
        {
            _specimenOutput.SetText(code);
        }
        
        public void HandleButtonClick()
        {
            if (!_station.ProcessingItem) return;
            _station.ProcessItem();
        }

        public void SetAcceptInput(bool yes)
        {
            AcceptsInput = yes;
        }
        
        
        void Start()
        {
            Keyboard.current.onTextInput += HandleKeypress;
        }

        private void OnDestroy()
        {
            Keyboard.current.onTextInput -= HandleKeypress;

        }

        private List<char> _buffer = new List<char>();
        // Update is called once per frame
        void Update()
        {
            if (!AcceptsInput) return;
            if (Keyboard.current.backspaceKey.wasPressedThisFrame && _buffer.Count > 0)
            {
                _buffer.RemoveAt(_buffer.Count - 1);
                _buffer.TrimExcess();
            }
            
            if (Keyboard.current.enterKey.wasPressedThisFrame)
            {
                HandleButtonClick();
            }

            if (Time.frameCount % 200 == 0) _caretVisible = !_caretVisible;

            var sb = new StringBuilder();
            //if (_caretVisible) sb.Append("▮");
            sb.Append(_buffer.ToArray());
            if (_caretVisible) sb.Append("|");

            _input.SetText(sb.ToString().ToLower());
        }

        private void HandleKeypress(char c)
        {
            if (!char.IsDigit(c) || !AcceptsInput) return;
            if (_buffer.Count >= _maxBufferLen) return;
            _buffer.Add(c);
        }
    }
}