using UnityEngine;

public sealed class EnemyIdleState : EntityModeState<EnemyPatrolMode.StateID>
{
    private readonly EnemyPatrolMode        _enemyPatrolMode;
    private readonly EnemyPatrolModeData    _enemyPatrolModeData;

    private bool    _flipAfterIdle;
    private float   _idleTime;

    public EnemyIdleState(EnemyPatrolMode mode, EnemyPatrolModeData data, string animBoolName) 
        : base(mode, animBoolName)
    {
        _enemyPatrolMode        = mode;
        _enemyPatrolModeData    = data;
    }

    public override void Enter()
    {
        base.Enter();

        _enemyPatrolMode.Target_SetVelocityZero();
        SetRandomIdleTime();
    }

    public override void Update()                                      // Counts idle time
    {
        base.Update();

        if (_enemyPatrolMode.Target_CheckPlayerInMinAgroRange())
            _enemyPatrolMode.ChangeState(EnemyPatrolMode.StateID.LookForPlayer);
        else if (Time.time >= _stateStartTime + _idleTime)
            _enemyPatrolMode.ChangeState(EnemyPatrolMode.StateID.Move);
        else
        {
            // wait in idle
        }
    }

    public override void Exit()
    {
        base.Exit();

        if (_flipAfterIdle)
            _enemyPatrolMode.Target_Flip();
    }

    protected override void DoChecks()                                         // Check ranges
    {
        base.DoChecks();
    }

    private void SetRandomIdleTime()
    {
        _idleTime = Random.Range(_enemyPatrolModeData.MinIdleTime, _enemyPatrolModeData.MaxIdleTime);
    }
}
