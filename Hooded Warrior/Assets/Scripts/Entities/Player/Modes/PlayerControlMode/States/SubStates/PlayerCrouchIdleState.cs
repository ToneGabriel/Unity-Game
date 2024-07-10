
public sealed class PlayerCrouchIdleState : PlayerGroundedState
{
    public PlayerCrouchIdleState(PlayerControlMode mode, PlayerControlModeData data, string animBoolName)
        : base(mode, data, animBoolName) { /*Empty*/ }

    public override void Enter()
    {
        base.Enter();

        _playerControlMode.Target_SetVelocityZero();
        //_player.SetColiderHeight(_player.StateData.CrouchColliderHeight);
        //_player.SetLightOrbPosition(_player.StateData.CrouchLightOrbPosition);
    }

    public override void Update()
    {
        base.Update();

        if (_inputX != 0)
            _playerControlMode.ChangeState(PlayerControlMode.StateID.CrouchMove);
        else if (_inputY != -1 && !_isTouchingCeiling)
            _playerControlMode.ChangeState(PlayerControlMode.StateID.Idle);
    }

    public override void Exit()
    {
        base.Exit();

        //_player.SetColiderHeight(_player.StateData.StandColliderHeight);
        //_player.SetLightOrbPosition(_player.StateData.StandLightOrbPosition);
    }
}