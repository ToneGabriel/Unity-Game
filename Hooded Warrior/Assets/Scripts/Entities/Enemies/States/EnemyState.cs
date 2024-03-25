
public abstract class EnemyState : EntityState
{
    protected Enemy _enemy;

    public EnemyState(Enemy enemy, string animBoolName)
        : base(enemy, animBoolName)
    {
        _enemy = enemy;
    }
}
