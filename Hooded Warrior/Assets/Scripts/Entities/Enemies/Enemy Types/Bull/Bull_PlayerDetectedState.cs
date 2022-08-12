
public class Bull_PlayerDetectedState : PlayerDetectedState
{
    private Bull _bull;

    public Bull_PlayerDetectedState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_PlayerDetected stateData)
        : base(enemy, stateMachine, animBoolName, stateData)
    {
        _bull = (Bull)enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_bull.MeleeAttackState.IsOnCooldown && _isPlayerInMeleeRange && _canMove)
            _stateMachine.ChangeState(_bull.MeleeAttackState);
        else if (_isPLayerInMinAgroRange && _canMove)
            _stateMachine.ChangeState(_bull.ChargeState);
        else if (!_isPLayerInMaxAgroRange)
            _stateMachine.ChangeState(_bull.LookForPlayerState);
    }
}
