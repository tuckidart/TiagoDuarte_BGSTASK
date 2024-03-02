using UnityEngine;

public class Character : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private Animator _animator = null;

    [SerializeField]
    private Rigidbody2D _rigidBody = null;

    [Space]

    [SerializeField]
    private float _moveSpeed = 3f;

    protected int _currentAnimationHash = -1;
    protected bool _canMove = true;
    protected Vector3 _moveInput;

    private bool _facingRight = true;

    #endregion

    #region Unity Methods

    public virtual void LateUpdate()
    {
        CheckChangedAnimationHash();
    }

    #endregion

    #region Other Methods

    public virtual void ChangeState(ECharacterState newState) { }
    public virtual void MoveCharacter(Vector2 moveInput) { }

    public void SetAnimation(int animationHash)
    {
        _currentAnimationHash = animationHash;
    }

    private void CheckChangedAnimationHash()
    {
        if (_animator != null)
        {
            if (_currentAnimationHash != -1)
            {
                _animator.Play(_currentAnimationHash, 0);
            }
        }
    }

    public void Flip()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        _facingRight = !_facingRight;
    }

    public void EnableMove() => _canMove = true;

    public void DisableMove() => _canMove = false;

    #endregion

    #region Gets

    public Rigidbody2D RigidBody => _rigidBody;
    public bool IsFacingRight => _facingRight;
    public Vector3 MoveInput => _moveInput;
    public float MoveSpeed => _moveSpeed;

    #endregion
}
