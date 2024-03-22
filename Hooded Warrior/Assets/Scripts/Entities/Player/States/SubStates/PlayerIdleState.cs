
public sealed class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, string animBoolName)
        : base(player, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        _player.SetVelocityX(0f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_inputX != 0)
            _player.ChangeState((int)PlayerStateID.Move);
        else if (_inputY == -1)
            _player.ChangeState((int)PlayerStateID.CrouchIdle);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        _player.SetVelocityX(0f);
    }
}