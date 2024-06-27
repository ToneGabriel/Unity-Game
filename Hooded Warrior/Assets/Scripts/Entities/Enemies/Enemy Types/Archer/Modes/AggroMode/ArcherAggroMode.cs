using UnityEngine;
using System.Collections.Generic;

public sealed class ArcherAggroMode : EntityMode
{
    private Archer                 _target;
    private ArcherAggroModeData    _aggroData;

    public override string[] AnimatorParameterNames { get { return AnimatorParameters.GetAnimatorParameterNames(); } }

    public ArcherAggroMode(Archer archer, ArcherAggroModeData data)
        : base(archer)
    {
        _target     = archer;
        _aggroData  = data;

        // Initialize FSM
        InitializeStates
        (
            new KeyValuePair<int, State>((int)StateID.PlayerDetected,   new ArcherPlayerDetectedState(this, _aggroData, AnimatorParameters.PlayerDetected_b)),
            new KeyValuePair<int, State>((int)StateID.Dodge,            new ArcherDodgeState(this, _aggroData, AnimatorParameters.Dodge_b)),
            new KeyValuePair<int, State>((int)StateID.MeleeAttack,      new ArcherMeleeAttackState(this, _aggroData, AnimatorParameters.MeleeAttack_b)),
            new KeyValuePair<int, State>((int)StateID.RangedAttack,     new ArcherRangedAttackState(this, _aggroData, AnimatorParameters.RangedAttack_b))
        );

        SetDefaultState((int)StateID.Default);
    }

    #region Checkers
    public bool CheckPlayerInMinAgroRange()                                     // Raycast to check agro enter range
    {
        return Physics2D.Raycast(_target.Sensors.EnvironmentCheck.transform.position,
                                    _target.Sensors.EnvironmentCheck.transform.right,
                                    _target.EnemyBaseData.MinAgroDistance, _target.EnemyBaseData.WhatIsPlayer);
    }

    public bool CheckPlayerInMaxAgroRange()                                     // Raycast to check agro exit range
    {
        return Physics2D.Raycast(_target.Sensors.EnvironmentCheck.transform.position,
                                    _target.Sensors.EnvironmentCheck.transform.right,
                                    _target.EnemyBaseData.MaxAgroDistance, _target.EnemyBaseData.WhatIsPlayer);
    }

    public bool CheckPlayerInMeleeRange()                                       // Raycast to check melee range
    {
        return Physics2D.Raycast(_target.Sensors.EnvironmentCheck.transform.position,
                                    _target.Sensors.EnvironmentCheck.transform.right,
                                    _target.EnemyBaseData.CloseRangeActionDistance, _target.EnemyBaseData.WhatIsPlayer);
    }
    #endregion

    public enum StateID
    {
        PlayerDetected,
        Dodge,
        MeleeAttack,
        RangedAttack,

        Default = PlayerDetected
    }

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