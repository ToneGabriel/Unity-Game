using UnityEngine;

public class ArcherMeleeAttackState : EnemyState
{
    public bool IsOnCooldown { get; private set; }

    private Archer _archer;
    private AttackDetails _attackDetails;
    private bool _isPlayerInMinAgroRange;

    public ArcherMeleeAttackState(Archer archer, string animBoolName)
        : base(archer, animBoolName)
    {
        _archer = archer;
    }

    public override void Enter()
    {
        base.Enter();

        _isStateAnimationFinished = false;
        _archer.SetVelocityZero();

        _attackDetails.DamageAmount = _archer.AggroData.MeleeAttackDamage;
        _attackDetails.Position = _archer.transform.position;
    }

    public override void Exit()
    {
        base.Exit();

        IsOnCooldown = true;
        //CooldownManager.Instance.Subscribe(this);
    }

    protected override void DoChecks()
    {
        base.DoChecks();

        _isPlayerInMinAgroRange = _archer.CheckPlayerInMinAgroRange();
    }

    public virtual void FinishMeleeAttack()
    {
        _isStateAnimationFinished = true;
    }

    public void CheckCooldown()
    {
        if (IsOnCooldown && Time.time >= _stateStartTime + _archer.AggroData.MeleeAttackCooldown)
            ResetCooldown();
    }

    public void ResetCooldown()
    {
        IsOnCooldown = false;
        //CooldownManager.Instance.UnSubscribe(this);
    }

    public void TriggerMeleeAttack()
    {
        Collider2D detectedObject = Physics2D.OverlapCircle(_archer.MeleeAttackPosition.transform.position,
                                                            _archer.AggroData.MeleeAttackRadius,
                                                            _archer.AggroData.WhatIsPlayer);
        if (detectedObject)
            detectedObject.gameObject.GetComponent<IDamageble>().Damage(_attackDetails);
    }
}
