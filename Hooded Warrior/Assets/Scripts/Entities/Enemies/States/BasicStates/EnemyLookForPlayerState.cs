using UnityEngine;

public class EnemyLookForPlayerState : EnemyState
{
    protected EnemyPatrolModeData _stateData;
    protected bool _turnImmediately;
    protected bool _isPLayerInMinAgroRange;
    protected bool _isAllTurnsDone;
    protected bool _isAllTurnsTimeDone;
    protected float _lastTurnTime;
    protected int _amountOfTurnsDone;

    public EnemyLookForPlayerState(Enemy enemy, string animBoolName, EnemyPatrolModeData stateData) 
        : base(enemy, animBoolName)
    {
        _stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        _isAllTurnsDone = false;
        _isAllTurnsTimeDone = false;
        _lastTurnTime = _stateStartTime;
        _amountOfTurnsDone = 0;
        _enemy.SetVelocityZero();
    }

    public override void LogicUpdate()                                          // Counts turns and time between turns
    {
        base.LogicUpdate();

        if (_turnImmediately)
        {
            _enemy.Flip();
            _lastTurnTime = Time.time;
            _amountOfTurnsDone++;
            _turnImmediately = false;
        }
        else if (Time.time >= _lastTurnTime + _stateData.TimeBetweenTurns && !_isAllTurnsDone)
        {
            _enemy.Flip();
            _lastTurnTime = Time.time;
            _amountOfTurnsDone++;
        }

        if (_amountOfTurnsDone >= _stateData.AmountOfTurns)
            _isAllTurnsDone = true;

        if (Time.time >= _lastTurnTime + _stateData.TimeBetweenTurns && _isAllTurnsDone)
            _isAllTurnsTimeDone = true;
    }

    protected override void DoChecks()
    {
        base.DoChecks();

        _isPLayerInMinAgroRange = _enemy.CheckPlayerInMinAgroRange();
    }

    public void SetTurnImmediately(bool flip)
    {
        _turnImmediately = flip;
    }
}
