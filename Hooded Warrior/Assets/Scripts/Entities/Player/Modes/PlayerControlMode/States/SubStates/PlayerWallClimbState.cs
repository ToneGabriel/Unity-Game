
public sealed class PlayerWallClimbState : PlayerTouchingWallState
{
    public PlayerWallClimbState(PlayerControlMode mode, PlayerControlModeData data, string animBoolName)
        : base(mode, data, animBoolName) { }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //_player.SetVelocityY(_player.StateData.WallClimbVelocity);

        //if (_inputY != 1)
        //    _player.ChangeState((int)PlayerStateID.WallGrab);
    }
}