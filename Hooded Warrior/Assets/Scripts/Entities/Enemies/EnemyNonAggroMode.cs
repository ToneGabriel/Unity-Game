
public class EnemyNonAggroMode : FiniteStateMachine
{
    public EnemyNonAggroMode(Enemy enemy, string idle, string move, string playerDetected)
        : base()
    {
        AddNewState((int)EnemyStateID.Idle,             new EnemyIdleState(enemy, idle));
        AddNewState((int)EnemyStateID.Move,             new EnemyMoveState(enemy, move));
        AddNewState((int)EnemyStateID.PlayerDetected,   new EnemyPlayerDetectedState(enemy, playerDetected));

        _defaultStateID = (int)EnemyStateID.Move;
    }
}