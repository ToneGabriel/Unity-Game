using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BringerOfDeathAggroMode : EntityMode<BringerOfDeathAggroMode.StateID>
{
    public enum StateID
    {
        PlayerDetected,
        MeleeAttack,
        RangedAttack,

        Default = PlayerDetected
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
        public static readonly string PlayerDetected_b = "PlayerDetected_b";
        public static readonly string Dodge_b = "Dodge_b";
        public static readonly string MeleeAttack_b = "MeleeAttack_b";
        public static readonly string RangedAttack_b = "RangedAttack_b";

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

    private readonly BringerOfDeath                 _target;
    private readonly BringerOfDeathAggroModeData    _aggroData;

    public override string[] AnimatorParameterNames { get { return AnimatorParameters.GetAnimatorParameterNames(); } }

    public BringerOfDeathAggroMode(BringerOfDeath bod, BringerOfDeathAggroModeData data)
        : base(bod)
    {
        _target     = bod;
        _aggroData  = data;

        // Initialize FSM
        InitializeStates
        (
            new KeyValuePair<StateID, State>(StateID.PlayerDetected,   new BringerOfDeathPlayerDetectedState(this, _aggroData, AnimatorParameters.PlayerDetected_b)),
            new KeyValuePair<StateID, State>(StateID.MeleeAttack,      new BringerOfDeathMeleeAttackState(this, _aggroData, AnimatorParameters.MeleeAttack_b)),
            new KeyValuePair<StateID, State>(StateID.RangedAttack,     new BringerOfDeathRangedAttackState(this, _aggroData, AnimatorParameters.RangedAttack_b))
        );

        SetDefaultState(StateID.Default);
    }
}
