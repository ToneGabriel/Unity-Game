using UnityEngine;

public sealed class EnemyIdleState : EntityModeState
{
    private EnemyPatrolMode     _enemyPatrolMode;
    private EnemyPatrolModeData _enemyPatrolModeData;

    private bool _flipAfterIdle;
    private bool _isIdleTimeOver;
    private bool _isPlayerInMinAgroRange;
    private float _idleTime;

    public EnemyIdleState(EnemyPatrolMode mode, EnemyPatrolModeData data, string animBoolName) 
        : base(mode, animBoolName)
    {
        _enemyPatrolMode        = mode;
        _enemyPatrolModeData    = data;
    }

    public override void Enter()
    {
        base.Enter();

        _enemyPatrolMode.SetVelocityZero();
        _isIdleTimeOver = false;
        SetRandomIdleTime();
    }

    public override void Exit()
    {
        base.Exit();

        if (_flipAfterIdle)
            _enemyPatrolMode.Flip();
    }

    public override void LogicUpdate()                                      // Counts idle time
    {
        base.LogicUpdate();

        if (Time.time >= _stateStartTime + _idleTime)
            _enemyPatrolMode.ChangeState((int)EnemyPatrolMode.StateID.Move);
        else if (_enemyPatrolMode.CheckPlayerInMinAgroRange())
            _enemyPatrolMode.ChangeState((int)EnemyPatrolMode.StateID.PlayerDetected);
    }

    protected override void DoChecks()                                         // Check ranges
    {
        base.DoChecks();

        _isPlayerInMinAgroRange = _enemyPatrolMode.CheckPlayerInMinAgroRange();
    }

    public void SetFlipAfterIdle(bool flip)
    {
        _flipAfterIdle = flip;
    }

    private void SetRandomIdleTime()
    {
        _idleTime = Random.Range(_enemyPatrolModeData.MinIdleTime, _enemyPatrolModeData.MaxIdleTime);
    }
}
