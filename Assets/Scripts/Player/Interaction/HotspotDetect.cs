using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CircleCollider2D))]
public class HotspotDetect : MonoBehaviour, InputActions.IInteractionActions
{
    public ActionList currentInteractable;
    public PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement.Input.Interaction.SetCallbacks(this);
    }

    private async void Interact()
    {
        await currentInteractable.Run();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out ActionList hotspot))
        {
            currentInteractable = hotspot;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.TryGetComponent(out ActionList hotspot)) return;
        
        if (hotspot == currentInteractable)
        {
            currentInteractable = null;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.action.WasPressedThisFrame()) return;
        
        if (currentInteractable && !DialogueManager.Active)
        {
            Interact();
        }
    }
}
