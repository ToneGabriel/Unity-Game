
public sealed class PlayerCrouchMoveState : PlayerGroundedState
{
    public PlayerCrouchMoveState(PlayerControlMode mode, PlayerControlModeData data, string animBoolName)
        : base(mode, data, animBoolName) { /*Empty*/ }

    public override void Enter()
    {
        base.Enter();

        _playerControlMode.Target_SetBoxColliderHeight(_playerControlModeData.CrouchColliderHeight);
        _playerControlMode.Target_SetLightOrbPosition(_playerControlModeData.CrouchLightOrbPosition);
    }

    public override void Update()
    {
        base.Update();

        if (_inputX == 0)
            _playerControlMode.ChangeState(PlayerControlMode.StateID.CrouchIdle);
        else if (_inputY != -1 && !_isTouchingCeiling)
            _playerControlMode.ChangeState(PlayerControlMode.StateID.Move);

        _playerControlMode.Target_FlipOnInputX(_inputX);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        _playerControlMode.Target_SetVelocityX(_playerControlModeData.CrouchMovementVelocity * _inputX);
    }

    public override void Exit()
    {
        base.Exit();

        _playerControlMode.Target_SetBoxColliderHeight(_playerControlModeData.StandColliderHeight);
        _playerControlMode.Target_SetLightOrbPosition(_playerControlModeData.StandLightOrbPosition);
    }
}