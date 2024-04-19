using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

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

    public PlayerInAirState(Player player, string animBoolName)
        : base(player, animBoolName) { /*Empty*/ }

    protected override void DoChecks()
    {
        base.DoChecks();

        _isGrounded         = _player.IsGrounded();
        _isTouchingWall     = _player.IsTouchingWall();
        _isTouchingLedge    = _player.IsTouchingLedge(_player.transform.right);

        // Save player position as soon as it detects ledge
        //if (_isTouchingWall && !_isTouchingLedge)
        //    _player.AdvancedStatus.LedgeDetectedposition = _player.transform.position;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _inputX         = InputManager.Instance.NormalizedInputX;
        _jumpInput      = InputManager.Instance.JumpInput;
        //_jumpInputStop  = InputManager.Instance.JumpInputStop;
        _grabInput      = InputManager.Instance.GrabInput;
        _dashInput      = InputManager.Instance.DashInput;

        CheckJumpMultiplier();

        if (_isGrounded && _player.VelocityY < 0.01f)
            _player.ChangeState((int)PlayerStateID.Land);
        else if (_isTouchingWall && !_isTouchingLedge && !_isGrounded)
            _player.ChangeState((int)PlayerStateID.LedgeClimb);
        else if (_jumpInput && _player.CanJump())
            _player.ChangeState((int)PlayerStateID.Jump);
        else if (_isTouchingWall && _grabInput && _isTouchingLedge)
            _player.ChangeState((int)PlayerStateID.WallGrab);
        else if (_isTouchingWall && !_grabInput)
            _player.ChangeState((int)PlayerStateID.WallSlide);
        else if (_dashInput /*&& _player._dashState.CheckIfCanDash()*/)
            _player.ChangeState((int)PlayerStateID.Dash);
        else
        {
            _player.FlipIfShould(_inputX);
            _player.SetVelocity(_player.Data.MovementVelocity * _inputX,
                                Mathf.Clamp(_player.VelocityY,
                                            -_player.Data.MaxVelocityY,
                                            _player.Data.MaxVelocityY));  // prevent falling too fast
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
                _player.SetVelocityY(_player.VelocityY * _player.Data.JumpHeightMultiplier);
                _isJumping = false;
            }
            else if (_player.VelocityY <= 0f)
                _isJumping = false;
    }

}