
public abstract class EnemyMoveState : EnemyState
{
    protected Data_Move _stateData;
    protected bool _isDetectingWall;
    protected bool _isDetectingLedge;
    protected bool _isPlayerInMinAgroRange;

    public EnemyMoveState(Enemy enemy, string animBoolName, Data_Move stateData)
        : base(enemy, animBoolName)
    {
        _stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        _enemy.SetVelocity(_stateData.MovementSpeed);                             // Set velocity
    }

    public override void DoChecks()                                              // Check Ranges
    {
        base.DoChecks();

        _isDetectingWall = _enemy.IsTouchingWall();
        _isDetectingLedge = _enemy.IsTouchingLedge(-_enemy.transform.up);
        _isPlayerInMinAgroRange = _enemy.CheckPlayerInMinAgroRange();
    }
}