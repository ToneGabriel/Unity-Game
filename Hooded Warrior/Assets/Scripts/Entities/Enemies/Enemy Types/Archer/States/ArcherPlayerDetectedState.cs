using UnityEngine;

public class ArcherPlayerDetectedState : EntityModeState
{
    private ArcherAggroMode     _archerAggroMode;
    private ArcherAggroModeData _archerAggroModeData;

    protected bool _isPLayerInMinAgroRange;
    protected bool _isPLayerInMaxAgroRange;
    protected bool _isPlayerInMeleeRange;
    protected bool _canMove;

    public ArcherPlayerDetectedState(ArcherAggroMode mode, ArcherAggroModeData data, string animBoolName) 
        : base(mode, animBoolName)
    {
        _archerAggroMode        = mode;
        _archerAggroModeData    = data;
    }

    public override void Enter()
    {
        base.Enter();

        _archerAggroMode.SetVelocityZero();
        _canMove = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= _stateStartTime + _archerAggroModeData.LookTime)       // Counts time before taking action
            _canMove = true;

        if (false)
            _enemy.ChangeMode((int)EnemyModeID.Aggro);
        else if (false)
            _archerAggroMode.ChangeState((int)EnemyPatrolMode.StateID.Move);
    }

    protected override void DoChecks()                                             // Check Ranges
    {
        base.DoChecks();

        _isPLayerInMinAgroRange = _archerAggroMode.CheckPlayerInMinAgroRange();
        _isPLayerInMaxAgroRange = _archerAggroMode.CheckPlayerInMaxAgroRange();
        _isPlayerInMeleeRange = _archerAggroMode.CheckPlayerInMeleeRange();
    }

}
