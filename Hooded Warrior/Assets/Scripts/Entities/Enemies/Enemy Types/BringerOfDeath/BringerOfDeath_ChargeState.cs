
public class BringerOfDeath_ChargeState : ChargeState
{
    private BringerOfDeath _bod;

    public BringerOfDeath_ChargeState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_Charge stateData)
        : base(enemy, stateMachine, animBoolName, stateData)
    {
        _bod = (BringerOfDeath)enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isPlayerInMeleeRange)
            _stateMachine.ChangeState(_bod.MeleeAttackState);
        else if (!_isDetectingLedge || _isDetectingWall)
            _stateMachine.ChangeState(_bod.LookForPlayerState);
        else if (_isChargeTimeOver)
        {
            if (_isPlayerInMinAgroRange)
                _stateMachine.ChangeState(_bod.PlayerDetectedState);
            else
                _stateMachine.ChangeState(_bod.LookForPlayerState);
        }
    }
}
