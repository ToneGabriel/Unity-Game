
public sealed class PlayerJumpState : PlayerAbilityState
{
    public PlayerJumpState(PlayerControlMode mode, PlayerControlModeData data, string animBoolName)
        : base(mode, data, animBoolName) { /*Empty*/ }

    public override void Enter()
    {
        base.Enter();

        InputManager.Instance.UseJumpInput();
        _playerControlMode.Target_SetVelocityY(_playerControlModeData.JumpVelocity);
        --_playerControlMode.Target_JumpCount;

        _isAbilityDone = true;
    }
}