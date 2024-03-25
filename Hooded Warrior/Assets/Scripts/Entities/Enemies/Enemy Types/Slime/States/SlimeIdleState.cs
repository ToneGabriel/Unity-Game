
public class SlimeIdleState : EnemyIdleState
{
    private Slime _slime;

    public SlimeIdleState(Slime slime, string animBoolName, Data_Idle stateData) 
        : base(slime, animBoolName, stateData)
    {
        _slime = slime;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isPlayerInMinAgroRange)
            _slime.ChangeState((int)SlimeStateID.PlayerDetected);
        else if (_isIdleTimeOver)
            _slime.ChangeState((int)SlimeStateID.Move);
    }
}
