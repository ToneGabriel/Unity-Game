
public sealed class PlayerWallClimbState : PlayerTouchingWallState
{
    public PlayerWallClimbState(PlayerControlMode mode, PlayerControlModeData data, string animBoolName)
        : base(mode, data, animBoolName) { /* Empty*/ }

    public override void Update()
    {
        base.Update();

        //_playerControlMode.Target_SetVelocityY(_player.StateData.WallClimbVelocity);

        if (_inputY != 1)
            _playerControlMode.ChangeState(PlayerControlMode.StateID.WallGrab);
    }
}