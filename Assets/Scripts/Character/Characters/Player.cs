using UnityEngine.InputSystem;
using UnityEngine;

public class Player : Character
{
    #region Variables

    public static Player Instance { get; private set; } = null;

    [Space]

    [SerializeField]
    private EquipView[] _equipViews = null;

    [Space]

    [SerializeField]
    private PlayerInput _inputs = null;

    private CharacterState _characterState = null;
    private Interact _interact = null;
    private Movement _movement = null;
    private Idle _idle = null;

    #endregion

    #region Unity Methods

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
        _interact = new Interact();
    }

    public void Update()
    {
        _characterState?.UpdateState();
    }

    private void FixedUpdate()
    {
        _characterState?.FixedUpdateState();
    }

    #endregion

    #region State Methods

    public override void ChangeState(ECharacterState characterState)
    {
        _characterState?.ExitState();

        switch (characterState)
        {
            case ECharacterState.INTERACT:
                _characterState = _interact;
                break;
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

    #endregion

    #region Input Methods

    public void EnableInputs()
    {
        _inputs.enabled = true;
    }

    public void Reset()
    {
        UIManager.Instance.CloseCurrentUIs();
        ChangeToIdle();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (_canMove)
        {
            MoveCharacter(context.ReadValue<Vector2>());
        }
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

    #endregion

    #region Equip Methods

    public void EquipItem(ItemData data)
    {
        int it = 0;
        for (int i = 0; i < _equipViews.Length; i++)
        {
            if (_equipViews[i].EquipType == data.type)
            {
                _equipViews[i].Equip(data.Sprites[it++]);
            }
        }
    }

    public void UnequipItem(ItemData data)
    {
        for (int i = 0; i < _equipViews.Length; i++)
        {
            if (_equipViews[i].EquipType == data.type)
            {
                _equipViews[i].Unequip();
            }
        }
    }

    #endregion
}
