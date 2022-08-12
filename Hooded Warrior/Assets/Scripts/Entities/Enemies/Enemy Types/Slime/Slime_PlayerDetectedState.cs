
public class Slime_PlayerDetectedState : PlayerDetectedState
{
    private Slime _slime;

    public Slime_PlayerDetectedState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_PlayerDetected stateData) 
        : base(enemy, stateMachine, animBoolName, stateData)
    {
        _slime = (Slime)enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!_isPLayerInMaxAgroRange)
        {
            _slime.IdleState.SetFlipAfterIdle(false);
            _stateMachine.ChangeState(_slime.IdleState);
        }
    }
}
