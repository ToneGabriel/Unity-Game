
public class Slime_MoveState : MoveState
{
    private Slime _slime;

    public Slime_MoveState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_Move stateData)
        : base(enemy, stateMachine, animBoolName, stateData)
    {
        _slime = (Slime)enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isPlayerInMinAgroRange)
            _stateMachine.ChangeState(_slime.PlayerDetectedState);
        else if (_isDetectingWall || !_isDetectingLedge)
        {
            _slime.IdleState.SetFlipAfterIdle(true);
            _stateMachine.ChangeState(_slime.IdleState);
        }
    }
}
