using UnityEngine;

public sealed class ArcherAggroMode : FiniteStateMachine
{
    [SerializeField] private Archer                 _target;
    [SerializeField] private ArcherAggroModeData    _aggroData;

    // TODO: change
    public override string[] AnimatorParameterNames
    {
        get
        {
            return new string[] {   "Idle_b" ,
                                    "Move_b",
                                    "PlayerDetected_b" };
        }
    }

    protected override void Awake()
    {
        string[] animParams = AnimatorParameterNames;

        CreateStateArray((int)EnemyPatrolModeStateID.Count);

        // TODO: add look for player

        //AddNewState((int)ArcherAggroModeStateID.Dodge,          GetComponentInChildren<ArcherDodgeState>());
        //AddNewState((int)ArcherAggroModeStateID.MeleeAttack,    GetComponentInChildren<ArcherMeleeAttackState>());
        //AddNewState((int)ArcherAggroModeStateID.RangedAttack,   GetComponentInChildren<ArcherRangedAttackState>());

        SetDefaultState((int)ArcherAggroModeStateID.LookForPlayer);
    }
}

public enum ArcherAggroModeStateID
{
    LookForPlayer,
    Dodge,
    MeleeAttack,
    RangedAttack,

    Count
}
