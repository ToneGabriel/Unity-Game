
public sealed class PlayerCrouchIdleState : PlayerGroundedState
{
    public PlayerCrouchIdleState(PlayerControlMode mode, PlayerControlModeData data, string animBoolName)
        : base(mode, data, animBoolName) { /*Empty*/ }

    public override void Enter()
    {
        base.Enter();

        _playerControlMode.Target_SetVelocityZero();
        _playerControlMode.Target_SetBoxColliderHeight(_playerControlModeData.CrouchColliderHeight);
        _playerControlMode.Target_SetLightOrbPosition(_playerControlModeData.CrouchLightOrbPosition);
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

        _playerControlMode.Target_SetBoxColliderHeight(_playerControlModeData.StandColliderHeight);
        _playerControlMode.Target_SetLightOrbPosition(_playerControlModeData.StandLightOrbPosition);
    }
}