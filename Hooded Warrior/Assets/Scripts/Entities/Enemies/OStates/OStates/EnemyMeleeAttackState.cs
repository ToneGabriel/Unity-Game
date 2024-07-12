using UnityEngine;

public abstract class EnemyMeleeAttackState
{
    //public bool IsOnCooldown { get; private set; }

    //protected AttackDetails _attackDetails;
    //protected bool _isPlayerInMinAgroRange;

    //public EnemyMeleeAttackState(Enemy enemy, string animBoolName) 
    //    : base(enemy, animBoolName)
    //{
    //}

    //public override void Enter()
    //{
    //    base.Enter();

    //    _isStateAnimationFinished = false;
    //    _enemy.SetVelocityZero();

    //    //_attackDetails.DamageAmount = _stateData.AttackDamage;
    //    _attackDetails.Position = _enemy.transform.position;
    //}

    //public override void Exit()
    //{
    //    base.Exit();

    //    IsOnCooldown = true;
    //    //CooldownManager.Instance.Subscribe(this);
    //}

    //protected override void DoChecks()
    //{
    //    base.DoChecks();

    //    _isPlayerInMinAgroRange = _enemy.CheckPlayerInMinAgroRange();
    //}

    //public virtual void TriggerMeleeAttack() { }

    //public virtual void FinishMeleeAttack()
    //{
    //    _isStateAnimationFinished = true;
    //}

    //public void CheckCooldown()
    //{
    //    //if (IsOnCooldown && Time.time >= _stateStartTime + _stateData.AttackCooldown)
    //    //    ResetCooldown();
    //}

    //public void ResetCooldown()
    //{
    //    IsOnCooldown = false;
    //    //CooldownManager.Instance.UnSubscribe(this);
    //}
}
