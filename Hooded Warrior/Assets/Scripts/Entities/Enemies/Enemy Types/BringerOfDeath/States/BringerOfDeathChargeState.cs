
public class BringerOfDeathChargeState : EnemyChargeState
{
    private BringerOfDeath _bod;

    public BringerOfDeathChargeState(BringerOfDeath bod, string animBoolName, Data_Charge stateData)
        : base(bod, animBoolName, stateData)
    {
        _bod = bod;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isPlayerInMeleeRange)
            _bod.ChangeState((int)BringerOfDeathStateID.MeleeAttack);
        else if (!_isDetectingLedge || _isDetectingWall)
            _bod.ChangeState((int)BringerOfDeathStateID.LookForPlayer);
        else if (_isChargeTimeOver)
        {
            if (_isPlayerInMinAgroRange)
                _bod.ChangeState((int)BringerOfDeathStateID.PlayerDetected);
            else
                _bod.ChangeState((int)BringerOfDeathStateID.LookForPlayer);
        }
    }
}
