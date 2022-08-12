
public abstract class PlayerTouchingWallState : PlayerState
{
    protected bool _isGrounded;
    protected bool _isTouchingWall;
    protected bool _isTouchingLedge;
    protected bool _grabInput;
    protected bool _jumpInput;
    protected int _inputX;
    protected int _inputY;

    public PlayerTouchingWallState(Player player, FiniteStateMachine stateMachine, Data_Player dataPlayer, string animBoolName) 
        : base(player, stateMachine, dataPlayer, animBoolName)
    { }

    public override void Enter()
    {
        base.Enter();

        _player.JumpState.ResetAmountOfJumpsLeft();
        _player.JumpState.DecreaseAmountOfJumpsLeft();
        _player.DashState.ResetCanDash();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _inputX = _player.InputHandler.NormalizedInputX;
        _inputY = _player.InputHandler.NormalizedInputY;
        _grabInput = _player.InputHandler.GrabInput;
        _jumpInput = _player.InputHandler.JumpInput;

        if (_isGrounded && !_grabInput)
            _stateMachine.ChangeState(_player.IdleState);
        else if (!_isTouchingWall)
            _stateMachine.ChangeState(_player.InAirState);
        else if (_isTouchingWall && !_isTouchingLedge)
            _stateMachine.ChangeState(_player.LedgeClimbState);
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isGrounded = _player.CheckIfGrounded();
        _isTouchingWall = _player.CheckIfTouchingWall();
        _isTouchingLedge = _player.CheckIfTouchingLedge(_player.transform.right);

        if (_isTouchingWall && !_isTouchingLedge)
            _player.LedgeClimbState.SetDetectedPosition(_player.transform.position);
    }
}
