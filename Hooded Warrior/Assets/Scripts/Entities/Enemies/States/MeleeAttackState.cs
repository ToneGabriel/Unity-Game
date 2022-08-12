
public abstract class MeleeAttackState : EnemyState
{
    public bool IsOnCooldown;                // boolean for cooldown ready (cooldown counts in enemy specific update)
    
    protected Data_MeleeAttack _stateData;
    protected AttackDetails _attackDetails;
    protected bool _isPlayerInMinAgroRange;

    public MeleeAttackState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_MeleeAttack stateData) 
        : base(enemy, stateMachine, animBoolName)
    {
        _stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        _isAnimationFinished = false;
        _enemy.SetVelocityZero();

        _attackDetails.DamageAmount = _stateData.AttackDamage;
        _attackDetails.Position = _enemy.transform.position;
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

    public virtual void TriggerMeleeAttack() { }

    public virtual void FinishMeleeAttack()
    {
        _isAnimationFinished = true;
    }

}
