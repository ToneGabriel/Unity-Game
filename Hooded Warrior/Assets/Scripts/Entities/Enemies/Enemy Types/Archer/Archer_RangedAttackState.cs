
public class Archer_RangedAttackState : RangedAttackState
{
    private Archer _archer;

    public Archer_RangedAttackState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_RangedAttack stateData) 
        : base(enemy, stateMachine, animBoolName, stateData)
    {
        _archer = (Archer)enemy;

        ObjectPoolManager.Instance.RequestPool<Arrow>();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(_isAnimationFinished)
        {
            if (_isPlayerInMinAgroRange)
                _stateMachine.ChangeState(_archer.PlayerDetectedState);
            else
                _stateMachine.ChangeState(_archer.LookForPlayerState);
        }
    }

    public override void TriggerRangedAttack()
    {
        base.TriggerRangedAttack();

        ObjectPoolManager.Instance.GetFromPool<Arrow>(_archer.RangedAttackPosition.transform.position, _archer.RangedAttackPosition.transform.rotation);
    }
}
