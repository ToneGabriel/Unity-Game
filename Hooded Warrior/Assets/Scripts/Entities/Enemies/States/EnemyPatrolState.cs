
public abstract class EnemyPatrolState : EntityState
{
    protected Enemy                 _enemy;
    protected EnemyPatrolModeData   _data;

    public EnemyPatrolState(Enemy enemy, EnemyPatrolModeData data, string animBoolName)
        : base(enemy, animBoolName)
    {
        _enemy  = enemy;
        _data   = data;
    }
}
