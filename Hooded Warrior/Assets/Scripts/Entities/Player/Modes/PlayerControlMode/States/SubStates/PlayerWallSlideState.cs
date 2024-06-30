
public sealed class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(PlayerControlMode mode, PlayerControlModeData data, string animBoolName)
        : base(mode, data, animBoolName) { }

    public override void Update()
    {
        base.Update();

        //_player.SetVelocityY(-_player.StateData.WallSlideVelocity);
        //_player.SetVelocityX(_player.StateData.MovementVelocity * _inputX);

        //if (_grabInput && _inputY == 0)
        //    _player.ChangeState((int)PlayerStateID.WallGrab);
        //else if (_jumpInput && _isTouchingWall)
        //    _player.ChangeState((int)PlayerStateID.WallJump);
        //else if (!_grabInput && _inputX != 0 && _inputX != _player.FacingDirection)
        //    _player.ChangeState((int)PlayerStateID.InAir);
    }
}