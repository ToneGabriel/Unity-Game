
public sealed class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(Player player, FiniteStateMachine stateMachine, Data_Player playerData, string animBoolName)
        : base(player, stateMachine, playerData, animBoolName)
    { }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _player.SetVelocityY(-_dataPlayer.WallSlideVelocity);
        _player.SetVelocityX(_dataPlayer.MovementVelocity * _inputX);

        if (_grabInput && _inputY == 0)
            _stateMachine.ChangeState(_player._wallGrabState);
        else if (_jumpInput && _isTouchingWall)
            _stateMachine.ChangeState(_player._wallJumpState);
        else if (!_grabInput && _inputX != 0 && _inputX != _player.FacingDirection)
            _stateMachine.ChangeState(_player._inAirState);
    }
}