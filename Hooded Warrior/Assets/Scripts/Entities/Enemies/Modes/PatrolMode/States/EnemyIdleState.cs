using UnityEngine;

public sealed class EnemyIdleState : EntityModeState<EnemyPatrolMode.StateID>
{
    private readonly EnemyPatrolMode        _enemyPatrolMode;
    private readonly EnemyPatrolModeData    _enemyPatrolModeData;

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

    public override void Update()                                      // Counts idle time
    {
        base.Update();

        if (Time.time >= _stateStartTime + _idleTime)
            _enemyPatrolMode.ChangeState(EnemyPatrolMode.StateID.Move);
        else if (_enemyPatrolMode.CheckPlayerInMinAgroRange())
            _enemyPatrolMode.ChangeState(EnemyPatrolMode.StateID.LookForPlayer);
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
