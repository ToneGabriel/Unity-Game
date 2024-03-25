
public class BringerOfDeathIdleState : EnemyIdleState
{
    private BringerOfDeath _bod;

    public BringerOfDeathIdleState(BringerOfDeath bod, string animBoolName, Data_Idle stateData)
        : base(bod, animBoolName, stateData)
    {
        _bod = bod;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isPlayerInMinAgroRange)
            _bod.ChangeState((int)BringerOfDeathStateID.PlayerDetected);
        else if (_isIdleTimeOver)
            _bod.ChangeState((int)BringerOfDeathStateID.Move);
    }
}
