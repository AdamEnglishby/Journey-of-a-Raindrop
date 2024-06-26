using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour, InputActions.IMovementActions
{
    [SerializeField] private float speedMultiplier = 7.5f;
    [SerializeField] private Animator[] animators;
    [SerializeField] private AnimationCurve animationSpeedPerMovementSpeed;

    public InputActions Input;
    
    private Rigidbody2D _rigidbody;
    private Vector2 _velocity;

    public PlayerState State;
    private Vector3 _moveDirection;
    
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Forwards = Animator.StringToHash("Forwards");
    private static readonly int AnimationSpeed = Animator.StringToHash("Animation Speed");

    private void OnEnable()
    {
        State = new PlayerState(PlayerState.State.FreeMovement);
        
        _rigidbody = GetComponent<Rigidbody2D>();
        
        if (Input == null)
        {
            Input = new InputActions();
            Input.Movement.SetCallbacks(this);
        }
        Input.Enable();
    }

    private void Update()
    {
        if (!State.CanMove())
        {
            _moveDirection = Vector3.zero;
        }
        
        var targetVelocity = _moveDirection * speedMultiplier;
        _rigidbody.velocity = Vector2.SmoothDamp(_rigidbody.velocity, targetVelocity, ref _velocity, Time.deltaTime);
        

        var speed = _rigidbody.velocity.magnitude / speedMultiplier;
        foreach (var animator in animators)
        {
            animator.SetFloat(Speed, speed);
            animator.SetFloat(AnimationSpeed, animationSpeedPerMovementSpeed.Evaluate(speed));
        }

        if (_moveDirection.magnitude <= 0) return;

        switch (_moveDirection.y)
        {
            case > 0: 
                foreach (var animator in animators)
                {
                    animator.SetBool(Forwards, false);
                }
                break;
            case < 0:
                foreach (var animator in animators)
                {
                    animator.SetBool(Forwards, true);
                }
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
