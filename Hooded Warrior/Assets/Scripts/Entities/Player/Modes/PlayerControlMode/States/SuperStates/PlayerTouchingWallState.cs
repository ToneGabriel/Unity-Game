
public abstract class PlayerTouchingWallState : PlayerState
{
    protected bool _isGrounded;
    protected bool _isTouchingWall;
    protected bool _isTouchingLedge;
    protected bool _grabInput;
    protected bool _jumpInput;
    protected int _inputX;
    protected int _inputY;

    public PlayerTouchingWallState(PlayerControlMode mode, PlayerControlModeData data, string animBoolName)
        : base(mode, data, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        _playerControlMode.Target_ResetAndDecreaseAmountOfJumpsLeft();
        // TODO
        // _player.AdvancedStatus.CanDash = true;
    }

    public override void Update()
    {
        base.Update();

        _inputX     = InputManager.Instance.NormalizedInputX;
        _inputY     = InputManager.Instance.NormalizedInputY;
        _grabInput  = InputManager.Instance.GrabInput;
        _jumpInput  = InputManager.Instance.JumpInput;

        if (_isGrounded && !_grabInput)
            _playerControlMode.ChangeState(PlayerControlMode.StateID.Idle);
        else if (!_isTouchingWall)
            _playerControlMode.ChangeState(PlayerControlMode.StateID.InAir);
        else if (_isTouchingWall && !_isTouchingLedge)
            _playerControlMode.ChangeState(PlayerControlMode.StateID.LedgeClimb);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        //_isGrounded = _player.IsGrounded();
        //_isTouchingWall = _player.IsTouchingWall();
        //_isTouchingLedge = _player.IsTouchingLedge(_player.transform.right);

        //if (_isTouchingWall && !_isTouchingLedge)
        //    _player.AdvancedStatus.LedgeDetectedposition = _player.transform.position;
    }

    public override void Exit()
    {
        base.Exit();
    }
}