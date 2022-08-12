
public class Bull_StunState : StunState
{
    private Bull _bull;

    public Bull_StunState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_Stun stateData)
        : base(enemy, stateMachine, animBoolName, stateData)
    {
        _bull = (Bull)enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isStunTimeOver)
        {
            if (_isPlayerInMeleeRange)
                _stateMachine.ChangeState(_bull.MeleeAttackState);
            else if (_isPlayerInMinAgroRange)
                _stateMachine.ChangeState(_bull.ChargeState);
            else
            {
                _bull.LookForPlayerState.SetTurnImmediately(true);
                _stateMachine.ChangeState(_bull.LookForPlayerState);
            }
        }
    }
}
