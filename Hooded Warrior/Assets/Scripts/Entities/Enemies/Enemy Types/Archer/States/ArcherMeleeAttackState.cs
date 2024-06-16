using UnityEngine;

public class ArcherMeleeAttackState : ArcherAggroState
{
    public bool IsOnCooldown { get; private set; }

    private AttackDetails _attackDetails;
    private bool _isPlayerInMinAgroRange;

    public ArcherMeleeAttackState(Archer archer, ArcherAggroModeData data, string animBoolName)
        : base(archer, data, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        _isStateAnimationFinished = false;
        _archer.SetVelocityZero();

        _attackDetails.DamageAmount = _archerAggroModeData.MeleeAttackDamage;
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
        if (IsOnCooldown && Time.time >= _stateStartTime + _archerAggroModeData.MeleeAttackCooldown)
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
                                                            _archerAggroModeData.MeleeAttackRadius,
                                                            _archerAggroModeData.WhatIsPlayer);
        if (detectedObject)
            detectedObject.gameObject.GetComponent<IDamageble>().Damage(_attackDetails);
    }
}
