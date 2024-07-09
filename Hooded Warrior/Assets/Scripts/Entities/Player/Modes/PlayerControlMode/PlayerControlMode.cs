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
        SpellCast
    }

    private static class AnimatorParameters
    {
        public static readonly string Idle_b        = "Idle_b";
        public static readonly string Move_b        = "Move_b";
        public static readonly string InAir_b       = "InAir_b";
        //public static readonly string Jump_b    = InAir_b;
        public static readonly string Land_b        = "Land_b";
        public static readonly string WallSlide_b   = "WallSlide_b";

        public static readonly string VelocityX_f   = "VelocityX_f";
        public static readonly string VelocityY_f   = "VelocityY_f";

        public static string[] GetAnimatorParameterNames()
        {
            return new string[] {
                                    Idle_b,
                                    Move_b,
                                    //Jump_b,
                                    InAir_b,
                                    Land_b,
                                    WallSlide_b,

                                    VelocityX_f,
                                    VelocityY_f
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
            new KeyValuePair<StateID, State>(StateID.Idle,  new PlayerIdleState(this, _controlData, AnimatorParameters.Idle_b)),
            new KeyValuePair<StateID, State>(StateID.Move,  new PlayerMoveState(this, _controlData, AnimatorParameters.Move_b)),
            new KeyValuePair<StateID, State>(StateID.Jump,  new PlayerJumpState(this, _controlData, AnimatorParameters.InAir_b)),
            new KeyValuePair<StateID, State>(StateID.InAir, new PlayerInAirState(this, _controlData, AnimatorParameters.InAir_b, AnimatorParameters.VelocityX_f, AnimatorParameters.VelocityY_f)),
            new KeyValuePair<StateID, State>(StateID.Land,  new PlayerLandState(this, _controlData, AnimatorParameters.Land_b)),
            new KeyValuePair<StateID, State>(StateID.WallSlide,  new PlayerWallSlideState(this, _controlData, AnimatorParameters.WallSlide_b))
        );

        SetDefaultState(StateID.Idle);
    }

    public void Target_SetDashArrowRotation(Quaternion rotation)
    {
        // TODO:
        //_playerExternComponents._dashDirectionIndicator.transform.rotation = rotation;
    }
}
