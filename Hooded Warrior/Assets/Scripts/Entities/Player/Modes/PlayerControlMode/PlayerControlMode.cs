using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerControlMode : EntityMode
{
    private readonly Player                 _target;
    private readonly PlayerControlModeData  _controlData;

    public override string[] AnimatorParameterNames { get { return AnimatorParameters.GetAnimatorParameterNames(); } }

    public PlayerControlMode(Player player, PlayerControlModeData data)
        : base(player)
    {
        _target         = player;
        _controlData    = data;

        // Initialize FSM
        InitializeStates
        (
            new KeyValuePair<int, State>((int)StateID.Idle, new PlayerIdleState(this, _controlData, AnimatorParameters.Idle_b)),
            new KeyValuePair<int, State>((int)StateID.Move, new PlayerMoveState(this, _controlData, AnimatorParameters.Move_b))
        );

        SetDefaultState((int)StateID.Default);
    }

    public enum StateID
    {
        Idle,
        Move,
        Jump,
        InAir,
        Land,
        WallSlide,
        WallGrab,
        WallClimb,
        WallJump,
        LedgeClimb,
        Dash,
        CrouchIdle,
        CrouchMove,
        Roll,
        PrimaryAttack,
        SecondaryDefend,
        SpellCast,

        Default = Idle
    }

    private static class AnimatorParameters
    {
        public static readonly string Idle_b = "Idle_b";
        public static readonly string Move_b = "Move_b";

        public static string[] GetAnimatorParameterNames()
        {
            return new string[] {
                                    Idle_b,
                                    Move_b,
                                };
        }
    }
}
