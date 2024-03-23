
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


    public PlayerGroundedState(Player player, string animBoolName)
        : base(player, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        _player._jumpState.ResetAmountOfJumpsLeft();
        _player._dashState.ResetCanDash();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _inputX                 = InputManager.Instance.NormalizedInputX;
        _inputY                 = InputManager.Instance.NormalizedInputY;
        _jumpInput              = InputManager.Instance.JumpInput;
        _grabInput              = InputManager.Instance.GrabInput;
        _dashInput              = InputManager.Instance.DashInput;
        _rollInput              = InputManager.Instance.RollInput;
        _primaryAttackInput     = InputManager.Instance.PrimaryAttackInput;
        _secondaryDefendInput   = InputManager.Instance.SecondaryDefendInput;
        _spellCastInput         = InputManager.Instance.SpellCastInput;

        _player.CheckIfShouldFlip(_inputX);

        if (_primaryAttackInput && !_isTouchingCeiling)
            _player.ChangeState((int)PlayerStateID.PrimaryAttack);
        else if (_secondaryDefendInput && !_isTouchingCeiling && _canDefend)
            _player.ChangeState((int)PlayerStateID.SecondaryDefend);
        else if (_spellCastInput && !_isTouchingCeiling && _canCastSpell)
            _player.ChangeState((int)PlayerStateID.SpellCast);
        else if (_jumpInput && _player._jumpState.CanJump())
        {
            InputManager.Instance.UseJumpInput();
            _player.ChangeState((int)PlayerStateID.Jump);
        }
        else if (!_isGrounded)
        {
            _player._jumpState.DecreaseAmountOfJumpsLeft();
            _player.ChangeState((int)PlayerStateID.InAir);
        }
        else if (_isTouchingWall && _grabInput && _isTouchingLedge)
            _player.ChangeState((int)PlayerStateID.WallGrab);
        else if (_dashInput && _player._dashState.CheckIfCanDash() && !_isTouchingCeiling)
            _player.ChangeState((int)PlayerStateID.Dash);
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