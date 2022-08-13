
public class BringerOfDeath_RangedAttackState : RangedAttackState
{
    private BringerOfDeath _bod;

    public BringerOfDeath_RangedAttackState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_RangedAttack stateData)
        : base(enemy, stateMachine, animBoolName, stateData)
    {
        _bod = (BringerOfDeath)enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isAnimationFinished)
        {
            if (_isPlayerInMinAgroRange)
                _stateMachine.ChangeState(_bod.PlayerDetectedState);
            else
                _stateMachine.ChangeState(_bod.LookForPlayerState);
        }
    }

    public void TriggerPortalRangedAttack()
    {
        base.TriggerRangedAttack();

        ObjectPoolManager.Instance.GetFromPool(typeof(DeathPortal), _bod.PortalRangedAttackPosition.transform.position, _bod.PortalRangedAttackPosition.transform.rotation);
    }

    public void TriggerOrbRangedAttack()
    {
        base.TriggerRangedAttack();

        ObjectPoolManager.Instance.GetFromPool(typeof(DeathOrb), _bod.OrbRangedAttackPosition.transform.position, _bod.OrbRangedAttackPosition.transform.rotation);
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
