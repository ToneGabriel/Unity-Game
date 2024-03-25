
public class BringerOfDeathRangedAttackState : EnemyRangedAttackState
{
    private BringerOfDeath _bod;

    public BringerOfDeathRangedAttackState(BringerOfDeath bod, string animBoolName, Data_RangedAttack stateData)
        : base(bod, animBoolName, stateData)
    {
        _bod = bod;

        ObjectPoolManager.Instance.RequestPool<DeathPortal>();
        ObjectPoolManager.Instance.RequestPool<DeathOrb>();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_bod.EntityIntStatusComponents.IsStateAnimationFinished)
        {
            if (_isPlayerInMinAgroRange)
                _bod.ChangeState((int)BringerOfDeathStateID.PlayerDetected);
            else
                _bod.ChangeState((int)BringerOfDeathStateID.LookForPlayer);
        }
    }

    public void TriggerPortalRangedAttack()
    {
        base.TriggerRangedAttack();

        ObjectPoolManager.Instance.GetFromPool<DeathPortal>(_bod.PortalRangedAttackPosition.transform.position, _bod.PortalRangedAttackPosition.transform.rotation);
    }

    public void TriggerOrbRangedAttack()
    {
        base.TriggerRangedAttack();

        ObjectPoolManager.Instance.GetFromPool<DeathOrb>(_bod.OrbRangedAttackPosition.transform.position, _bod.OrbRangedAttackPosition.transform.rotation);
    }

    public void FinishPortalRangedAttack()
    {
        base.FinishRangedAttack();
    }

    public void FinishOrbRangedAttack()
    {
        base.FinishRangedAttack();
    }

}
