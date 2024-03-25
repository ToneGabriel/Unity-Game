
public class ArcherMoveState : EnemyMoveState
{
    private Archer _archer;

    public ArcherMoveState(Archer archer, string animBoolName, Data_Move stateData) 
        : base(archer, animBoolName, stateData)
    {
        _archer = archer;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isPlayerInMinAgroRange)
            _archer.ChangeState((int)ArcherStateID.PlayerDetected);
        else if (_isDetectingWall || !_isDetectingLedge)
        {
            //_archer.IdleState.SetFlipAfterIdle(true);
            _archer.ChangeState((int)ArcherStateID.Idle);
        }
    }
}
