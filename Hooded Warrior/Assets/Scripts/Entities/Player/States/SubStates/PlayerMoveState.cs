
public sealed class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, FiniteStateMachine stateMachine, Data_Player playerData, string animBoolName)
        : base(player, stateMachine, playerData, animBoolName)
    { }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_inputX == 0)
            _stateMachine.ChangeState(_player._idleState);
        else if (_inputY == -1)
            _stateMachine.ChangeState(_player._crouchMoveState);
        else if (_isGrounded && _rollInput)
        {
            _player._inputHandler.UseRollInput();
            _stateMachine.ChangeState(_player._rollState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        _player.SetVelocityX(_dataPlayer.MovementVelocity * _inputX);
    }
}