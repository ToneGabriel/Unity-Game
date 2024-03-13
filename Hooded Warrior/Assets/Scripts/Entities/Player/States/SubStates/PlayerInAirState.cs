using UnityEngine;

public class PlayerInAirState : PlayerState
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
            _player.LedgeClimbState.SetDetectedPosition(_player.transform.position);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _inputX = _player.InputHandler.NormalizedInputX;
        _jumpInput = _player.InputHandler.JumpInput;
        _jumpInputStop = _player.InputHandler.JumpInputStop;
        _grabInput = _player.InputHandler.GrabInput;
        _dashInput = _player.InputHandler.DashInput;

        CheckJumpMultiplier();

        if (_isGrounded && _player.Rigidbody.velocity.y < 0.01f)
            _stateMachine.ChangeState(_player.LandState);
        else if (_isTouchingWall && !_isTouchingLedge && !_isGrounded)
            _stateMachine.ChangeState(_player.LedgeClimbState);
        else if (_jumpInput && _player.JumpState.CanJump())
        {
            _player.InputHandler.UseJumpInput();
            _stateMachine.ChangeState(_player.JumpState);
        }
        else if (_isTouchingWall && _grabInput && _isTouchingLedge)
            _stateMachine.ChangeState(_player.WallGrabState);
        else if (_isTouchingWall && !_grabInput)
            _stateMachine.ChangeState(_player.WallSlideState);
        else if (_dashInput && _player.DashState.CheckIfCanDash())
            _stateMachine.ChangeState(_player.DashState);
        else
        {
            _player.CheckIfShouldFlip(_inputX);
            _player.SetVelocityX(_dataPlayer.MovementVelocity * _inputX);

            _workspaceVector2.Set(_player.Rigidbody.velocity.x, Mathf.Clamp(_player.Rigidbody.velocity.y, -_dataPlayer.MaxVelocityY, _dataPlayer.MaxVelocityY));
            _player.Rigidbody.velocity = _workspaceVector2;

            _player.Animator.SetFloat(PlayerControllerParameters.VelocityY, _player.Rigidbody.velocity.y);
            _player.Animator.SetFloat(PlayerControllerParameters.VelocityX, Mathf.Abs(_player.Rigidbody.velocity.x));
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
                _player.SetVelocityY(_player.Rigidbody.velocity.y * _dataPlayer.JumpHeightMultiplier);
                _isJumping = false;
            }
            else if (_player.Rigidbody.velocity.y <= 0f)
                _isJumping = false;
    }

}
