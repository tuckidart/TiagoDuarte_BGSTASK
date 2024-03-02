using UnityEngine;

public class Idle : CharacterState
{
    #region Variables

    private readonly int _idleAnimationHash = Animator.StringToHash("Rogue_idle_01");

    #endregion

    #region State Methods

    public override void EnterState(Character character)
    {
        _character = character;
        _character.SetAnimation(_idleAnimationHash);
    }

    public override void UpdateState()
    {
        if (_character.MoveInput != Vector3.zero)
        {
            _character.ChangeState(ECharacterState.MOVEMENT);
        }
    }

    public override void FixedUpdateState() { }
    public override void ExitState() { }

    #endregion
}