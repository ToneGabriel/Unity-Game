
public sealed class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerControlMode mode, PlayerControlModeData data, string animBoolName)
        : base(mode, data, animBoolName) { /* Empty */ }

    public override void Enter()
    {
        base.Enter();

        _playerControlMode.Target_SetVelocityZero();
    }

    public override void Update()
    {
        base.Update();

        if (_inputX != 0)
            _playerControlMode.ChangeState(PlayerControlMode.StateID.Move);
        else if (_inputY == -1)
            _playerControlMode.ChangeState(PlayerControlMode.StateID.CrouchIdle);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        _playerControlMode.Target_SetVelocityZero();
    }

    public override void Exit()
    {
        base.Exit();
    }
}