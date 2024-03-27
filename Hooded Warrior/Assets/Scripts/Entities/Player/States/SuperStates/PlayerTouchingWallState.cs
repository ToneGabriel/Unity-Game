
public abstract class PlayerTouchingWallState : PlayerState
{
    protected bool _isGrounded;
    protected bool _isTouchingWall;
    protected bool _isTouchingLedge;
    protected bool _grabInput;
    protected bool _jumpInput;
    protected int _inputX;
    protected int _inputY;

    public PlayerTouchingWallState(Player player, string animBoolName)
        : base(player, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        // TODO
        //_player._jumpState.ResetAmountOfJumpsLeft();
        //_player._jumpState.DecreaseAmountOfJumpsLeft();
        //_player._dashState.ResetCanDash();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _inputX     = InputManager.Instance.NormalizedInputX;
        _inputY     = InputManager.Instance.NormalizedInputY;
        _grabInput  = InputManager.Instance.GrabInput;
        _jumpInput  = InputManager.Instance.JumpInput;

        if (_isGrounded && !_grabInput)
            _player.ChangeState((int)PlayerStateID.Idle);
        else if (!_isTouchingWall)
            _player.ChangeState((int)PlayerStateID.InAir);
        else if (_isTouchingWall && !_isTouchingLedge)
            _player.ChangeState((int)PlayerStateID.LedgeClimb);
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isGrounded = _player.IsGrounded();
        _isTouchingWall = _player.IsTouchingWall();
        _isTouchingLedge = _player.IsTouchingLedge(_player.transform.right);

        // TODO
        //if (_isTouchingWall && !_isTouchingLedge)
        //    _player._ledgeClimbState.SetDetectedPosition(_player.transform.position);
    }
}