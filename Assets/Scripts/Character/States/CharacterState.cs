public enum ECharacterState
{
    IDLE,
    MOVEMENT,
    INTERACT
}

public abstract class CharacterState
{
    protected Character _character = null;

    public abstract void EnterState(Character character);
    public abstract void UpdateState();
    public abstract void FixedUpdateState();
    public abstract void ExitState();
}