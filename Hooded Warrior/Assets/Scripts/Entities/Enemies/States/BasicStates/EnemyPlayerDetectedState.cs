using UnityEngine;

public class EnemyPlayerDetectedState : EnemyState
{
    protected EnemyStateData _stateData;
    protected bool _isPLayerInMinAgroRange;
    protected bool _isPLayerInMaxAgroRange;
    protected bool _isPlayerInMeleeRange;
    protected bool _canMove;

    public EnemyPlayerDetectedState(Enemy enemy, string animBoolName, EnemyStateData stateData) 
        : base(enemy, animBoolName)
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

        if (Time.time >= _stateStartTime + _stateData.LookTime)                 // Counts time before taking action
            _canMove = true;
    }

    protected override void DoChecks()                                             // Check Ranges
    {
        base.DoChecks();

        _isPLayerInMinAgroRange = _enemy.CheckPlayerInMinAgroRange();
        _isPLayerInMaxAgroRange = _enemy.CheckPlayerInMaxAgroRange();
        _isPlayerInMeleeRange = _enemy.CheckPlayerInMeleeRange();
    }

}
