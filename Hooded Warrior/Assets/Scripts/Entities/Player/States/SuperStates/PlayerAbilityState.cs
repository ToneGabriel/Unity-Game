
public abstract class PlayerAbilityState : PlayerState
{
    protected bool _isAbilityDone;
    protected bool _isTouchingCeiling;
    protected bool _isGrounded;

    public PlayerAbilityState(Player player, string animBoolName)
        : base(player, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
        _isAbilityDone = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isAbilityDone)
        {
            if (_isTouchingCeiling)
                _player.ChangeState(_player._crouchIdleState);
            else if (_isGrounded && _player._rigidbody.velocity.y < 0.01f)
                _player.ChangeState(_player._idleState);
            else
                _player.ChangeState(_player._inAirState);
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isGrounded = _player.CheckIfGrounded();
        _isTouchingCeiling = _player.CheckIfTouchingCeiling();
    }
}