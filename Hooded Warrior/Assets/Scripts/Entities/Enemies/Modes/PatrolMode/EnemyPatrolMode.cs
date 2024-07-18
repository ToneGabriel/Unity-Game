using System.Collections.Generic;
using UnityEngine;


public sealed class EnemyPatrolMode : EntityMode<EnemyPatrolMode.StateID>
{
    public enum StateID
    {
        Idle,
        Move,
        LookForPlayer
    }

    public static class AnimatorParameters
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

    private readonly Enemy                  _target;
    private readonly EnemyPatrolModeData    _patrolData;

    public EnemyPatrolMode(Enemy enemy, EnemyPatrolModeData data)
        : base(enemy)
    {
        _target     = enemy;
        _patrolData = data;

        // Initialize FSM
        InitializeStates
        (
            new KeyValuePair<StateID, State>(StateID.Idle,          new EnemyIdleState(this, _patrolData, AnimatorParameters.Idle_b)),
            new KeyValuePair<StateID, State>(StateID.Move,          new EnemyMoveState(this, _patrolData, AnimatorParameters.Move_b)),
            new KeyValuePair<StateID, State>(StateID.LookForPlayer, new EnemyLookForPlayerState(this, _patrolData, AnimatorParameters.LookForPlayer_b))
        );

        SetDefaultState(StateID.LookForPlayer);
    }

    #region Checkers
    public bool Target_CheckPlayerInMinAgroRange()                                     // Raycast to check agro enter range
    {
        return Physics2D.Raycast(   _target.Sensors.EnvironmentCheck.transform.position,
                                    _target.Sensors.EnvironmentCheck.transform.right,
                                    _target.EnemyBaseData.MinAgroDistance, _target.EnemyBaseData.WhatIsPlayer);
    }

    public bool Target_CheckPlayerInMaxAgroRange()                                     // Raycast to check agro exit range
    {
        return Physics2D.Raycast(   _target.Sensors.EnvironmentCheck.transform.position,
                                    _target.Sensors.EnvironmentCheck.transform.right,
                                    _target.EnemyBaseData.MaxAgroDistance, _target.EnemyBaseData.WhatIsPlayer);
    }

    public bool Target_CheckPlayerInMeleeRange()                                       // Raycast to check melee range
    {
        return Physics2D.Raycast(   _target.Sensors.EnvironmentCheck.transform.position,
                                    _target.Sensors.EnvironmentCheck.transform.right,
                                    _target.EnemyBaseData.CloseRangeActionDistance, _target.EnemyBaseData.WhatIsPlayer);
    }
    #endregion
}