
public abstract class PlayerAbilityState : PlayerState
{
    protected bool _isAbilityDone;
    protected bool _isTouchingCeiling;
    protected bool _isGrounded;

    public PlayerAbilityState(PlayerControlMode mode, PlayerControlModeData data, string animBoolName)
        : base(mode, data, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        _isAbilityDone      = false;
        _isTouchingCeiling  = false;
        _isGrounded         = false;
    }

    public override void Update()
    {
        base.Update();

        if (_isAbilityDone)
        {
            if (_isTouchingCeiling)
                _playerControlMode.ChangeState(PlayerControlMode.StateID.CrouchIdle);
            else if (_isGrounded)
                _playerControlMode.ChangeState(PlayerControlMode.StateID.Idle);
            else
                _playerControlMode.ChangeState(PlayerControlMode.StateID.InAir);
        }
        else
        {
            // wait
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        //_isGrounded = _player.IsGrounded();
        //_isTouchingCeiling = _player.IsTouchingCeiling();
    }

    public override void Exit()
    {
        base.Exit();
    }
}