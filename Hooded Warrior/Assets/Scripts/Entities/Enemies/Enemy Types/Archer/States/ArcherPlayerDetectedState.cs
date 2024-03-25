
public class ArcherPlayerDetectedState : EnemyPlayerDetectedState
{
    private Archer _archer;

    public ArcherPlayerDetectedState(Archer archer, string animBoolName, Data_PlayerDetected stateData) 
        : base(archer, animBoolName, stateData)
    {
        _archer = archer;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //if (!_archer.MeleeAttackState.IsOnCooldown && _isPlayerInMeleeRange)
        //{
        //    if (!_archer.DodgeState.IsOnCooldown)
        //        _archer.ChangeState((int)ArcherStateID.Dodge);
        //    else
        //        _archer.ChangeState((int)ArcherStateID.MeleeAttack);
        //}
        //else if (!_archer.RangedAttackState.IsOnCooldown)
        //    _archer.ChangeState((int)ArcherStateID.RangedAttack);
        //else if (!_isPLayerInMaxAgroRange)
        //    _archer.ChangeState((int)ArcherStateID.LookForPlayer);
    }
}
