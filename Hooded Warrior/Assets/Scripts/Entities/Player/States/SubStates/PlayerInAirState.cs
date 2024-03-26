﻿using UnityEngine;

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

        _inputX         = InputManager.Instance.NormalizedInputX;
        _jumpInput      = InputManager.Instance.JumpInput;
        _jumpInputStop  = InputManager.Instance.JumpInputStop;
        _grabInput      = InputManager.Instance.GrabInput;
        _dashInput      = InputManager.Instance.DashInput;

        CheckJumpMultiplier();

        if (_isGrounded && _player.RBVelocity.y < 0.01f)
            _player.ChangeState((int)PlayerStateID.Land);
        else if (_isTouchingWall && !_isTouchingLedge && !_isGrounded)
            _player.ChangeState((int)PlayerStateID.LedgeClimb);
        else if (_jumpInput && _player._jumpState.CanJump())
        {
            InputManager.Instance.UseJumpInput();
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

            _workspaceVector2.Set(  _player.RBVelocity.x,
                                    Mathf.Clamp(_player.RBVelocity.y,
                                                -_dataPlayer.MaxVelocityY,
                                                _dataPlayer.MaxVelocityY));
            _player.RBVelocity = _workspaceVector2;
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
                _player.SetVelocityY(_player.RBVelocity.y * _dataPlayer.JumpHeightMultiplier);
                _isJumping = false;
            }
            else if (_player.RBVelocity.y <= 0f)
                _isJumping = false;
    }

}