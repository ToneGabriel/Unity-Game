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

    public PlayerInAirState(Player player, FiniteStateMachine stateMachine, Data_Player dataPlayer, string animBoolName)
        : base(player, stateMachine, dataPlayer, animBoolName)
    { }

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

        if (_isGrounded && _player._rigidbody.velocity.y < 0.01f)
            _stateMachine.ChangeState(_player._landState);
        else if (_isTouchingWall && !_isTouchingLedge && !_isGrounded)
            _stateMachine.ChangeState(_player._ledgeClimbState);
        else if (_jumpInput && _player._jumpState.CanJump())
        {
            _player._inputHandler.UseJumpInput();
            _stateMachine.ChangeState(_player._jumpState);
        }
        else if (_isTouchingWall && _grabInput && _isTouchingLedge)
            _stateMachine.ChangeState(_player._wallGrabState);
        else if (_isTouchingWall && !_grabInput)
            _stateMachine.ChangeState(_player._wallSlideState);
        else if (_dashInput && _player._dashState.CheckIfCanDash())
            _stateMachine.ChangeState(_player._dashState);
        else
        {
            _player.CheckIfShouldFlip(_inputX);
            _player.SetVelocityX(_dataPlayer.MovementVelocity * _inputX);

            _workspaceVector2.Set(_player._rigidbody.velocity.x, Mathf.Clamp(_player._rigidbody.velocity.y, -_dataPlayer.MaxVelocityY, _dataPlayer.MaxVelocityY));
            _player._rigidbody.velocity = _workspaceVector2;

            _player._animator.SetFloat(PlayerControllerParameters.VelocityY_f, _player._rigidbody.velocity.y);
            _player._animator.SetFloat(PlayerControllerParameters.VelocityX_f, Mathf.Abs(_player._rigidbody.velocity.x));
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
                _player.SetVelocityY(_player._rigidbody.velocity.y * _dataPlayer.JumpHeightMultiplier);
                _isJumping = false;
            }
            else if (_player._rigidbody.velocity.y <= 0f)
                _isJumping = false;
    }

}