
public class BringerOfDeathLookForPlayerState : EnemyLookForPlayerState
{
    private BringerOfDeath _bod;

    public BringerOfDeathLookForPlayerState(BringerOfDeath bod, string animBoolName, Data_LookForPlayer stateData)
        : base(bod, animBoolName, stateData)
    {
        _bod = bod;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isPLayerInMinAgroRange)
            _bod.ChangeState((int)BringerOfDeathStateID.PlayerDetected);
        else if (_isAllTurnsTimeDone)
            _bod.ChangeState((int)BringerOfDeathStateID.Move);
    }
}
