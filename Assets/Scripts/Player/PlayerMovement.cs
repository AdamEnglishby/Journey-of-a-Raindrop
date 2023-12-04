using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerMovement : MonoBehaviour, InputActions.IMovementActions
{
    [SerializeField] private float speedMultiplier = 7.5f;

    public InputActions Input;
    
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Vector3 _velocity;
    
    private Vector3 _moveDirection;
    
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Forwards = Animator.StringToHash("Forwards");

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        
        if (Input == null)
        {
            Input = new InputActions();
            Input.Movement.SetCallbacks(this);
        }
        Input.Enable();
    }

    private void Update()
    {
        var targetVelocity = _moveDirection * speedMultiplier;
        _rigidbody.velocity = Vector3.SmoothDamp(_rigidbody.velocity, targetVelocity, ref _velocity, 0.01f);

        _animator.SetFloat(Speed, _rigidbody.velocity.magnitude / speedMultiplier);

        if (!(_moveDirection.magnitude > 0)) return;

        switch (_moveDirection.y)
        {
            case > 0: 
                _animator.SetBool(Forwards, false);
                break;
            case < 0:
                _animator.SetBool(Forwards, true);
                break;
        }
        
        switch (_moveDirection.x)
        {
            case > 0:
                transform.DOScaleX(1, 0.25f);
                break;
            case < 0:
                transform.DOScaleX(-1, 0.25f);
                break;
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
