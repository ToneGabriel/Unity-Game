using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerControlMode : EntityMode<PlayerControlMode.StateID>
{
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

    private readonly Player                 _target;
    private readonly PlayerControlModeData  _controlData;
    private PlayerActionComponents          _playerActionComponents;    // TODO: rename PlayerActionParameters


    public ref PlayerActionComponents ActionComponents { get { return ref _playerActionComponents; } }
    public override string[] AnimatorParameterNames { get { return AnimatorParameters.GetAnimatorParameterNames(); } }

    public PlayerControlMode(Player player, PlayerControlModeData data)
        : base(player)
    {
        _target         = player;
        _controlData    = data;

        // Initialize FSM
        InitializeStates
        (
            new KeyValuePair<StateID, State>(StateID.Idle, new PlayerIdleState(this, _controlData, AnimatorParameters.Idle_b)),
            new KeyValuePair<StateID, State>(StateID.Move, new PlayerMoveState(this, _controlData, AnimatorParameters.Move_b))
        );

        SetDefaultState(StateID.Default);
    }
}
