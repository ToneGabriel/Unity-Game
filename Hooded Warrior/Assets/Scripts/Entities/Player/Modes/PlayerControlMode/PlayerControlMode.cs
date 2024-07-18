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
        LedgeHang,
        LedgeClimb,
        Dash,
        CrouchIdle,
        CrouchMove,
        Roll

        //PrimaryAttack,
        //SecondaryDefend,
        //SpellCast
    }

    public static class AnimatorParameters
    {
        public static readonly string Idle_b        = "Idle_b";
        public static readonly string Move_b        = "Move_b";
        public static readonly string InAir_b       = "InAir_b";
        //public static readonly string Jump_b    = InAir_b;
        public static readonly string Land_b        = "Land_b";
        public static readonly string WallSlide_b   = "WallSlide_b";
        public static readonly string WallGrab_b    = "WallGrab_b";
        public static readonly string WallClimb_b   = "WallClimb_b";
        public static readonly string WallJump_b    = "WallJump_b";
        public static readonly string LedgeHang_b   = "LedgeHang_b";
        public static readonly string LedgeClimb_b  = "LedgeClimb_b";
        public static readonly string Dash_b        = "Dash_b";
        public static readonly string CrouchIdle_b  = "CrouchIdle_b";
        public static readonly string CrouchMove_b  = "CrouchMove_b";
        public static readonly string Roll_b        = "Roll_b";

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
                                    WallGrab_b,
                                    WallClimb_b,
                                    WallJump_b,
                                    LedgeHang_b,
                                    LedgeClimb_b,
                                    Dash_b,
                                    CrouchIdle_b,
                                    CrouchMove_b,
                                    Roll_b,

                                    VelocityX_f,
                                    VelocityY_f
                                };
        }
    }

    private readonly Player                 _target;
    private readonly PlayerControlModeData  _controlData;

    public PlayerControlMode(Player player, PlayerControlModeData data)
        : base(player)
    {
        _target         = player;
        _controlData    = data;

        // Initialize FSM
        InitializeStates
        (
            new KeyValuePair<StateID, State>(StateID.Idle,          new PlayerIdleState(this, _controlData, AnimatorParameters.Idle_b)),
            new KeyValuePair<StateID, State>(StateID.Move,          new PlayerMoveState(this, _controlData, AnimatorParameters.Move_b)),
            new KeyValuePair<StateID, State>(StateID.Jump,          new PlayerJumpState(this, _controlData, AnimatorParameters.InAir_b)),
            new KeyValuePair<StateID, State>(StateID.InAir,         new PlayerInAirState(this, _controlData, AnimatorParameters.InAir_b, AnimatorParameters.VelocityX_f, AnimatorParameters.VelocityY_f)),
            new KeyValuePair<StateID, State>(StateID.Land,          new PlayerLandState(this, _controlData, AnimatorParameters.Land_b)),
            new KeyValuePair<StateID, State>(StateID.WallSlide,     new PlayerWallSlideState(this, _controlData, AnimatorParameters.WallSlide_b)),
            new KeyValuePair<StateID, State>(StateID.WallGrab,      new PlayerWallGrabState(this, _controlData, AnimatorParameters.WallGrab_b)),
            new KeyValuePair<StateID, State>(StateID.WallClimb,     new PlayerWallClimbState(this, _controlData, AnimatorParameters.WallClimb_b)),
            new KeyValuePair<StateID, State>(StateID.WallJump,      new PlayerWallJumpState(this, _controlData, AnimatorParameters.WallJump_b)),
            new KeyValuePair<StateID, State>(StateID.LedgeHang,     new PlayerLedgeHangState(this, _controlData, AnimatorParameters.LedgeHang_b)),
            new KeyValuePair<StateID, State>(StateID.LedgeClimb,    new PlayerLedgeClimbState(this, _controlData, AnimatorParameters.LedgeClimb_b)),
            new KeyValuePair<StateID, State>(StateID.Dash,          new PlayerDashState(this, _controlData, AnimatorParameters.Dash_b)),
            new KeyValuePair<StateID, State>(StateID.CrouchIdle,    new PlayerCrouchIdleState(this, _controlData, AnimatorParameters.CrouchIdle_b)),
            new KeyValuePair<StateID, State>(StateID.CrouchMove,    new PlayerCrouchMoveState(this, _controlData, AnimatorParameters.CrouchMove_b)),
            new KeyValuePair<StateID, State>(StateID.Roll,          new PlayerRollState(this, _controlData, AnimatorParameters.Roll_b))
        );

        SetDefaultState(StateID.Idle);
    }

    #region Properties
    public int Target_JumpCount
    {
        get { return _target.JumpCount; }
        set { _target.JumpCount = value; }
    }

    public Vector2 Target_FuturePosition
    {
        get { return _target.FuturePosition; }
        set { _target.FuturePosition = value; }
    }
    #endregion Properties

    public void Target_SetLightOrbPosition(Vector2 position)
    {
        _target.LightOrbPosition.transform.localPosition = position;
    }

    public void Target_SetDashArrowActive(bool value)
    {
        _target.DashDirectionIndicator.SetActive(value);
    }

    public void Target_SetDashArrowRotation(Quaternion rotation)
    {
        _target.DashDirectionIndicator.transform.rotation = rotation;
    }

    //public bool Target_HasJumpsLeft()
    //{
    //    return _playerActionParameters.AmountOfJumpsLeft > 0;
    //}

    //public void Target_ResetAmountOfJumpsLeft()
    //{
    //    _playerActionParameters.AmountOfJumpsLeft = _controlData.MaxAmountOfJumps;
    //}

    //public void Target_DecreaseAmountOfJumpsLeft()
    //{
    //    --_playerActionParameters.AmountOfJumpsLeft;
    //}

    //public void Target_ResetAndDecreaseAmountOfJumpsLeft()
    //{
    //    _playerActionParameters.AmountOfJumpsLeft = _controlData.MaxAmountOfJumps - 1;
    //}

    public Vector2 Target_GetDetectedLedgeCornerPosition()
    {
        //RaycastHit2D xHit = Physics2D.Raycast(_sensors.EnvironmentCheck.transform.position,
        //                                        Vector2.right * EntityInternComponents.FacingDirection,
        //                                        _data.EnvironmentCheckDistance,
        //                                        _data.WhatIsGround);

        //float xDistance = xHit.distance;
        //_workspaceVector2.Set(xDistance * EntityInternComponents.FacingDirection, 0f);

        //RaycastHit2D yHit = Physics2D.Raycast(_sensors.LedgeCheck.transform.position + (Vector3)_workspaceVector2,
        //                                        Vector2.down,
        //                                        _sensors.LedgeCheck.transform.position.y - _sensors.EnvironmentCheck.transform.position.y,
        //                                        _data.WhatIsGround);

        //float yDistance = yHit.distance;
        //_workspaceVector2.Set(_sensors.EnvironmentCheck.transform.position.x + xDistance * EntityInternComponents.FacingDirection,
        //                        _sensors.LedgeCheck.transform.position.y - yDistance);

        return _workspaceVector2;
    }

    public void Target_FlipOnInputX(int inputX)
    {
        if (inputX != 0 && inputX != _target.FacingDirection)
            Target_Flip();
    }
}
