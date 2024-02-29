using System.Collections.Generic;
using UnityEngine;

public class Movement : CharacterState
{
    private readonly int _runAnimationHash = Animator.StringToHash("Rogue_run_01");

    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    private float _collisionOffset = 0.05f;

    public override void EnterState(Character character)
    {
        _character = character;
        _character.SetAnimation(_runAnimationHash);
    }

    public override void UpdateState() { }

    public override void FixedUpdateState()
    {
        Vector3 moveInput = _character.MoveInput;
        if (moveInput != Vector3.zero)
        {
            TryMoving();
            //_character.RigidBody.MovePosition(_character.transform.position + (moveInput * _character.MoveSpeed * Time.fixedDeltaTime));
            //CheckFlip();
            return;
        }

        _character.ChangeState(ECharacterState.IDLE);
    }

    private void TryMoving()
    {
        int count = _character.RigidBody.Cast(_character.MoveInput, castCollisions, 
                                              _character.MoveSpeed * Time.fixedDeltaTime + _collisionOffset);

        if (count == 0)
        {
            Vector2 moveVector = _character.MoveInput * _character.MoveSpeed * Time.fixedDeltaTime;
            _character.RigidBody.MovePosition(_character.RigidBody.position + moveVector);
            CheckFlip();
        }
    }

    private void CheckFlip()
    {
        if ((_character.MoveInput.x > 0 && !_character.IsFacingRight) ||
            (_character.MoveInput.x < 0 && _character.IsFacingRight))
        {
            _character.Flip();
        }
    }

    public override void ExitState() { }
}