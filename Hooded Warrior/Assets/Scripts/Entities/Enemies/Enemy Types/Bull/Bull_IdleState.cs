
public class Bull_IdleState : IdleState
{
    private Bull _bull;

    public Bull_IdleState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_Idle stateData)
        : base(enemy, stateMachine, animBoolName, stateData)
    {
        _bull = (Bull)enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isPlayerInMinAgroRange)
            _stateMachine.ChangeState(_bull.PlayerDetectedState);
        else if (_isIdleTimeOver)
            _stateMachine.ChangeState(_bull.MoveState);
    }
}
