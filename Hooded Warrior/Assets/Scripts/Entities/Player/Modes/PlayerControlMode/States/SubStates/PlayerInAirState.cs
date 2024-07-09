using UnityEngine;

public sealed class PlayerInAirState : PlayerState
{
    private readonly string _animVelocityXFloatName;
    private readonly string _animVelocityYFloatName;

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

    public PlayerInAirState(PlayerControlMode mode, PlayerControlModeData data, string animBoolName, string animVelocityXFloatName, string animVelocityYFloatName)
        : base(mode, data, animBoolName)
    {
        _animVelocityXFloatName = animVelocityXFloatName;
        _animVelocityYFloatName = animVelocityYFloatName;
    }

    //protected override void DoChecks()
    //{
    //    base.DoChecks();

    //    //_isGrounded         = _player.IsGrounded();
    //    //_isTouchingWall     = _player.IsTouchingWall();
    //    //_isTouchingLedge    = _player.IsTouchingLedge(_player.transform.right);

    //    // Save player position as soon as it detects ledge
    //    //if (_isTouchingWall && !_isTouchingLedge)
    //    //    _player.AdvancedStatus.LedgeDetectedposition = _player.transform.position;
    //}

    public override void Update()
    {
        base.Update();

        _playerControlMode.Target_SetAnimatorFloatParam(_animVelocityXFloatName, _playerControlMode.Target_Velocity.x);
        _playerControlMode.Target_SetAnimatorFloatParam(_animVelocityYFloatName, _playerControlMode.Target_Velocity.y);

        _inputX         = InputManager.Instance.NormalizedInputX;
        _jumpInput      = InputManager.Instance.JumpInput;
        //_jumpInputStop  = InputManager.Instance.JumpInputStop;
        _grabInput      = InputManager.Instance.GrabInput;
        _dashInput      = InputManager.Instance.DashInput;

        CheckJumpMultiplier();

        //if (_isGrounded && _player.VelocityY < 0.01f)
        //    _playerControlMode.ChangeState(PlayerControlMode.StateID.Land);
        //else if (_isTouchingWall && !_isTouchingLedge && !_isGrounded)
        //    _playerControlMode.ChangeState(PlayerControlMode.StateID.LedgeClimb);
        //else if (_jumpInput && _player.CanJump())
        //    _playerControlMode.ChangeState(PlayerControlMode.StateID.Jump);
        //else if (_isTouchingWall && _grabInput && _isTouchingLedge)
        //    _playerControlMode.ChangeState(PlayerControlMode.StateID.WallGrab);
        //else if (_isTouchingWall && !_grabInput)
        //    _playerControlMode.ChangeState(PlayerControlMode.StateID.WallSlide);
        //else if (_dashInput /*&& _player._dashState.CheckIfCanDash()*/)
        //    _playerControlMode.ChangeState(PlayerControlMode.StateID.Dash);
        //else
        //{
        //    _player.FlipIfShould(_inputX);
        //    _playerControlMode.Target_SetVelocity(  _player.StateData.MovementVelocity * _inputX,
        //                                            Mathf.Clamp(_player.VelocityY,
        //                                                        -_player.StateData.MaxVelocityY,
        //                                                        _player.StateData.MaxVelocityY));  // prevent falling too fast
        //}
    }

    public void SetIsJumping() // used for jump multiplier only
    {
        _isJumping = true;
    }

    private void CheckJumpMultiplier()
    {
        //if (_isJumping)
        //    if (_jumpInputStop)
        //    {
        //        _player.SetVelocityY(_player.VelocityY * _player.StateData.JumpHeightMultiplier);
        //        _isJumping = false;
        //    }
        //    else if (_player.VelocityY <= 0f)
        //        _isJumping = false;
    }

}