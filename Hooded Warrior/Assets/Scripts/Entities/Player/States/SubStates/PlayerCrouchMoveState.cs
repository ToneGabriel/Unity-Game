
public sealed class PlayerCrouchMoveState : PlayerGroundedState
{
    public PlayerCrouchMoveState(Player player, string animBoolName)
        : base(player, animBoolName) { }

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
            _player.ChangeState((int)PlayerStateID.CrouchIdle);
        else if (_inputY != -1 && !_isTouchingCeiling)
            _player.ChangeState((int)PlayerStateID.Move);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        _player.SetVelocityX(_dataPlayer.CrouchMovementVelocity * _player.FacingDirection);
    }

}