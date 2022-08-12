
public abstract class PlayerGroundedState : PlayerState
{
    // Inputs
    protected int _inputX;
    protected int _inputY;
    protected bool _jumpInput;
    protected bool _grabInput;
    protected bool _dashInput;
    protected bool _rollInput;
    protected bool _primaryAttackInput;
    protected bool _secondaryDefendInput;
    protected bool _spellCastInput;
    // Checks
    protected bool _isGrounded;
    protected bool _isTouchingWall;
    protected bool _isTouchingLedge;
    protected bool _isTouchingCeiling;
    protected bool _canDefend;
    protected bool _canCastSpell;
    

    public PlayerGroundedState(Player player, FiniteStateMachine stateMachine, Data_Player playerData, string animBoolName) 
        : base(player, stateMachine, playerData, animBoolName)
    { }

    public override void Enter()
    {
        base.Enter();

        _player.JumpState.ResetAmountOfJumpsLeft();
        _player.DashState.ResetCanDash();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _inputX = _player.InputHandler.NormalizedInputX;
        _inputY = _player.InputHandler.NormalizedInputY;
        _jumpInput = _player.InputHandler.JumpInput;
        _grabInput = _player.InputHandler.GrabInput;
        _dashInput = _player.InputHandler.DashInput;
        _rollInput = _player.InputHandler.RollInput;
        _primaryAttackInput = _player.InputHandler.PrimaryAttackInput;
        _secondaryDefendInput = _player.InputHandler.SecondaryDefendInput;
        _spellCastInput = _player.InputHandler.SpellCastInput;

        _player.CheckIfShouldFlip(_inputX);

        if (_primaryAttackInput && !_isTouchingCeiling)
            _stateMachine.ChangeState(_player.PrimaryAttackState);
        else if (_secondaryDefendInput && !_isTouchingCeiling && _canDefend)
            _stateMachine.ChangeState(_player.SecondaryDefendState);
        else if (_spellCastInput && !_isTouchingCeiling && _canCastSpell)
            _stateMachine.ChangeState(_player.SpellCastState);
        else if (_jumpInput && _player.JumpState.CanJump())
        {
            _player.InputHandler.UseJumpInput();
            _stateMachine.ChangeState(_player.JumpState);
        }
        else if (!_isGrounded)
        {
            _player.JumpState.DecreaseAmountOfJumpsLeft();
            _stateMachine.ChangeState(_player.InAirState);
        }
        else if (_isTouchingWall && _grabInput && _isTouchingLedge)
            _stateMachine.ChangeState(_player.WallGrabState);
        else if (_dashInput && _player.DashState.CheckIfCanDash() && !_isTouchingCeiling)
            _stateMachine.ChangeState(_player.DashState);
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isGrounded = _player.CheckIfGrounded();
        _isTouchingWall = _player.CheckIfTouchingWall();
        _isTouchingLedge = _player.CheckIfTouchingLedge(_player.transform.right);
        _isTouchingCeiling = _player.CheckIfTouchingCeiling();
        _canDefend = _player.CheckIfCanDefend();
        _canCastSpell = _player.CheckIfCanCastSpell();
    }
}
