using UnityEngine;

public sealed class ArcherAggroMode : FiniteStateMachine
{
    [SerializeField] private Archer                 _target;
    [SerializeField] private ArcherAggroModeData    _aggroData;

    public override string[] AnimatorParameterNames { get { return AnimatorParameters.GetAnimatorParameterNames(); } }

    protected override void Awake()
    {
        CreateStateArray((int)StateID.Count);

        // TODO: add look for player

        AddNewState((int)StateID.Dodge,          new ArcherDodgeState(_target, _aggroData, AnimatorParameters.Dodge_b));
        AddNewState((int)StateID.MeleeAttack,    new ArcherMeleeAttackState(_target, _aggroData, AnimatorParameters.MeleeAttack_b));
        AddNewState((int)StateID.RangedAttack,   new ArcherRangedAttackState(_target, _aggroData, AnimatorParameters.RangedAttack_b));

        SetDefaultState((int)StateID.LookForPlayer);
    }

    public enum StateID
    {
        LookForPlayer,
        Dodge,
        MeleeAttack,
        RangedAttack,

        Count
    }

    private static class AnimatorParameters
    {
        public static readonly string LookForPlayer_b   = "LookForPlayer_b";
        public static readonly string Dodge_b           = "Dodge_b";
        public static readonly string MeleeAttack_b     = "MeleeAttack_b";
        public static readonly string RangedAttack_b    = "RangedAttack_b";

        public static string[] GetAnimatorParameterNames()
        {
            return new string[] {
                                    LookForPlayer_b,
                                    Dodge_b,
                                    MeleeAttack_b,
                                    RangedAttack_b,
                                };
        }
    }
}