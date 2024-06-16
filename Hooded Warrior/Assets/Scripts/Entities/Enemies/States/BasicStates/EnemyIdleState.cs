using UnityEngine;

public class EnemyIdleState : EnemyPatrolState
{
    protected bool _flipAfterIdle;
    protected bool _isIdleTimeOver;
    protected bool _isPlayerInMinAgroRange;
    protected float _idleTime;

    public EnemyIdleState(Enemy enemy, EnemyPatrolModeData data, string animBoolName) 
        : base(enemy, data, animBoolName) { }

    public override void Enter()
    {
        base.Enter();

        _enemy.SetVelocityZero();
        _isIdleTimeOver = false;
        SetRandomIdleTime();
    }

    public override void Exit()
    {
        base.Exit();

        if (_flipAfterIdle)
            _enemy.Flip();
    }

    public override void LogicUpdate()                                      // Counts idle time
    {
        base.LogicUpdate();

        if (Time.time >= _stateStartTime + _idleTime)
            _isIdleTimeOver = true;
    }

    protected override void DoChecks()                                         // Check ranges
    {
        base.DoChecks();

        _isPlayerInMinAgroRange = _enemy.CheckPlayerInMinAgroRange();
    }

    public void SetFlipAfterIdle(bool flip)
    {
        _flipAfterIdle = flip;
    }

    private void SetRandomIdleTime()
    {
        _idleTime = Random.Range(_data.MinIdleTime, _data.MaxIdleTime);
    }
}
