
public class ArcherRangedAttackState : EnemyRangedAttackState
{
    private Archer _archer;

    public ArcherRangedAttackState(Archer archer, string animBoolName, Data_RangedAttack stateData) 
        : base(archer, animBoolName, stateData)
    {
        _archer = archer;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(_isStateAnimationFinished)
        {
            if (_isPlayerInMinAgroRange)
                _archer.ChangeState((int)ArcherStateID.PlayerDetected);
            else
                _archer.ChangeState((int)ArcherStateID.LookForPlayer);
        }
    }

    public override void TriggerRangedAttack()
    {
        base.TriggerRangedAttack();

        ObjectPoolManager.Instance.GetFromPool<Arrow>(  _archer.RangedAttackPosition.transform.position,
                                                        _archer.RangedAttackPosition.transform.rotation);
    }
}
