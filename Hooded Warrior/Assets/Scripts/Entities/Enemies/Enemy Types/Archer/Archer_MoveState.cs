
public class Archer_MoveState : MoveState
{
    private Archer _archer;

    public Archer_MoveState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_Move stateData) 
        : base(enemy, stateMachine, animBoolName, stateData)
    {
        _archer = (Archer)enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isPlayerInMinAgroRange)
            _stateMachine.ChangeState(_archer.PlayerDetectedState);
        else if (_isDetectingWall || !_isDetectingLedge)
        {
            _archer.IdleState.SetFlipAfterIdle(true);
            _stateMachine.ChangeState(_archer.IdleState);
        }
    }
}
