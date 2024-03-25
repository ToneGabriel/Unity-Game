
public class SlimePlayerDetectedState : EnemyPlayerDetectedState
{
    private Slime _slime;

    public SlimePlayerDetectedState(Slime slime, string animBoolName, Data_PlayerDetected stateData) 
        : base(slime, animBoolName, stateData)
    {
        _slime = slime;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!_isPLayerInMaxAgroRange)
        {
            //_slime.IdleState.SetFlipAfterIdle(false);
            _slime.ChangeState((int)SlimeStateID.Idle);
        }
    }
}
