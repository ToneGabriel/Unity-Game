
public sealed class PlayerJumpState : PlayerAbilityState
{
    public PlayerJumpState(Player player, string animBoolName)
        : base(player, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        InputManager.Instance.UseJumpInput();
        _player.SetVelocityY(_player.Data.JumpVelocity);
        _player.DecreaseAmountOfJumpsLeft();
        _isAbilityDone = true;
        // TODO
        //_player._inAirState.SetIsJumping();
    }
}