
public class BringerOfDeath_LookForPlayerState : LookForPlayerState
{
    private BringerOfDeath _bod;

    public BringerOfDeath_LookForPlayerState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_LookForPlayer stateData)
        : base(enemy, stateMachine, animBoolName, stateData)
    {
        _bod = (BringerOfDeath)enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isPLayerInMinAgroRange)
            _stateMachine.ChangeState(_bod.PlayerDetectedState);
        else if (_isAllTurnsTimeDone)
            _stateMachine.ChangeState(_bod.MoveState);
    }
}
