
public class ArcherStunState : EnemyStunState
{
    private Archer _archer;

    public ArcherStunState(Archer archer, string animBoolName, Data_Stun stateData) 
        : base(archer, animBoolName, stateData)
    {
        _archer = archer;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if (_isStunTimeOver)
        {
            if (_isPlayerInMeleeRange)
                _archer.ChangeState((int)ArcherStateID.MeleeAttack);
            else if (_isPlayerInMinAgroRange)
                _archer.ChangeState((int)ArcherStateID.PlayerDetected);
            else
            {
                //_archer.LookForPlayerState.SetTurnImmediately(true);
                _archer.ChangeState((int)ArcherStateID.LookForPlayer);
            }
        }
    }
}
