
public sealed class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(PlayerControlMode mode, PlayerControlModeData data, string animBoolName)
        : base(mode, data, animBoolName) { }

    public override void Update()
    {
        base.Update();

        if (_inputX != 0)
            _playerControlMode.ChangeState(PlayerControlMode.StateID.Move);
        else if (_isStateAnimationFinished)
            _playerControlMode.ChangeState(PlayerControlMode.StateID.Idle);
    }
}