
public class SlimeMoveState : EnemyMoveState
{
    private Slime _slime;

    public SlimeMoveState(Slime slime, string animBoolName, Data_Move stateData)
        : base(slime, animBoolName, stateData)
    {
        _slime = slime;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isPlayerInMinAgroRange)
            _slime.ChangeState((int)SlimeStateID.PlayerDetected);
        else if (_isDetectingWall || !_isDetectingLedge)
        {
            //_slime.IdleState.SetFlipAfterIdle(true);
            _slime.ChangeState((int)SlimeStateID.Idle);
        }
    }
}
