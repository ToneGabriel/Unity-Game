using UnityEngine;

public class ArcherRangedAttackState : EntityModeState<ArcherAggroMode.StateID>
{
    public bool IsOnCooldown { get; private set; }

    private ArcherAggroMode     _archerAggroMode;
    private ArcherAggroModeData _archerAggroModeData;

    private bool _isPlayerInMinAgroRange;

    public ArcherRangedAttackState(ArcherAggroMode mode, ArcherAggroModeData data, string animBoolName)
        : base(mode, animBoolName)
    {
        _archerAggroMode        = mode;
        _archerAggroModeData    = data;
    }

    public override void Enter()
    {
        base.Enter();

        _isStateAnimationFinished = false;
        _archerAggroMode.Target_SetVelocityZero();
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

        _isPlayerInMinAgroRange = _archerAggroMode.CheckPlayerInMinAgroRange();
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
        //ObjectPoolManager.Instance.GetFromPool<Arrow>(  _archer.RangedAttackPosition.transform.position,
        //                                                _archer.RangedAttackPosition.transform.rotation);
    }
}
