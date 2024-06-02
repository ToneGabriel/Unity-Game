
public class ArcherAggroMode : FiniteStateMachine
{
    public ArcherAggroMode(Archer archer, string dodge, string meleeAttack, string rangedAttack)
        : base()
    {
        AddNewState((int)ArcherStateID.Dodge,           new ArcherDodgeState(archer, dodge));
        AddNewState((int)ArcherStateID.MeleeAttack,     new ArcherMeleeAttackState(archer, meleeAttack));
        AddNewState((int)ArcherStateID.RangedAttack,    new ArcherRangedAttackState(archer, rangedAttack));

        //AddNewState((int)ArcherStateID.Idle,            new EnemyIdleState(this, "idle", _archerStateData));
        //AddNewState((int)ArcherStateID.Move,            new EnemyMoveState(this, "walk", _archerStateData));
        //AddNewState((int)ArcherStateID.PlayerDetected,  new EnemyPlayerDetectedState(this, "playerDetected", _archerStateData));
        //AddNewState((int)ArcherStateID.LookForPlayer,   new EnemyLookForPlayerState(this, "lookForPlayer", _archerStateData));
        //AddNewState((int)ArcherStateID.Stun,            new EnemyStunState(this, "stun", _archerStateData));
        //AddNewState((int)ArcherStateID.Dead,            new EnemyDeadState(this, "dead", _archerStateData));

        _defaultStateID = (int)ArcherStateID.LookForPlayer;
    }
}
