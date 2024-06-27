
public class BringerOfDeathRangedAttackState : EntityModeState<BringerOfDeathAggroMode.StateID>
{
    private readonly BringerOfDeathAggroMode        _bringerOfDeathAggroMode;
    private readonly BringerOfDeathAggroModeData    _bringerOfDeathAggroModeData;

    public BringerOfDeathRangedAttackState(BringerOfDeathAggroMode mode, BringerOfDeathAggroModeData data, string animBoolName)
        : base(mode, animBoolName)
    {
        _bringerOfDeathAggroMode        = mode;
        _bringerOfDeathAggroModeData    = data;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //if (_isStateAnimationFinished)
        //{
        //    if (_isPlayerInMinAgroRange)
        //        _bod.ChangeState((int)BringerOfDeathStateID.PlayerDetected);
        //    else
        //        _bod.ChangeState((int)BringerOfDeathStateID.LookForPlayer);
        //}
    }

    //public void TriggerPortalRangedAttack()
    //{
    //    base.TriggerRangedAttack();

    //    ObjectPoolManager.Instance.GetFromPool<DeathPortal>(_bod.PortalRangedAttackPosition.transform.position, _bod.PortalRangedAttackPosition.transform.rotation);
    //}

    //public void TriggerOrbRangedAttack()
    //{
    //    base.TriggerRangedAttack();

    //    ObjectPoolManager.Instance.GetFromPool<DeathOrb>(_bod.OrbRangedAttackPosition.transform.position, _bod.OrbRangedAttackPosition.transform.rotation);
    //}

    //public void FinishPortalRangedAttack()
    //{
    //    base.FinishRangedAttack();
    //}

    //public void FinishOrbRangedAttack()
    //{
    //    base.FinishRangedAttack();
    //}

}
