
public sealed class EnemyMoveState : EntityModeState<EnemyPatrolMode.StateID>
{
    private readonly EnemyPatrolMode        _enemyPatrolMode;
    private readonly EnemyPatrolModeData    _enemyPatrolModeData;

    public EnemyMoveState(EnemyPatrolMode mode, EnemyPatrolModeData data, string animBoolName)
        : base(mode, animBoolName)
    {
        _enemyPatrolMode        = mode;
        _enemyPatrolModeData    = data;
    }

    public override void Enter()
    {
        base.Enter();

        _enemyPatrolMode.Target_SetVelocityX(_enemyPatrolModeData.MovementSpeed * _enemyPatrolMode.Target_FacingDirection);
    }

    public override void Update()
    {
        base.Update();

        if (_enemyPatrolMode.Target_CheckPlayerInMinAgroRange())
            _enemyPatrolMode.ChangeState(EnemyPatrolMode.StateID.LookForPlayer);
        else if (_enemyPatrolMode.Target_IsTouchingWall() /*|| _enemyPatrolMode.Target_IsTouchingLedge()*/)
            _enemyPatrolMode.ChangeState(EnemyPatrolMode.StateID.Idle);
        else
        {
            // keep moving
        }
    }

    public override void Exit()
    {
        base.Exit();

        _enemyPatrolMode.Target_SetVelocityZero();
    }
}