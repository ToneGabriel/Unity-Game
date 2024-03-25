
public class BullMoveState : EnemyMoveState
{
    private Bull _bull;

    public BullMoveState(Bull bull, string animBoolName, Data_Move stateData)
        : base(bull, animBoolName, stateData)
    {
        _bull = bull;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isPlayerInMinAgroRange)
            _bull.ChangeState((int)BullStateID.PlayerDetected);
        else if (_isDetectingWall || !_isDetectingLedge)
        {
            //_bull.IdleState.SetFlipAfterIdle(true);
            _bull.ChangeState((int)BullStateID.Idle);
        }
    }
}
