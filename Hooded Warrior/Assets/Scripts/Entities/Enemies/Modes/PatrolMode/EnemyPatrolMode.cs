using System.Collections.Generic;
using UnityEngine;


public sealed class EnemyPatrolMode : EntityMode
{
    private readonly Enemy                  _target;
    private readonly EnemyPatrolModeData    _patrolData;

    public override string[] AnimatorParameterNames { get { return AnimatorParameters.GetAnimatorParameterNames(); } }

    public EnemyPatrolMode(Enemy enemy, EnemyPatrolModeData data)
        : base(enemy)
    {
        _target     = enemy;
        _patrolData = data;

        // Initialize FSM
        InitializeStates
        (
            new KeyValuePair<int, State>((int)StateID.Idle,             new EnemyIdleState(this, _patrolData, AnimatorParameters.Idle_b)),
            new KeyValuePair<int, State>((int)StateID.Move,             new EnemyMoveState(this, _patrolData, AnimatorParameters.Move_b)),
            new KeyValuePair<int, State>((int)StateID.LookForPlayer,    new EnemyLookForPlayerState(this, _patrolData, AnimatorParameters.LookForPlayer_b))
        );

        SetDefaultState((int)StateID.Default);
    }

    #region Checkers
    public bool CheckPlayerInMinAgroRange()                                     // Raycast to check agro enter range
    {
        return Physics2D.Raycast(   _target.Sensors.EnvironmentCheck.transform.position,
                                    _target.Sensors.EnvironmentCheck.transform.right,
                                    _target.EnemyBaseData.MinAgroDistance, _target.EnemyBaseData.WhatIsPlayer);
    }

    public bool CheckPlayerInMaxAgroRange()                                     // Raycast to check agro exit range
    {
        return Physics2D.Raycast(   _target.Sensors.EnvironmentCheck.transform.position,
                                    _target.Sensors.EnvironmentCheck.transform.right,
                                    _target.EnemyBaseData.MaxAgroDistance, _target.EnemyBaseData.WhatIsPlayer);
    }

    public bool CheckPlayerInMeleeRange()                                       // Raycast to check melee range
    {
        return Physics2D.Raycast(   _target.Sensors.EnvironmentCheck.transform.position,
                                    _target.Sensors.EnvironmentCheck.transform.right,
                                    _target.EnemyBaseData.CloseRangeActionDistance, _target.EnemyBaseData.WhatIsPlayer);
    }
    #endregion

    public enum StateID
    {
        Idle,
        Move,
        LookForPlayer,

        Default = LookForPlayer
    }

    private static class AnimatorParameters
    {
        public static readonly string Idle_b            = "Idle_b";
        public static readonly string Move_b            = "Move_b";
        public static readonly string LookForPlayer_b   = "LookForPlayer_b";

        public static string[] GetAnimatorParameterNames()
        {
            return new string[] {
                                    Idle_b,
                                    Move_b,
                                    LookForPlayer_b
                                };
        }
    }
}