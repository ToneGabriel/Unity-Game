
using UnityEngine;

public abstract class RangedAttackState : EnemyState, ICooldown
{
    public bool IsOnCooldown { get; private set; }
    
    protected Data_RangedAttack _stateData;
    protected bool _isPlayerInMinAgroRange;

    public RangedAttackState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_RangedAttack stateData) 
        : base(enemy, stateMachine, animBoolName)
    {
        _stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        _isAnimationFinished = false;
        _enemy.SetVelocityZero();
    }

    public override void Exit()
    {
        base.Exit();

        IsOnCooldown = true;
        CooldownManager.Instance.Subscribe(this);
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isPlayerInMinAgroRange = _enemy.CheckPlayerInMinAgroRange();
    }

    public void CheckCooldown()
    {
        if (IsOnCooldown && Time.time >= StartTime + _stateData.AttackCooldown)
            ResetCooldown();
    }

    public void ResetCooldown()
    {
        IsOnCooldown = false;
        CooldownManager.Instance.UnSubscribe(this);
    }

    public virtual void TriggerRangedAttack() { }

    public virtual void FinishRangedAttack()
    {
        _isAnimationFinished = true;
    }
}
