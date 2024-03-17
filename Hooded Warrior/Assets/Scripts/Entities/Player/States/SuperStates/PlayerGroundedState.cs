
public sealed partial class Player
{
    private abstract partial class PlayerGroundedState
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

            _player._jumpState.ResetAmountOfJumpsLeft();
            _player._dashState.ResetCanDash();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            _inputX = _player._inputHandler.NormalizedInputX;
            _inputY = _player._inputHandler.NormalizedInputY;
            _jumpInput = _player._inputHandler.JumpInput;
            _grabInput = _player._inputHandler.GrabInput;
            _dashInput = _player._inputHandler.DashInput;
            _rollInput = _player._inputHandler.RollInput;
            _primaryAttackInput = _player._inputHandler.PrimaryAttackInput;
            _secondaryDefendInput = _player._inputHandler.SecondaryDefendInput;
            _spellCastInput = _player._inputHandler.SpellCastInput;

            _player.CheckIfShouldFlip(_inputX);

            if (_primaryAttackInput && !_isTouchingCeiling)
                _stateMachine.ChangeState(_player._primaryAttackState);
            else if (_secondaryDefendInput && !_isTouchingCeiling && _canDefend)
                _stateMachine.ChangeState(_player._secondaryDefendState);
            else if (_spellCastInput && !_isTouchingCeiling && _canCastSpell)
                _stateMachine.ChangeState(_player._spellCastState);
            else if (_jumpInput && _player._jumpState.CanJump())
            {
                _player._inputHandler.UseJumpInput();
                _stateMachine.ChangeState(_player._jumpState);
            }
            else if (!_isGrounded)
            {
                _player._jumpState.DecreaseAmountOfJumpsLeft();
                _stateMachine.ChangeState(_player._inAirState);
            }
            else if (_isTouchingWall && _grabInput && _isTouchingLedge)
                _stateMachine.ChangeState(_player._wallGrabState);
            else if (_dashInput && _player._dashState.CheckIfCanDash() && !_isTouchingCeiling)
                _stateMachine.ChangeState(_player._dashState);
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
}
