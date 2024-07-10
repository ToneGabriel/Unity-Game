
public sealed class PlayerCrouchMoveState : PlayerGroundedState
{
    public PlayerCrouchMoveState(PlayerControlMode mode, PlayerControlModeData data, string animBoolName)
        : base(mode, data, animBoolName) { /*Empty*/ }

    public override void Enter()
    {
        base.Enter();

        //_player.SetColiderHeight(_player.StateData.CrouchColliderHeight);
        //_player.SetLightOrbPosition(_player.StateData.CrouchLightOrbPosition);
    }

    public override void Update()
    {
        base.Update();

        if (_inputX == 0)
            _playerControlMode.ChangeState(PlayerControlMode.StateID.CrouchIdle);
        else if (_inputY != -1 && !_isTouchingCeiling)
            _playerControlMode.ChangeState(PlayerControlMode.StateID.Move);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        //_playerControlMode.Target_SetVelocityX(_player.StateData.CrouchMovementVelocity * _inputX);
    }

    public override void Exit()
    {
        base.Exit();

        //_player.SetColiderHeight(_player.StateData.StandColliderHeight);
        //_player.SetLightOrbPosition(_player.StateData.StandLightOrbPosition);
    }
}