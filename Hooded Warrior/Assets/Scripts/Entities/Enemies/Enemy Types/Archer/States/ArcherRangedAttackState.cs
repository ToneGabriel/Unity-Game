using UnityEngine;

public class ArcherRangedAttackState : ArcherAggroState
{
    public bool IsOnCooldown { get; private set; }

    private bool _isPlayerInMinAgroRange;

    public ArcherRangedAttackState(Archer archer, ArcherAggroModeData data, string animBoolName)
        : base(archer, data, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        _isStateAnimationFinished = false;
        _archer.SetVelocityZero();
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

    public void CheckCooldown()
    {
        if (IsOnCooldown && Time.time >= _stateStartTime + _archerAggroModeData.RangedAttackCooldown)
            ResetCooldown();
    }

    public void ResetCooldown()
    {
        IsOnCooldown = false;
        //CooldownManager.Instance.UnSubscribe(this);
    }

    public void FinishRangedAttack()
    {
        _isStateAnimationFinished = true;
    }

    public void TriggerRangedAttack()
    {
        ObjectPoolManager.Instance.GetFromPool<Arrow>(  _archer.RangedAttackPosition.transform.position,
                                                        _archer.RangedAttackPosition.transform.rotation);
    }
}
