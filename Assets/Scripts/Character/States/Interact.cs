using UnityEngine;

public class Interact : CharacterState
{
    #region Variables

    private readonly int _idleAnimationHash = Animator.StringToHash("Rogue_idle_01");

    #endregion

    #region State Methods

    public override void EnterState(Character character)
    {
        _character = character;
        _character.SetAnimation(_idleAnimationHash);
        _character.DisableMove();
        _character.SetInteracting(true);
    }

    public override void UpdateState() { }

    public override void FixedUpdateState() { }
    public override void ExitState()
    {
        _character.EnableMove();
        _character.SetInteracting(false);
    }

    #endregion
}