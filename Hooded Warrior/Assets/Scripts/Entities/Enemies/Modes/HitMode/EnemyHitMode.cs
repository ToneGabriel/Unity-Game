using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public sealed class EnemyHitMode : EntityMode<EnemyHitMode.StateID>
{
    public enum StateID
    {
        Hit,
        Stun,
        Dead
    }

    private static class AnimatorParameters
    {
        public static readonly string Hit_b     = "Hit_b";
        public static readonly string Stun_b    = "Stun_b";
        public static readonly string Dead_b    = "Dead_b";

        public static string[] GetAnimatorParameterNames()
        {
            return new string[] {
                                    Hit_b,
                                    Stun_b,
                                    Dead_b
                                };
        }
    }

    private readonly Enemy              _target;
    private readonly EnemyHitModeData   _hitData;

    public override string[] AnimatorParameterNames { get { return AnimatorParameters.GetAnimatorParameterNames(); } }

    public EnemyHitMode(Enemy enemy, EnemyHitModeData data)
        : base(enemy)
    {
        _target     = enemy;
        _hitData    = data;

        // Initialize FSM
        InitializeStates
        (
            new KeyValuePair<StateID, State>(StateID.Hit,   new EnemyHitState(this, _hitData, AnimatorParameters.Hit_b)),
            new KeyValuePair<StateID, State>(StateID.Stun,  new EnemyStunState(this, _hitData, AnimatorParameters.Stun_b)),
            new KeyValuePair<StateID, State>(StateID.Dead,  new EnemyDeadState(this, _hitData, AnimatorParameters.Dead_b))
        );

        SetDefaultState(StateID.Hit);
    }
}
