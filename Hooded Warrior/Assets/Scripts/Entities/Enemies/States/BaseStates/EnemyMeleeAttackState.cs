using UnityEngine;

public abstract class EnemyMeleeAttackState : EnemyState, ICooldown
{
    public bool IsOnCooldown { get; private set; }

    protected Data_MeleeAttack _stateData;
    protected AttackDetails _attackDetails;
    protected bool _isPlayerInMinAgroRange;

    public EnemyMeleeAttackState(Enemy enemy, string animBoolName, Data_MeleeAttack stateData) 
        : base(enemy, animBoolName)
    {
        _stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        _enemy.EntityIntStatusComponents.IsStateAnimationFinished = false;
        _enemy.SetVelocityZero();

        _attackDetails.DamageAmount = _stateData.AttackDamage;
        _attackDetails.Position = _enemy.transform.position;
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

    public virtual void TriggerMeleeAttack() { }

    public virtual void FinishMeleeAttack()
    {
        _enemy.EntityIntStatusComponents.IsStateAnimationFinished = true;
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
}
