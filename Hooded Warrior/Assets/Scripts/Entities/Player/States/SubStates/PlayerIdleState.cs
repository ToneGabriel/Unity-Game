
public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, FiniteStateMachine stateMachine, Data_Player playerData, string animBoolName) 
        : base(player, stateMachine, playerData, animBoolName)
    { }

    public override void Enter()
    {
        base.Enter();

        _player.SetVelocityX(0f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_inputX != 0)
            _stateMachine.ChangeState(_player.MoveState);
        else if (_inputY == -1)
            _stateMachine.ChangeState(_player.CrouchIdleState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        _player.SetVelocityX(0f);
    }
}
