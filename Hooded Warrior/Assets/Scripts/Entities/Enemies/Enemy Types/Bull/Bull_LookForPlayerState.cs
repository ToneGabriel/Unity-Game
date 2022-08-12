
public class Bull_LookForPlayerState : LookForPlayerState
{
    private Bull _bull;

    public Bull_LookForPlayerState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_LookForPlayer stateData)
        : base(enemy, stateMachine, animBoolName, stateData)
    {
        _bull = (Bull)enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isPLayerInMinAgroRange)
            _stateMachine.ChangeState(_bull.PlayerDetectedState);
        else if (_isAllTurnsTimeDone)
            _stateMachine.ChangeState(_bull.MoveState);
    }
}
