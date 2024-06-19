using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class EnemyHitMode : EntityMode
{
    private Enemy               _target;
    private EnemyHitModeData    _hitData;

    public EnemyHitMode(Enemy enemy)
        : base(enemy)
    {
        _target = enemy;

        // Initialize FSM
        CreateStateArray((int)StateID.Count);

        AddNewState((int)StateID.Hit,   new EnemyHitState(this, _hitData, AnimatorParameters.Hit_b));
        AddNewState((int)StateID.Stun,  new EnemyStunState(this, _hitData, AnimatorParameters.Stun_b));
        AddNewState((int)StateID.Dead,  new EnemyDeadState(this, _hitData, AnimatorParameters.Dead_b));

        SetDefaultState((int)StateID.Hit);
    }

    public override string[] AnimatorParameterNames => throw new System.NotImplementedException();

    public enum StateID
    {
        Hit,
        Stun,
        Dead,

        Count
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
}
