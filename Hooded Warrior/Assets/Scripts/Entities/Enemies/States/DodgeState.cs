using UnityEngine;

public abstract class DodgeState : EnemyState, ICooldown
{
    public bool IsOnCooldown;

    protected Data_Dodge _stateData;
    protected bool _isPlayerInMeleeRange;
    protected bool _isPlayerInMaxAgroRange;
    protected bool _isGrounded;
    protected bool _isDodgeOver;

    public DodgeState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_Dodge stateData) 
        : base(enemy, stateMachine, animBoolName)
    {
        _stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        _isDodgeOver = false;
        _enemy.SetVelocity(_stateData.DodgeSpeed, _stateData.DodgeAngle, -_enemy.FacingDirection);
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

        if (Time.time >= StartTime + _stateData.DodgeTime && _isGrounded)
            _isDodgeOver = true;
    }

    public override void DoChecks()                                                     // Check Ranges
    {
        base.DoChecks();

        _isPlayerInMeleeRange = _enemy.CheckPlayerInMeleeRange();
        _isPlayerInMaxAgroRange = _enemy.CheckPlayerInMaxAgroRange();
        _isGrounded = _enemy.CheckIfGrounded();
    }

    public void CheckCooldown()
    {
        if (IsOnCooldown && Time.time >= StartTime + _stateData.DodgeCooldown)
        {
            IsOnCooldown = false;
            CooldownManager.Instance.UnSubscribe(this);
        }
    }
}
