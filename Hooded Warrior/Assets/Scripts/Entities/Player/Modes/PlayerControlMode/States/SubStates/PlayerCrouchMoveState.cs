
public sealed class PlayerCrouchMoveState : PlayerGroundedState
{
    public PlayerCrouchMoveState(PlayerControlMode mode, PlayerControlModeData data, string animBoolName)
        : base(mode, data, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        //_player.SetColiderHeight(_player.StateData.CrouchColliderHeight);
        //_player.SetLightOrbPosition(_player.StateData.CrouchLightOrbPosition);
    }

    public override void Update()
    {
        base.Update();

        //if (_inputX == 0)
        //    _player.ChangeState((int)PlayerStateID.CrouchIdle);
        //else if (_inputY != -1 && !_isTouchingCeiling)
        //    _player.ChangeState((int)PlayerStateID.Move);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        //_player.SetVelocityX(_player.StateData.CrouchMovementVelocity * _inputX);
    }

    public override void Exit()
    {
        base.Exit();

        //_player.SetColiderHeight(_player.StateData.StandColliderHeight);
        //_player.SetLightOrbPosition(_player.StateData.StandLightOrbPosition);
    }
}