
public class Slime_IdleState : IdleState
{
    private Slime _slime;

    public Slime_IdleState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_Idle stateData) 
        : base(enemy, stateMachine, animBoolName, stateData)
    {
        _slime = (Slime)enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isPlayerInMinAgroRange)
            _stateMachine.ChangeState(_slime.PlayerDetectedState);
        else if (_isIdleTimeOver)
            _stateMachine.ChangeState(_slime.MoveState);
    }
}
