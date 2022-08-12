
public class Bull_MoveState : MoveState
{
    private Bull _bull;

    public Bull_MoveState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_Move stateData)
        : base(enemy, stateMachine, animBoolName, stateData)
    {
        _bull = (Bull)enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isPlayerInMinAgroRange)
            _stateMachine.ChangeState(_bull.PlayerDetectedState);
        else if (_isDetectingWall || !_isDetectingLedge)
        {
            _bull.IdleState.SetFlipAfterIdle(true);
            _stateMachine.ChangeState(_bull.IdleState);
        }
    }
}
