
public sealed class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(PlayerControlMode mode, PlayerControlModeData data, string animBoolName)
        : base(mode, data, animBoolName) { }

    public override void Update()
    {
        base.Update();

        //_playerControlMode.Target_SetVelocityY(-_player.StateData.WallSlideVelocity);
        //_playerControlMode.Target_SetVelocityX(_player.StateData.MovementVelocity * _inputX);

        if (_grabInput && _inputY == 0)
            _playerControlMode.ChangeState(PlayerControlMode.StateID.WallGrab);
        else if (_jumpInput && _isTouchingWall)
            _playerControlMode.ChangeState(PlayerControlMode.StateID.WallJump);
        //else if (!_grabInput && _inputX != 0 && _inputX != _player.FacingDirection)
        //    _playerControlMode.ChangeState(PlayerControlMode.StateID.InAir);
    }
}