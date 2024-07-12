
public sealed class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(PlayerControlMode mode, PlayerControlModeData data, string animBoolName)
        : base(mode, data, animBoolName) { /*Empty*/ }

    public override void Update()
    {
        base.Update();

        if (_inputX == 0)
            _playerControlMode.ChangeState(PlayerControlMode.StateID.Idle);
        else if (_inputY == -1)
            _playerControlMode.ChangeState(PlayerControlMode.StateID.CrouchMove);
        else if (_isGrounded && _rollInput)
            _playerControlMode.ChangeState(PlayerControlMode.StateID.Roll);

        _playerControlMode.Target_FlipOnInputX(_inputX);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        _playerControlMode.Target_SetVelocityX(_playerControlModeData.MovementVelocity * _inputX);
    }
}