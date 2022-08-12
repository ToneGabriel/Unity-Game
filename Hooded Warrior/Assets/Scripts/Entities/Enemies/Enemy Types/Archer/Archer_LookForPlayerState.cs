
public class Archer_LookForPlayerState : LookForPlayerState
{
    private Archer _archer;

    public Archer_LookForPlayerState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_LookForPlayer stateData) 
        : base(enemy, stateMachine, animBoolName, stateData)
    {
        _archer = (Archer)enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isPLayerInMinAgroRange)
            _stateMachine.ChangeState(_archer.PlayerDetectedState);
        else if (_isAllTurnsTimeDone)
            _stateMachine.ChangeState(_archer.MoveState);
    }
}
