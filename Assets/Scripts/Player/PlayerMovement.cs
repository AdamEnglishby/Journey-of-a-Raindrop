using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, InputActions.IMovementActions
{
    [SerializeField] private float speedMultiplier = 7.5f;
    [SerializeField] private SpriteRenderer forwardDirection, backwardDirection, idle;

    public InputActions Input;
    private Vector3 _moveDirection;
    private Transform _transform;
    
    private void OnEnable()
    {
        _transform = transform;
        if (Input == null)
        {
            Input = new InputActions();
            Input.Movement.SetCallbacks(this);
        }
        Input.Enable();
    }

    private void Update()
    {
        var firstPos = _transform.position;
        _transform.position += _moveDirection * Time.deltaTime * speedMultiplier;
        var difference = firstPos - _transform.position;

        var left = difference.x > 0;
        _transform.localScale = new Vector3(left ? -1 : 1, 1, 1);

        var forwards = difference.y >= 0;
        forwardDirection.gameObject.SetActive(forwards);
        backwardDirection.gameObject.SetActive(!forwards);

        if (difference.magnitude == 0)
        {
            forwardDirection.gameObject.SetActive(false);
            backwardDirection.gameObject.SetActive(false);
            idle.gameObject.SetActive(true);
        }
        else
        {
            idle.gameObject.SetActive(false);
        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        var dir = context.ReadValue<Vector2>();
        
        // only normalise input if magnitude is over 1
        // this allows for more fine-grained analog input
        if (dir.magnitude > 1)
        {
            dir.Normalize();
        }
        
        _moveDirection = dir;
    }
}
