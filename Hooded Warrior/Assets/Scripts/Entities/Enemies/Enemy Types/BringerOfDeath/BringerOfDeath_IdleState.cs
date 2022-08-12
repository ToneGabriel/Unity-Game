
public class BringerOfDeath_IdleState : IdleState
{
    private BringerOfDeath _bod;

    public BringerOfDeath_IdleState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_Idle stateData)
        : base(enemy, stateMachine, animBoolName, stateData)
    {
        _bod = (BringerOfDeath)enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isPlayerInMinAgroRange)
            _stateMachine.ChangeState(_bod.PlayerDetectedState);
        else if (_isIdleTimeOver)
            _stateMachine.ChangeState(_bod.MoveState);
    }
}
