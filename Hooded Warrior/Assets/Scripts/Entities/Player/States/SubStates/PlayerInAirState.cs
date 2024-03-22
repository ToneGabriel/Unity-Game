using UnityEngine;

public sealed class PlayerInAirState : PlayerState
{
    // Inputs
    private int _inputX;
    private bool _jumpInput;
    private bool _jumpInputStop;
    private bool _grabInput;
    private bool _dashInput;
    // Checks
    private bool _isGrounded;
    private bool _isJumping;
    private bool _isTouchingWall;
    private bool _isTouchingLedge;

    private Vector2 _workspaceVector2;

    public PlayerInAirState(Player player, string animBoolName)
        : base(player, animBoolName) { }

    public override void DoChecks()
    {
        base.DoChecks();

        _isGrounded = _player.CheckIfGrounded();
        _isTouchingWall = _player.CheckIfTouchingWall();
        _isTouchingLedge = _player.CheckIfTouchingLedge(_player.transform.right);

        // Save player position as soon as it detects ledge
        if (_isTouchingWall && !_isTouchingLedge)
            _player._ledgeClimbState.SetDetectedPosition(_player.transform.position);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _inputX = _player._inputHandler.NormalizedInputX;
        _jumpInput = _player._inputHandler.JumpInput;
        _jumpInputStop = _player._inputHandler.JumpInputStop;
        _grabInput = _player._inputHandler.GrabInput;
        _dashInput = _player._inputHandler.DashInput;

        CheckJumpMultiplier();

        if (_isGrounded && _player.ObjectComponents.Rigidbody.velocity.y < 0.01f)
            _player.ChangeState((int)PlayerStateID.Land);
        else if (_isTouchingWall && !_isTouchingLedge && !_isGrounded)
            _player.ChangeState((int)PlayerStateID.LedgeClimb);
        else if (_jumpInput && _player._jumpState.CanJump())
        {
            _player._inputHandler.UseJumpInput();
            _player.ChangeState((int)PlayerStateID.Jump);
        }
        else if (_isTouchingWall && _grabInput && _isTouchingLedge)
            _player.ChangeState((int)PlayerStateID.WallGrab);
        else if (_isTouchingWall && !_grabInput)
            _player.ChangeState((int)PlayerStateID.WallSlide);
        else if (_dashInput && _player._dashState.CheckIfCanDash())
            _player.ChangeState((int)PlayerStateID.Dash);
        else
        {
            _player.CheckIfShouldFlip(_inputX);
            _player.SetVelocityX(_dataPlayer.MovementVelocity * _inputX);

            _workspaceVector2.Set(  _player.ObjectComponents.Rigidbody.velocity.x,
                                    Mathf.Clamp(_player.ObjectComponents.Rigidbody.velocity.y,
                                                -_dataPlayer.MaxVelocityY,
                                                _dataPlayer.MaxVelocityY));
            _player.ObjectComponents.Rigidbody.velocity = _workspaceVector2;

            _player.ObjectComponents.Animator.SetFloat(PlayerControllerParameters.VelocityY_f, _player.ObjectComponents.Rigidbody.velocity.y);
            _player.ObjectComponents.Animator.SetFloat(PlayerControllerParameters.VelocityX_f, Mathf.Abs(_player.ObjectComponents.Rigidbody.velocity.x));
        }
    }

    public void SetIsJumping() // used for jump multiplier only
    {
        _isJumping = true;
    }

    private void CheckJumpMultiplier()
    {
        if (_isJumping)
            if (_jumpInputStop)
            {
                _player.SetVelocityY(_player.ObjectComponents.Rigidbody.velocity.y * _dataPlayer.JumpHeightMultiplier);
                _isJumping = false;
            }
            else if (_player.ObjectComponents.Rigidbody.velocity.y <= 0f)
                _isJumping = false;
    }

}