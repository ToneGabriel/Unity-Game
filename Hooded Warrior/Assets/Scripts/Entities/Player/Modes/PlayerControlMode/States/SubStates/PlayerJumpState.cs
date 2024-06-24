
public sealed class PlayerJumpState : PlayerAbilityState
{
    public PlayerJumpState(PlayerControlMode mode, PlayerControlModeData data, string animBoolName)
        : base(mode, data, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        InputManager.Instance.UseJumpInput();
        //_player.SetVelocityY(_player.StateData.JumpVelocity);
        //_player.DecreaseAmountOfJumpsLeft();
        _isAbilityDone = true;
        // TODO
        //_player._inAirState.SetIsJumping();
    }
}