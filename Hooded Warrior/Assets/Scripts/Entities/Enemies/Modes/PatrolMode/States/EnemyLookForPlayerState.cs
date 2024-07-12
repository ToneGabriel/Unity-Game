using UnityEngine;

public sealed class EnemyLookForPlayerState : EntityModeState<EnemyPatrolMode.StateID>
{
    private readonly EnemyPatrolMode        _enemyPatrolMode;
    private readonly EnemyPatrolModeData    _enemyPatrolModeData;

    private float   _lastTurnTime;
    private int     _amountOfTurnsDone;

    public EnemyLookForPlayerState(EnemyPatrolMode mode, EnemyPatrolModeData data, string animBoolName)
        : base(mode, animBoolName)
    {
        _enemyPatrolMode        = mode;
        _enemyPatrolModeData    = data;
    }

    public override void Enter()
    {
        base.Enter();

        _lastTurnTime       = _stateStartTime;
        _amountOfTurnsDone  = 0;
        _enemyPatrolMode.Target_SetVelocityZero();
    }

    public override void Update()                                          // Counts turns and time between turns
    {
        base.Update();

        if (_enemyPatrolMode.Target_CheckPlayerInMinAgroRange())
            _enemyPatrolMode.ExitCurrentMode(); // transition to aggro mode
        else if (_amountOfTurnsDone >= _enemyPatrolModeData.AmountOfTurns)
            _enemyPatrolMode.ChangeState(EnemyPatrolMode.StateID.Move);
        else if (Time.time >= _lastTurnTime + _enemyPatrolModeData.TimeBetweenTurns)
        {
            _enemyPatrolMode.Target_Flip();
            _lastTurnTime = Time.time;
            ++_amountOfTurnsDone;
        }
        else
        {
            // wait for next turn time
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
