using UnityEngine;

public abstract class EnemyDodgeState : EnemyState, ICooldown
{
    public bool IsOnCooldown { get; private set; }

    protected Data_Dodge _stateData;
    protected bool _isPlayerInMeleeRange;
    protected bool _isPlayerInMaxAgroRange;
    protected bool _isGrounded;
    protected bool _isDodgeOver;

    public EnemyDodgeState(Enemy enemy, string animBoolName, Data_Dodge stateData) 
        : base(enemy, animBoolName)
    {
        _stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        _isDodgeOver = false;
        _enemy.SetVelocity(_stateData.DodgeSpeed, _stateData.DodgeAngle, -_enemy.EntityIntStatusComponents.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();

        IsOnCooldown = true;
        CooldownManager.Instance.Subscribe(this);
    }

    public override void LogicUpdate()                                              // Counts dodge time
    {
        base.LogicUpdate();

        if (Time.time >= _enemy.EntityIntStatusComponents.StateStartTime + _stateData.DodgeTime && _isGrounded)
            _isDodgeOver = true;
    }

    public override void DoChecks()                                                     // Check Ranges
    {
        base.DoChecks();

        _isPlayerInMeleeRange = _enemy.CheckPlayerInMeleeRange();
        _isPlayerInMaxAgroRange = _enemy.CheckPlayerInMaxAgroRange();
        _isGrounded = _enemy.IsGrounded();
    }

    public void CheckCooldown()
    {
        if (IsOnCooldown && Time.time >= _enemy.EntityIntStatusComponents.StateStartTime + _stateData.DodgeCooldown)
            ResetCooldown();
    }

    public void ResetCooldown()
    {
        IsOnCooldown = false;
        CooldownManager.Instance.UnSubscribe(this);
    }
}
