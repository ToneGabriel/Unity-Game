
public class BullChargeState : EntityModeState<BullAggroMode.StateID>
{
    private BullAggroMode     _bullAggroMode;
    private BullAggroModeData _bullAggroModeData;

    public BullChargeState(BullAggroMode mode, BullAggroModeData data, string animBoolName) 
        : base(mode, animBoolName)
    {
        _bullAggroMode      = mode;
        _bullAggroModeData  = data;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //if (_isPlayerInMeleeRange)
        //    _bull.ChangeState((int)BullStateID.MeleeAttack);
        //else if (!_isDetectingLedge || _isDetectingWall)
        //    _bull.ChangeState((int)BullStateID.LookForPlayer);
        //else if (_isChargeTimeOver)
        //{
        //    if (_isPlayerInMinAgroRange)
        //        _bull.ChangeState((int)BullStateID.PlayerDetected);
        //    else
        //        _bull.ChangeState((int)BullStateID.LookForPlayer);
        //}    
    }
}
