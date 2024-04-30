using UnityEngine;

public class ArcherRangedAttackState : EnemyState
{
    public bool IsOnCooldown { get; private set; }

    private Archer _archer;
    private ArcherStateData _stateData;
    private bool _isPlayerInMinAgroRange;

    public ArcherRangedAttackState(Archer archer, string animBoolName, ArcherStateData stateData)
        : base(archer, animBoolName)
    {
        _archer     = archer;
        _stateData  = stateData;
    }

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
        if (IsOnCooldown && Time.time >= _stateStartTime + _stateData.RangedAttackCooldown)
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
