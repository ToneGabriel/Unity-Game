
public class EnemyMoveState : EnemyState
{
    protected bool _isDetectingWall;
    protected bool _isDetectingLedge;
    protected bool _isPlayerInMinAgroRange;

    public EnemyMoveState(Enemy enemy, string animBoolName)
        : base(enemy, animBoolName) {}

    public override void Enter()
    {
        base.Enter();

        _enemy.SetVelocity(_enemy.PatrolData.MovementSpeed);                 // Set velocity
    }

    protected override void DoChecks()                                              // Check Ranges
    {
        base.DoChecks();

        _isDetectingWall = _enemy.IsTouchingWall();
        _isDetectingLedge = _enemy.IsTouchingLedge(-_enemy.transform.up);
        _isPlayerInMinAgroRange = _enemy.CheckPlayerInMinAgroRange();
    }
}