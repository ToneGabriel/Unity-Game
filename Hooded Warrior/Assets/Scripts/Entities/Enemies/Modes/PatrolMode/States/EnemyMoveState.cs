
public class EnemyMoveState : EntityModeState<EnemyPatrolMode.StateID>
{
    private readonly EnemyPatrolMode        _enemyPatrolMode;
    private readonly EnemyPatrolModeData    _enemyPatrolModeData;

    protected bool _isDetectingWall;
    protected bool _isDetectingLedge;
    protected bool _isPlayerInMinAgroRange;

    public EnemyMoveState(EnemyPatrolMode mode, EnemyPatrolModeData data, string animBoolName)
        : base(mode, animBoolName)
    {
        _enemyPatrolMode        = mode;
        _enemyPatrolModeData    = data;
    }

    public override void Enter()
    {
        base.Enter();

        _enemyPatrolMode.SetVelocity(_enemyPatrolModeData.MovementSpeed);                 // Set velocity
    }

    protected override void DoChecks()                                              // Check Ranges
    {
        base.DoChecks();

        _isDetectingWall = _enemyPatrolMode.IsTouchingWall();
        //_isDetectingLedge = _enemyPatrolMode.IsTouchingLedge(-_enemy.transform.up);
        _isPlayerInMinAgroRange = _enemyPatrolMode.CheckPlayerInMinAgroRange();
    }
}