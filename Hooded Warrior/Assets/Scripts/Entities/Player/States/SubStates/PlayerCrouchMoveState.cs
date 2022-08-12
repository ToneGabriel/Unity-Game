
public class PlayerCrouchMoveState : PlayerGroundedState
{
    public PlayerCrouchMoveState(Player player, FiniteStateMachine stateMachine, Data_Player playerData, string animBoolName) 
        : base(player, stateMachine, playerData, animBoolName)
    { }

    public override void Enter()
    {
        base.Enter();

        _player.SetColiderHeight(_dataPlayer.CrouchColliderHeight);
        _player.SetLightOrbPosition(_dataPlayer.CrouchLightOrbPosition);
    }

    public override void Exit()
    {
        base.Exit();
        
        _player.SetColiderHeight(_dataPlayer.StandColliderHeight);
        _player.SetLightOrbPosition(_dataPlayer.StandLightOrbPosition);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_inputX == 0)
            _stateMachine.ChangeState(_player.CrouchIdleState);
        else if (_inputY != -1 && !_isTouchingCeiling)
            _stateMachine.ChangeState(_player.MoveState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        _player.SetVelocityX(_dataPlayer.CrouchMovementVelocity * _player.FacingDirection);
    }

}
