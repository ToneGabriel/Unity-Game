using UnityEngine;

public abstract class LookForPlayerState : EnemyState
{
    protected Data_LookForPlayer _stateData;
    protected bool _turnImmediately;
    protected bool _isPLayerInMinAgroRange;
    protected bool _isAllTurnsDone;
    protected bool _isAllTurnsTimeDone;
    protected float _lastTurnTime;
    protected int _amountOfTurnsDone;

    public LookForPlayerState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_LookForPlayer stateData) 
        : base(enemy, stateMachine, animBoolName)
    {
        _stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        _isAllTurnsDone = false;
        _isAllTurnsTimeDone = false;
        _lastTurnTime = StartTime;
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

    public override void DoChecks()
    {
        base.DoChecks();

        _isPLayerInMinAgroRange = _enemy.CheckPlayerInMinAgroRange();
    }

    public void SetTurnImmediately(bool flip)
    {
        _turnImmediately = flip;
    }
}
