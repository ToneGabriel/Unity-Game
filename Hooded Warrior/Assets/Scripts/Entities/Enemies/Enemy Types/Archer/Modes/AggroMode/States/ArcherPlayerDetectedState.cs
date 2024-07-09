using UnityEngine;

public sealed class ArcherPlayerDetectedState : EntityModeState<ArcherAggroMode.StateID>
{
    private readonly ArcherAggroMode        _archerAggroMode;
    private readonly ArcherAggroModeData    _archerAggroModeData;

    public ArcherPlayerDetectedState(ArcherAggroMode mode, ArcherAggroModeData data, string animBoolName) 
        : base(mode, animBoolName)
    {
        _archerAggroMode        = mode;
        _archerAggroModeData    = data;
    }

    public override void Enter()
    {
        base.Enter();

        _archerAggroMode.Target_SetVelocityZero();
    }

    public override void Update()
    {
        base.Update();

        if (Time.time >= _stateStartTime + _archerAggroModeData.LookTime)       // Counts time before taking action
        {
            if (!_archerAggroMode.CheckPlayerInMinAgroRange())
                _archerAggroMode.ExitCurrentMode(); // transition to patrol mode
            //else if ()
            //    _archerAggroMode.ChangeState(ArcherAggroMode.StateID.Dodge);
            //else if ()
            //    _archerAggroMode.ChangeState(ArcherAggroMode.StateID.MeleeAttack);
            //else if ()
            //    _archerAggroMode.ChangeState(ArcherAggroMode.StateID.RangedAttack);
        }
        else
        {
            // wait
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    protected override void DoChecks()                                             // Check Ranges
    {
        base.DoChecks();
    }

}
