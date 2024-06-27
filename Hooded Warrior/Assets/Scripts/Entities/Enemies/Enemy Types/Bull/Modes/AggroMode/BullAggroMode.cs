using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullAggroMode : EntityMode
{
    private readonly Bull               _target;
    private readonly BullAggroModeData  _aggroData;

    public override string[] AnimatorParameterNames { get { return AnimatorParameters.GetAnimatorParameterNames(); } }

    public BullAggroMode(Bull bull, BullAggroModeData data)
        : base(bull)
    {
        _target     = bull;
        _aggroData  = data;

        // Initialize FSM
        InitializeStates
        (
            new KeyValuePair<int, State>((int)StateID.PlayerDetected,   new BullPlayerDetectedState(this, _aggroData, AnimatorParameters.PlayerDetected_b)),
            new KeyValuePair<int, State>((int)StateID.Charge,           new BullChargeState(this, _aggroData, AnimatorParameters.Charge_b)),
            new KeyValuePair<int, State>((int)StateID.MeleeAttack,      new BullMeleeAttackState(this, _aggroData, AnimatorParameters.MeleeAttack_b))
        );

        SetDefaultState((int)StateID.Default);
    }

    public enum StateID
    {
        PlayerDetected,
        Charge,
        MeleeAttack,

        Default = PlayerDetected
    }

    private static class AnimatorParameters
    {
        public static readonly string PlayerDetected_b  = "PlayerDetected_b";
        public static readonly string Charge_b          = "Charge_b";
        public static readonly string MeleeAttack_b     = "MeleeAttack_b";

        public static string[] GetAnimatorParameterNames()
        {
            return new string[] {
                                    PlayerDetected_b,
                                    Charge_b,
                                    MeleeAttack_b,
                                };
        }
    }
}
