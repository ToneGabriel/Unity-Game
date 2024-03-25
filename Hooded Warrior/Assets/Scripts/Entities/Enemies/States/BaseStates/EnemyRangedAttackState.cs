using UnityEngine;

public abstract class EnemyRangedAttackState : EnemyState, ICooldown
{
    public bool IsOnCooldown { get; private set; }
    
    protected Data_RangedAttack _stateData;
    protected bool _isPlayerInMinAgroRange;

    public EnemyRangedAttackState(Enemy enemy, string animBoolName, Data_RangedAttack stateData) 
        : base(enemy, animBoolName)
    {
        _stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        _enemy.EntityIntStatusComponents.IsStateAnimationFinished = false;
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
        if (IsOnCooldown && Time.time >= _enemy.EntityIntStatusComponents.StateStartTime + _stateData.AttackCooldown)
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
        _enemy.EntityIntStatusComponents.IsStateAnimationFinished = true;
    }
}
