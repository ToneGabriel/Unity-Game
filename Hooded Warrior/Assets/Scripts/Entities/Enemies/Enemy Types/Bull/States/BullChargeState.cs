
public class BullChargeState : EnemyChargeState
{
    private Bull _bull;

    public BullChargeState(Bull bull, string animBoolName, Data_Charge stateData) 
        : base(bull, animBoolName, stateData)
    {
        _bull = bull;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isPlayerInMeleeRange)
            _bull.ChangeState((int)BullStateID.MeleeAttack);
        else if (!_isDetectingLedge || _isDetectingWall)
            _bull.ChangeState((int)BullStateID.LookForPlayer);
        else if (_isChargeTimeOver)
        {
            if (_isPlayerInMinAgroRange)
                _bull.ChangeState((int)BullStateID.PlayerDetected);
            else
                _bull.ChangeState((int)BullStateID.LookForPlayer);
        }    
    }
}
