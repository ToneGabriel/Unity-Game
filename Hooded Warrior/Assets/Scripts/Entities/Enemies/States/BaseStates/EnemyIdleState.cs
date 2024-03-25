using UnityEngine;

public abstract class EnemyIdleState : EnemyState
{
    protected Data_Idle _stateData;
    protected bool _flipAfterIdle;
    protected bool _isIdleTimeOver;
    protected bool _isPlayerInMinAgroRange;
    protected float _idleTime;

    public EnemyIdleState(Enemy enemy, string animBoolName, Data_Idle stateData) 
        : base(enemy, animBoolName)
    {
        _stateData = stateData;
    }

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

        if (Time.time >= _enemy.EntityIntStatusComponents.StateStartTime + _idleTime)
            _isIdleTimeOver = true;
    }

    public override void DoChecks()                                         // Check ranges
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
        _idleTime = Random.Range(_stateData.MinIdleTime, _stateData.MaxIdleTime);
    }
}
