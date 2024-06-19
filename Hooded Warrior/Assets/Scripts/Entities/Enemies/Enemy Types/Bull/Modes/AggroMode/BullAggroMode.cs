using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullAggroMode : EntityMode
{
    private Bull                _target;
    private BullAggroModeData   _aggroData;

    public BullAggroMode(Bull bull, BullAggroModeData data)
        : base(bull)
    {
        _target     = bull;
        _aggroData  = data;

        // Initialize FSM
        CreateStateArray((int)StateID.Count);

        AddNewState((int)StateID.PlayerDetected,    new BullPlayerDetectedState(this, _aggroData, AnimatorParameters.PlayerDetected_b));
        AddNewState((int)StateID.Charge,            new BullChargeState(this, _aggroData, AnimatorParameters.Charge_b));
        AddNewState((int)StateID.MeleeAttack,       new BullMeleeAttackState(this, _aggroData, AnimatorParameters.MeleeAttack_b));

        SetDefaultState((int)StateID.PlayerDetected);
    }

    public override string[] AnimatorParameterNames => throw new System.NotImplementedException();

    public enum StateID
    {
        PlayerDetected,
        Charge,
        MeleeAttack,

        Count
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
