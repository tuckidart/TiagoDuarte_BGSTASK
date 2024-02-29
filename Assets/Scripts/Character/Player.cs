using UnityEngine.InputSystem;
using UnityEngine;

public class Player : Character
{
    public static Player Instance { get; private set; } = null;

    private CharacterState _characterState = null;

    private Movement _movement = null;
    private Idle _idle = null;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;

        _idle = new Idle();
        _movement = new Movement();
    }

    private void Start()
    {
        ChangeState(ECharacterState.IDLE);
    }

    public void Update()
    {
        _characterState?.UpdateState();
    }

    private void FixedUpdate()
    {
        _characterState?.FixedUpdateState();
    }

    public override void ChangeState(ECharacterState characterState)
    {
        _characterState?.ExitState();

        switch (characterState)
        {
            case ECharacterState.MOVEMENT:
                _characterState = _movement;
                break;
            case ECharacterState.IDLE:
                _characterState = _idle;
                break;
            default:
                break;
        }

        _characterState?.EnterState(this);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveCharacter(context.ReadValue<Vector2>());
    }

    public override void MoveCharacter(Vector2 moveInput)
    {
        _moveInput.x = moveInput.x;
        _moveInput.y = moveInput.y;

        if (_characterState == _idle)
        {
            ChangeState(ECharacterState.MOVEMENT);
        }
    }
}
