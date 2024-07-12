using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullAggroMode : EntityMode<BullAggroMode.StateID>
{
    public enum StateID
    {
        PlayerDetected,
        Charge,
        MeleeAttack
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
            new KeyValuePair<StateID, State>(StateID.PlayerDetected,    new BullPlayerDetectedState(this, _aggroData, AnimatorParameters.PlayerDetected_b)),
            new KeyValuePair<StateID, State>(StateID.Charge,            new BullChargeState(this, _aggroData, AnimatorParameters.Charge_b)),
            new KeyValuePair<StateID, State>(StateID.MeleeAttack,       new BullMeleeAttackState(this, _aggroData, AnimatorParameters.MeleeAttack_b))
        );

        SetDefaultState(StateID.PlayerDetected);
    }
}
