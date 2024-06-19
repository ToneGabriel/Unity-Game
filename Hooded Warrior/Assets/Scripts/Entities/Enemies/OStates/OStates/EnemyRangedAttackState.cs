using UnityEngine;

public abstract class EnemyRangedAttackState
{
    //public bool IsOnCooldown { get; private set; }
    
    //protected bool _isPlayerInMinAgroRange;

    //public EnemyRangedAttackState(Enemy enemy, string animBoolName)
    //    : base(enemy, animBoolName) { }

    //public override void Enter()
    //{
    //    base.Enter();

    //    _isStateAnimationFinished = false;
    //    _enemy.SetVelocityZero();
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

    //public virtual void TriggerRangedAttack() { }

    //public virtual void FinishRangedAttack()
    //{
    //    _isStateAnimationFinished = true;
    //}
}
