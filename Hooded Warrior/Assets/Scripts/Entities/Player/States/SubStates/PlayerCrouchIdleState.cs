
public class PlayerCrouchIdleState : PlayerGroundedState
{
    public PlayerCrouchIdleState(Player player, FiniteStateMachine stateMachine, Data_Player playerData, string animBoolName) 
        : base(player, stateMachine, playerData, animBoolName)
    { }

    public override void Enter()
    {
        base.Enter();

        _player.SetVelocityZero();
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

        if (_inputX != 0)
            _stateMachine.ChangeState(_player.CrouchMoveState);
        else if (_inputY != -1 && !_isTouchingCeiling)
            _stateMachine.ChangeState(_player.IdleState);
    }
}
