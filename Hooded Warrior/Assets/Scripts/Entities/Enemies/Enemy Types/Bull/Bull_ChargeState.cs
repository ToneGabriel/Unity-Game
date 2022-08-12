
public class Bull_ChargeState : ChargeState
{
    private Bull _bull;

    public Bull_ChargeState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_Charge stateData) 
        : base(enemy, stateMachine, animBoolName, stateData)
    {
        _bull = (Bull)enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isPlayerInMeleeRange)
            _stateMachine.ChangeState(_bull.MeleeAttackState);
        else if (!_isDetectingLedge || _isDetectingWall)
            _stateMachine.ChangeState(_bull.LookForPlayerState);
        else if (_isChargeTimeOver)
        {
            if (_isPlayerInMinAgroRange)
                _stateMachine.ChangeState(_bull.PlayerDetectedState);
            else
                _stateMachine.ChangeState(_bull.LookForPlayerState);
        }    
    }
}
