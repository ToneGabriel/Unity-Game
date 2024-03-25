
public abstract class EnemyDeadState : EnemyState
{
    protected Data_Dead _stateData;

    public EnemyDeadState(Enemy enemy, string animBoolName, Data_Dead stateData) 
        : base(enemy, animBoolName)
    {
        _stateData = stateData;
    }

    public virtual void FinishDeathAnimation()              // Called on death animation frame
    {
        _enemy.gameObject.SetActive(false);
    }

}
