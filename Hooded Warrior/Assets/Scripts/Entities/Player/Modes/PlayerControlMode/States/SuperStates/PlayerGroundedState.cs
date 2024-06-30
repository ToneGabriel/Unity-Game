
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


    public PlayerGroundedState(PlayerControlMode mode, PlayerControlModeData data, string animBoolName)
        : base(mode, data, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        //_playerControlMode.ResetAmountOfJumpsLeft();
        // TODO
        // _player.AdvancedStatus.CanDash = true;
    }

    public override void Update()
    {
        base.Update();

        _inputX                 = InputManager.Instance.NormalizedInputX;
        _inputY                 = InputManager.Instance.NormalizedInputY;
        _jumpInput              = InputManager.Instance.JumpInput;
        _grabInput              = InputManager.Instance.GrabInput;
        _dashInput              = InputManager.Instance.DashInput;
        _rollInput              = InputManager.Instance.RollInput;
        _primaryAttackInput     = InputManager.Instance.PrimaryAttackInput;
        _secondaryDefendInput   = InputManager.Instance.SecondaryDefendInput;
        _spellCastInput         = InputManager.Instance.SpellCastInput;

        //_playerControlMode.FlipIfShould(_inputX);

        //if (_primaryAttackInput && !_isTouchingCeiling)
        //    _playerControlMode.ChangeState((int)PlayerStateID.PrimaryAttack);
        //else if (_secondaryDefendInput && !_isTouchingCeiling && _canDefend)
        //    _playerControlMode.ChangeState((int)PlayerStateID.SecondaryDefend);
        //else if (_spellCastInput && !_isTouchingCeiling && _canCastSpell)
        //    _playerControlMode.ChangeState((int)PlayerStateID.SpellCast);
        //else if (_jumpInput && _player.CanJump())
        //    _playerControlMode.ChangeState((int)PlayerStateID.Jump);
        //else if (!_isGrounded)
        //{
        //    _playerControlMode.DecreaseAmountOfJumpsLeft();
        //    _playerControlMode.ChangeState((int)PlayerStateID.InAir);
        //}
        //else if (_isTouchingWall && _grabInput && _isTouchingLedge)
        //    _playerControlMode.ChangeState((int)PlayerStateID.WallGrab);
        //else if (_dashInput /*&& _player._dashState.CheckIfCanDash()*/ && !_isTouchingCeiling)
        //    _playerControlMode.ChangeState((int)PlayerStateID.Dash);
    }

    //protected override void DoChecks()
    //{
    //    base.DoChecks();

    //    //_isGrounded         = _player.IsGrounded();
    //    //_isTouchingWall     = _player.IsTouchingWall();
    //    //_isTouchingLedge    = _player.IsTouchingLedge(_player.transform.right);
    //    //_isTouchingCeiling  = _player.IsTouchingCeiling();
    //    //_canDefend          = _player.CanDefend();
    //    //_canCastSpell       = _player.CanCastSpell();
    //}
}