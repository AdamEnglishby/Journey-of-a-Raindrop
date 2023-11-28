using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using Febucci.UI;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace _Morrigan.Scripts.Dialogue
{
    public class DialogueManager : MonoBehaviour, InputActions.IDialogueActions
    {
        [SerializeField] private Transform body, speaker, choices;
        [SerializeField] private TMP_Text speakerText, bodyText;
        [SerializeField] private RectTransform speakerPanel, bodyPanel;
        [SerializeField] private List<TMP_Text> choicesTexts;
        [SerializeField] private Image nextButton;
        private bool active;

        private InputActions _input;
        private TextBox _currentTextBox, _nextTextBox;
        public bool _textBoxVisible, _waitingForInput, _shownText;
        private int _selectedIndex = -1;

        public bool _receivedButton = false;

        public event Action<bool> OnWaitStateChange;
        
        private bool WaitingForInput
        {
            get => _waitingForInput;
            set
            {
                _waitingForInput = value;
                _instance.OnWaitStateChange?.Invoke(value);
            }
        }

        private bool _conversationActive = false;
        private static DialogueManager _instance;
        public static bool ConversationActive => _instance._conversationActive;
        public static bool Active => _instance.active;

        private void _WaitStateChange(bool isWaiting)
        {
            nextButton.transform.DOScale(isWaiting ? Vector3.one : Vector3.zero, 0.1f);
        }

        private void OnEnable()
        {
            OnWaitStateChange += _WaitStateChange;
            if (_input != null) return;

            _instance = this;
            _input = new InputActions();
            _instance._input.Dialogue.SetCallbacks(_instance);
            _instance._input.Dialogue.Enable();
        }

        public static async Task DoTextBox(TextBox box)
        {
            _instance.active = true;
            _instance.bodyText.gameObject.SetActive(true);
            _instance.speakerText.gameObject.SetActive(true);
            
            _instance.bodyText.text = box.text;
            _instance.speakerText.text = box.speakerName;
            
            _instance.bodyText.GetComponent<TypewriterByCharacter>().onTextShowed.AddListener(() =>
            {
                _instance._waitingForInput = true;
                _instance.bodyText.GetComponent<TypewriterByCharacter>().onTextShowed.RemoveAllListeners();
            });

            _instance.speakerPanel.DOScale(Vector3.one, 0.1f);
            _instance.bodyPanel.DOScale(Vector3.one, 0.1f);
            
            while (!_instance._receivedButton)
            {
                await Awaitable.NextFrameAsync();
            }
            _instance._receivedButton = false;
            _instance._waitingForInput = false;
            
            _instance.speakerPanel.DOScale(Vector3.zero, 0.1f);
            _instance.bodyPanel.DOScale(Vector3.zero, 0.1f);

            _instance.bodyText.gameObject.SetActive(false);
            _instance.speakerText.gameObject.SetActive(false);
            await Awaitable.WaitForSecondsAsync(0.1f);
            _instance.active = false;
        }

        public async void OnContinue(InputAction.CallbackContext context)
        {
            if (!context.action.WasPressedThisFrame()) return;
            if (!_waitingForInput) return;
            await Awaitable.NextFrameAsync();
            _receivedButton = true;
            _waitingForInput = false;
        }
        
    }
}