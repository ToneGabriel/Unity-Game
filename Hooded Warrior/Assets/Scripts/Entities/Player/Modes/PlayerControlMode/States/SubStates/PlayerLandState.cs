
public sealed class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(PlayerControlMode mode, PlayerControlModeData data, string animBoolName)
        : base(mode, data, animBoolName) { }

    public override void Update()
    {
        base.Update();

        //if (_inputX != 0)
        //    _player.ChangeState((int)PlayerStateID.Move);
        //else if (_isStateAnimationFinished)
        //    _player.ChangeState((int)PlayerStateID.Idle);
    }
}