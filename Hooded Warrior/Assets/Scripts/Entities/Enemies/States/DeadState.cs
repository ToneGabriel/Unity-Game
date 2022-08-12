
public abstract class DeadState : EnemyState
{
    protected Data_Dead _stateData;

    public DeadState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_Dead stateData) 
        : base(enemy, stateMachine, animBoolName)
    {
        _stateData = stateData;
    }

    public virtual void FinishDeathAnimation()              // Called on death animation frame
    {
        _enemy.gameObject.SetActive(false);
    }

}
