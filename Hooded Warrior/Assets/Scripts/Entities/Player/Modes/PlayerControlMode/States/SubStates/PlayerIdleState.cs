
public sealed class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerControlMode mode, PlayerControlModeData data, string animBoolName)
        : base(mode, data, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        //_player.SetVelocityX(0f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //if (_inputX != 0)
        //    _playerControlMode.ChangeState((int)PlayerStateID.Move);
        //else if (_inputY == -1)
        //    _playerControlMode.ChangeState((int)PlayerStateID.CrouchIdle);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        //_player.SetVelocityX(0f);
    }
}