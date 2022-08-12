using UnityEngine;

public abstract class PlayerDetectedState : EnemyState
{
    protected Data_PlayerDetected _stateData;
    protected bool _isPLayerInMinAgroRange;
    protected bool _isPLayerInMaxAgroRange;
    protected bool _isPlayerInMeleeRange;
    protected bool _canMove;

    public PlayerDetectedState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_PlayerDetected stateData) 
        : base(enemy, stateMachine, animBoolName)
    {
        _stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        _enemy.SetVelocityZero();
        _canMove = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= StartTime + _stateData.LookTime)                        // Counts time before taking action
            _canMove = true;
    }

    public override void DoChecks()                                             // Check Ranges
    {
        base.DoChecks();

        _isPLayerInMinAgroRange = _enemy.CheckPlayerInMinAgroRange();
        _isPLayerInMaxAgroRange = _enemy.CheckPlayerInMaxAgroRange();
        _isPlayerInMeleeRange = _enemy.CheckPlayerInMeleeRange();
    }

}
