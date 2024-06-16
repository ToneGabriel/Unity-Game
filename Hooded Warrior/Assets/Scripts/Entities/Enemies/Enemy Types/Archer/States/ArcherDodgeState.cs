using UnityEngine;

public class ArcherDodgeState : ArcherAggroState
{
    public bool IsOnCooldown { get; private set; }

    private bool _isPlayerInMeleeRange;
    private bool _isPlayerInMaxAgroRange;
    private bool _isGrounded;
    private bool _isDodgeOver;

    public ArcherDodgeState(Archer archer, ArcherAggroModeData data, string animBoolName)
        : base(archer, data, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        _isDodgeOver = false;
        _archer.SetVelocity(_archerAggroModeData.DodgeSpeed,
                            _archerAggroModeData.DodgeAngle,
                            -_archer.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();

        IsOnCooldown = true;
        //CooldownManager.Instance.Subscribe(this);
    }

    public override void LogicUpdate()                                              // Counts dodge time
    {
        base.LogicUpdate();

        if (Time.time >= _stateStartTime + _archerAggroModeData.DodgeTime && _isGrounded)
            _isDodgeOver = true;
    }

    protected override void DoChecks()                                                     // Check Ranges
    {
        base.DoChecks();

        _isPlayerInMeleeRange = _archer.CheckPlayerInMeleeRange();
        _isPlayerInMaxAgroRange = _archer.CheckPlayerInMaxAgroRange();
        _isGrounded = _archer.IsGrounded();
    }

    public void CheckCooldown()
    {
        if (IsOnCooldown && Time.time >= _stateStartTime + _archerAggroModeData.DodgeCooldown)
            ResetCooldown();
    }

    public void ResetCooldown()
    {
        IsOnCooldown = false;
        //CooldownManager.Instance.UnSubscribe(this);
    }
}
