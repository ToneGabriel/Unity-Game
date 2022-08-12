
public abstract class RangedAttackState : EnemyState
{
    public bool IsOnCooldown;                             // boolean for cooldown ready (cooldown counts in enemy specific update)
    
    protected Data_RangedAttack _stateData;
    protected bool _isPlayerInMinAgroRange;

    public RangedAttackState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_RangedAttack stateData) 
        : base(enemy, stateMachine, animBoolName)
    {
        _stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        _isAnimationFinished = false;
        _enemy.SetVelocityZero();
    }

    public override void Exit()
    {
        base.Exit();

        IsOnCooldown = true;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isPlayerInMinAgroRange = _enemy.CheckPlayerInMinAgroRange();
    }

    public virtual void TriggerRangedAttack() { }

    public virtual void FinishRangedAttack()
    {
        _isAnimationFinished = true;
    }

}
