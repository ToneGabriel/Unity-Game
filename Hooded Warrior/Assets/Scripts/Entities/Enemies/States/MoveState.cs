
public abstract class MoveState : EnemyState
{
    protected Data_Move _stateData;
    protected bool _isDetectingWall;
    protected bool _isDetectingLedge;
    protected bool _isPlayerInMinAgroRange;

    public MoveState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_Move stateData)
        : base(enemy, stateMachine, animBoolName)
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

        _isDetectingWall = _enemy.CheckIfTouchingWall();
        _isDetectingLedge = _enemy.CheckIfTouchingLedge(-_enemy.transform.up);
        _isPlayerInMinAgroRange = _enemy.CheckPlayerInMinAgroRange();
    }
}