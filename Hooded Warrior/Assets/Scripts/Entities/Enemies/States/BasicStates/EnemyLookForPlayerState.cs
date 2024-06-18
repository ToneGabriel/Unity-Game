using UnityEngine;

public sealed class EnemyLookForPlayerState : EntityModeState
{
    private EnemyPatrolMode     _enemyPatrolMode;
    private EnemyPatrolModeData _enemyPatrolModeData;

    private bool _turnImmediately;
    private bool _isPLayerInMinAgroRange;
    private bool _isAllTurnsDone;
    private bool _isAllTurnsTimeDone;
    private float _lastTurnTime;
    private int _amountOfTurnsDone;

    public EnemyLookForPlayerState(EnemyPatrolMode mode, EnemyPatrolModeData data, string animBoolName)
        : base(mode, animBoolName)
    {
        _enemyPatrolMode        = mode;
        _enemyPatrolModeData    = data;
    }

    public override void Enter()
    {
        base.Enter();

        _isAllTurnsDone = false;
        _isAllTurnsTimeDone = false;
        _lastTurnTime = _stateStartTime;
        _amountOfTurnsDone = 0;
        _enemyPatrolMode.SetVelocityZero();
    }

    public override void LogicUpdate()                                          // Counts turns and time between turns
    {
        base.LogicUpdate();

        if (_turnImmediately)
        {
            _enemyPatrolMode.Flip();
            _lastTurnTime = Time.time;
            _amountOfTurnsDone++;
            _turnImmediately = false;
        }
        else if (Time.time >= _lastTurnTime + _enemyPatrolModeData.TimeBetweenTurns && !_isAllTurnsDone)
        {
            _enemyPatrolMode.Flip();
            _lastTurnTime = Time.time;
            _amountOfTurnsDone++;
        }

        if (_amountOfTurnsDone >= _enemyPatrolModeData.AmountOfTurns)
            _isAllTurnsDone = true;

        if (Time.time >= _lastTurnTime + _enemyPatrolModeData.TimeBetweenTurns && _isAllTurnsDone)
            _isAllTurnsTimeDone = true;
    }

    protected override void DoChecks()
    {
        base.DoChecks();

        _isPLayerInMinAgroRange = _enemyPatrolMode.CheckPlayerInMinAgroRange();
    }

    public void SetTurnImmediately(bool flip)
    {
        _turnImmediately = flip;
    }
}
