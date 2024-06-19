using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerOfDeathAggroMode : EntityMode
{
    private BringerOfDeath              _target;
    private BringerOfDeathAggroModeData _aggroData;

    public BringerOfDeathAggroMode(BringerOfDeath bod, BringerOfDeathAggroModeData data)
        : base(bod)
    {
        _target     = bod;
        _aggroData  = data;

        // Initialize FSM
        CreateStateArray((int)StateID.Count);

        AddNewState((int)StateID.PlayerDetected,    new BringerOfDeathPlayerDetectedState(this, _aggroData, AnimatorParameters.PlayerDetected_b));
        AddNewState((int)StateID.MeleeAttack,       new BringerOfDeathMeleeAttackState(this, _aggroData, AnimatorParameters.MeleeAttack_b));
        AddNewState((int)StateID.RangedAttack,      new BringerOfDeathRangedAttackState(this, _aggroData, AnimatorParameters.RangedAttack_b));

        SetDefaultState((int)StateID.PlayerDetected);
    }

    public override string[] AnimatorParameterNames => throw new System.NotImplementedException();

    public enum StateID
    {
        PlayerDetected,
        MeleeAttack,
        RangedAttack,

        Count
    }

    //public enum BringerOfDeathStateID
    //{
    //    PlayerDetected,
    //    Charge,
    //    MeleeAttack,
    //    PortalRangedAttack,
    //    OrbRangedAttack,
    //}

    private static class AnimatorParameters
    {
        public static readonly string PlayerDetected_b  = "PlayerDetected_b";
        public static readonly string Dodge_b           = "Dodge_b";
        public static readonly string MeleeAttack_b     = "MeleeAttack_b";
        public static readonly string RangedAttack_b    = "RangedAttack_b";

        public static string[] GetAnimatorParameterNames()
        {
            return new string[] {
                                    PlayerDetected_b,
                                    Dodge_b,
                                    MeleeAttack_b,
                                    RangedAttack_b,
                                };
        }
    }
}
