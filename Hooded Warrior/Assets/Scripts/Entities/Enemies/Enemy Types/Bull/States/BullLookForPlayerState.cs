
public class BullLookForPlayerState : EnemyLookForPlayerState
{
    private Bull _bull;

    public BullLookForPlayerState(Bull bull, string animBoolName, Data_LookForPlayer stateData)
        : base(bull, animBoolName, stateData)
    {
        _bull = bull;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isPLayerInMinAgroRange)
            _bull.ChangeState((int)BullStateID.PlayerDetected);
        else if (_isAllTurnsTimeDone)
            _bull.ChangeState((int)BullStateID.Move);
    }
}
