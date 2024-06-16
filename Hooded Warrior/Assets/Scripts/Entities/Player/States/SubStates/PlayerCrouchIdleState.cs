
public sealed class PlayerCrouchIdleState : PlayerGroundedState
{
    public PlayerCrouchIdleState(Player player, string animBoolName)
        : base(player, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        _player.SetVelocityZero();
        _player.SetColiderHeight(_player.StateData.CrouchColliderHeight);
        _player.SetLightOrbPosition(_player.StateData.CrouchLightOrbPosition);
    }

    public override void Exit()
    {
        base.Exit();

        _player.SetColiderHeight(_player.StateData.StandColliderHeight);
        _player.SetLightOrbPosition(_player.StateData.StandLightOrbPosition);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_inputX != 0)
            _player.ChangeState((int)PlayerStateID.CrouchMove);
        else if (_inputY != -1 && !_isTouchingCeiling)
            _player.ChangeState((int)PlayerStateID.Idle);
    }
}