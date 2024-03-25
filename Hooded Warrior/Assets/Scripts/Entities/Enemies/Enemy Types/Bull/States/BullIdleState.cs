
public class BullIdleState : EnemyIdleState
{
    private Bull _bull;

    public BullIdleState(Bull bull, string animBoolName, Data_Idle stateData)
        : base(bull, animBoolName, stateData)
    {
        _bull = bull;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isPlayerInMinAgroRange)
            _bull.ChangeState((int)BullStateID.PlayerDetected);
        else if (_isIdleTimeOver)
            _bull.ChangeState((int)BullStateID.Move);
    }
}
