using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SlimeAggroMode : EntityMode<SlimeAggroMode.StateID>
{
    public enum StateID
    {
        PlayerDetected,
        MeleeAttack
    }

    public static class AnimatorParameters
    {
        public static readonly string PlayerDetected_b  = "PlayerDetected_b";
        public static readonly string MeleeAttack_b     = "MeleeAttack_b";

        public static string[] GetAnimatorParameterNames()
        {
            return new string[] {
                                    PlayerDetected_b,
                                    MeleeAttack_b
                                };
        }
    }

    private readonly Slime              _target;
    private readonly SlimeAggroModeData _aggroData;

    public SlimeAggroMode(Slime slime, SlimeAggroModeData data)
        : base(slime)
    {
        _target     = slime;
        _aggroData  = data;

        // Initialize FSM
        InitializeStates
        (
            new KeyValuePair<StateID, State>(StateID.PlayerDetected, null),
            new KeyValuePair<StateID, State>(StateID.MeleeAttack, null)
        );

        SetDefaultState(StateID.PlayerDetected);
    }
}
