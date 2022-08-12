
public class Archer_IdleState : IdleState
{
    private Archer _archer;

    public Archer_IdleState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_Idle stateData) 
        : base(enemy, stateMachine, animBoolName, stateData)
    {
        _archer = (Archer)enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isPlayerInMinAgroRange)
            _stateMachine.ChangeState(_archer.PlayerDetectedState);
        else if (_isIdleTimeOver)
            _stateMachine.ChangeState(_archer.MoveState);
    }
}
