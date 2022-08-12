
public class BringerOfDeath_MoveState : MoveState
{
    private BringerOfDeath _bod;

    public BringerOfDeath_MoveState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_Move stateData)
        : base(enemy, stateMachine, animBoolName, stateData)
    {
        _bod = (BringerOfDeath)enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isPlayerInMinAgroRange)
            _stateMachine.ChangeState(_bod.PlayerDetectedState);
        else if (_isDetectingWall || !_isDetectingLedge)
        {
            _bod.IdleState.SetFlipAfterIdle(true);
            _stateMachine.ChangeState(_bod.IdleState);
        }
    }
}
