
public sealed class PlayerCrouchMoveState : PlayerGroundedState
{
    public PlayerCrouchMoveState(Player player, string animBoolName)
        : base(player, animBoolName) { /*Empty*/ }

    public override void Enter()
    {
        base.Enter();

        _player.SetColiderHeight(_player.Data.CrouchColliderHeight);
        _player.SetLightOrbPosition(_player.Data.CrouchLightOrbPosition);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_inputX == 0)
            _player.ChangeState((int)PlayerStateID.CrouchIdle);
        else if (_inputY != -1 && !_isTouchingCeiling)
            _player.ChangeState((int)PlayerStateID.Move);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        _player.SetVelocityX(_player.Data.CrouchMovementVelocity * _inputX);
    }

    public override void Exit()
    {
        base.Exit();

        _player.SetColiderHeight(_player.Data.StandColliderHeight);
        _player.SetLightOrbPosition(_player.Data.StandLightOrbPosition);
    }
}