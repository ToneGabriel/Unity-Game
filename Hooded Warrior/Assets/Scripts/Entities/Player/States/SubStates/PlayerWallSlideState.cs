
public sealed class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(Player player, string animBoolName)
        : base(player, animBoolName) { }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _player.SetVelocityY(-_dataPlayer.WallSlideVelocity);
        _player.SetVelocityX(_dataPlayer.MovementVelocity * _inputX);

        if (_grabInput && _inputY == 0)
            _player.ChangeState((int)PlayerStateID.WallGrab);
        else if (_jumpInput && _isTouchingWall)
            _player.ChangeState((int)PlayerStateID.WallJump);
        else if (!_grabInput && _inputX != 0 && _inputX != _player.EntityIntStatusComponents.FacingDirection)
            _player.ChangeState((int)PlayerStateID.InAir);
    }
}