using System.Threading.Tasks;
using DG.Tweening;
using Febucci.UI;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour, InputActions.IDialogueActions
{
    [SerializeField] private TMP_Text speakerText, bodyText;
    [SerializeField] private RectTransform speakerPanel, bodyPanel;

    private bool _active;
    private InputActions _input;
    private TextBox _currentTextBox, _nextTextBox;
    private bool _waitingForInput;
    public bool receivedButton;

    private static DialogueManager _instance;
    public static bool Active => _instance._active;

    private void OnEnable()
    {
        if (_input != null) return;

        _instance = this;
        _input = new InputActions();
        _instance._input.Dialogue.SetCallbacks(_instance);
        _instance._input.Dialogue.Enable();
    }

    public static async Task DoTextBox(TextBox box)
    {
        _instance.bodyText.gameObject.SetActive(true);
        _instance.speakerText.gameObject.SetActive(true);
        
        _instance.bodyText.text = box.text;
        _instance.speakerText.text = box.speakerName;

        _instance._active = true;

        _instance.bodyText.GetComponent<TypewriterByCharacter>().onTextShowed.AddListener(() =>
        {
            _instance._waitingForInput = true;
            _instance.bodyText.GetComponent<TypewriterByCharacter>().onTextShowed.RemoveAllListeners();
        });

        _instance.speakerPanel.DOScale(Vector3.one, 0.1f);
        _instance.bodyPanel.DOScale(Vector3.one, 0.1f);

        while (!_instance.receivedButton)
        {
            await Awaitable.NextFrameAsync();
        }

        _instance.receivedButton = false;
        _instance._waitingForInput = false;

        _instance.speakerPanel.DOScale(Vector3.zero, 0.1f);
        await _instance.bodyPanel.DOScale(Vector3.zero, 0.1f).AsyncWaitForCompletion();

        _instance.bodyText.text = "";
        _instance.speakerText.text = "";

        _instance.bodyText.gameObject.SetActive(false);
        _instance.speakerText.gameObject.SetActive(false);
        _instance._active = false;
    }

    public async void OnContinue(InputAction.CallbackContext context)
    {
        if (!context.action.WasPressedThisFrame()) return;
        if (!_waitingForInput) return;
        await Awaitable.NextFrameAsync();
        receivedButton = true;
        _waitingForInput = false;
    }
}