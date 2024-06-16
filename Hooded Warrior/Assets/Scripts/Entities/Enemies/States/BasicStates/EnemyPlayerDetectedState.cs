using UnityEngine;

public class EnemyPlayerDetectedState : EnemyPatrolState
{
    protected bool _isPLayerInMinAgroRange;
    protected bool _isPLayerInMaxAgroRange;
    protected bool _isPlayerInMeleeRange;
    protected bool _canMove;

    public EnemyPlayerDetectedState(Enemy enemy, EnemyPatrolModeData data, string animBoolName) 
        : base(enemy, data, animBoolName) {}

    public override void Enter()
    {
        base.Enter();

        _enemy.SetVelocityZero();
        _canMove = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= _stateStartTime + _data.LookTime)       // Counts time before taking action
            _canMove = true;

        if (false)
            _enemy.ChangeMode((int)EnemyModeID.Aggro);
        else if (false)
            _enemy.ChangeState((int)EnemyPatrolMode.StateID.Move);
    }

    protected override void DoChecks()                                             // Check Ranges
    {
        base.DoChecks();

        _isPLayerInMinAgroRange = _enemy.CheckPlayerInMinAgroRange();
        _isPLayerInMaxAgroRange = _enemy.CheckPlayerInMaxAgroRange();
        _isPlayerInMeleeRange = _enemy.CheckPlayerInMeleeRange();
    }

}
