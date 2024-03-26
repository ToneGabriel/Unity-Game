
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
                _player.ChangeState((int)PlayerStateID.CrouchIdle);
            else if (_isGrounded && _player.RBVelocity.y < 0.01f)
                _player.ChangeState((int)PlayerStateID.Idle);
            else
                _player.ChangeState((int)PlayerStateID.InAir);
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isGrounded = _player.CheckIfGrounded();
        _isTouchingCeiling = _player.CheckIfTouchingCeiling();
    }
}