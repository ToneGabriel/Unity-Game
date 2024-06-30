using UnityEngine;

public class ArcherDodgeState : EntityModeState<ArcherAggroMode.StateID>
{
    public bool IsOnCooldown { get; private set; }

    private ArcherAggroMode     _archerAggroMode;
    private ArcherAggroModeData _archerAggroModeData;

    private bool _isPlayerInMeleeRange;
    private bool _isPlayerInMaxAgroRange;
    private bool _isGrounded;
    private bool _isDodgeOver;

    public ArcherDodgeState(ArcherAggroMode mode, ArcherAggroModeData data, string animBoolName)
        : base(mode, animBoolName)
    {
        _archerAggroMode        = mode;
        _archerAggroModeData    = data;
    }

    public override void Enter()
    {
        base.Enter();

        //_isDodgeOver = false;
        //_archerAggroMode.SetVelocity(   _archerAggroModeData.DodgeSpeed,
        //                                _archerAggroModeData.DodgeAngle,
        //                                -_archerAggroMode.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();

        IsOnCooldown = true;
        //CooldownManager.Instance.Subscribe(this);
    }

    public override void Update()                                              // Counts dodge time
    {
        base.Update();

        if (Time.time >= _stateStartTime + _archerAggroModeData.DodgeTime && _isGrounded)
            _isDodgeOver = true;
    }

    protected override void DoChecks()                                                     // Check Ranges
    {
        base.DoChecks();

        _isPlayerInMeleeRange = _archerAggroMode.CheckPlayerInMeleeRange();
        _isPlayerInMaxAgroRange = _archerAggroMode.CheckPlayerInMaxAgroRange();
        _isGrounded = _archerAggroMode.IsGrounded();
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
