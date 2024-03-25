
public class ArcherDodgeState : EnemyDodgeState
{
    private Archer _archer;

    public ArcherDodgeState(Archer archer, string animBoolName, Data_Dodge stateData)
        : base(archer, animBoolName, stateData)
    {
        _archer = archer;
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(_isDodgeOver)
        {
            if (_isPlayerInMeleeRange)
                _archer.ChangeState((int)ArcherStateID.MeleeAttack);
            else if (_isPlayerInMaxAgroRange && !_isPlayerInMeleeRange)
                _archer.ChangeState((int)ArcherStateID.RangedAttack);
            else if (!_isPlayerInMaxAgroRange)
                _archer.ChangeState((int)ArcherStateID.LookForPlayer);
        }
    }
}
