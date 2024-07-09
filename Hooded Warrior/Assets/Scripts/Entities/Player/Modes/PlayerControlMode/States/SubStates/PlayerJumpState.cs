
public sealed class PlayerJumpState : PlayerAbilityState
{
    public PlayerJumpState(PlayerControlMode mode, PlayerControlModeData data, string animBoolName)
        : base(mode, data, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        InputManager.Instance.UseJumpInput();
        // TODO
        //_playerControlMode.Target_SetVelocityY(_player.StateData.JumpVelocity);
        //_player.DecreaseAmountOfJumpsLeft();
        //_player._inAirState.SetIsJumping();
        _isAbilityDone = true;
    }
}