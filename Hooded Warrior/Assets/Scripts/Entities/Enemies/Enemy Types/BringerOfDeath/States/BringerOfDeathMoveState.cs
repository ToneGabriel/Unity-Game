
public class BringerOfDeathMoveState : EnemyMoveState
{
    private BringerOfDeath _bod;

    public BringerOfDeathMoveState(BringerOfDeath bod, string animBoolName, Data_Move stateData)
        : base(bod, animBoolName, stateData)
    {
        _bod = bod;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isPlayerInMinAgroRange)
            _bod.ChangeState((int)BringerOfDeathStateID.PlayerDetected);
        else if (_isDetectingWall || !_isDetectingLedge)
        {
            //_bod.IdleState.SetFlipAfterIdle(true);
            _bod.ChangeState((int)BringerOfDeathStateID.Idle);
        }
    }
}
