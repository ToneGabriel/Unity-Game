
public class ArcherLookForPlayerState : EnemyLookForPlayerState
{
    private Archer _archer;

    public ArcherLookForPlayerState(Archer archer, string animBoolName, Data_LookForPlayer stateData) 
        : base(archer, animBoolName, stateData)
    {
        _archer = archer;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isPLayerInMinAgroRange)
            _archer.ChangeState((int)ArcherStateID.PlayerDetected);
        else if (_isAllTurnsTimeDone)
            _archer.ChangeState((int)ArcherStateID.Move);
    }
}
