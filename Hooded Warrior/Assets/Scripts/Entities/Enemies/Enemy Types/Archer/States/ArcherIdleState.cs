
public class ArcherIdleState : EnemyIdleState
{
    private Archer _archer;

    public ArcherIdleState(Archer archer, string animBoolName, Data_Idle stateData) 
        : base(archer, animBoolName, stateData)
    {
        _archer = archer;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isPlayerInMinAgroRange)
            _archer.ChangeState((int)ArcherStateID.PlayerDetected);
        else if (_isIdleTimeOver)
            _archer.ChangeState((int)ArcherStateID.Move);
    }
}
