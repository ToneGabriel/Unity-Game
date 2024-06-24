
public sealed class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(PlayerControlMode mode, PlayerControlModeData data, string animBoolName)
        : base(mode, data, animBoolName) { }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //if (_inputX != 0)
        //    _player.ChangeState((int)PlayerStateID.Move);
        //else if (_isStateAnimationFinished)
        //    _player.ChangeState((int)PlayerStateID.Idle);
    }
}