
public class EnemyMoveState : EnemyPatrolState
{
    protected bool _isDetectingWall;
    protected bool _isDetectingLedge;
    protected bool _isPlayerInMinAgroRange;

    public EnemyMoveState(Enemy enemy, EnemyPatrolModeData data, string animBoolName)
        : base(enemy, data, animBoolName) {}

    public override void Enter()
    {
        base.Enter();

        _enemy.SetVelocity(_data.MovementSpeed);                 // Set velocity
    }

    protected override void DoChecks()                                              // Check Ranges
    {
        base.DoChecks();

        _isDetectingWall = _enemy.IsTouchingWall();
        _isDetectingLedge = _enemy.IsTouchingLedge(-_enemy.transform.up);
        _isPlayerInMinAgroRange = _enemy.CheckPlayerInMinAgroRange();
    }
}