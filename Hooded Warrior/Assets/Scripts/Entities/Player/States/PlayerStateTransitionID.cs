
public enum PlayerStateTransitionID
{
    // base state transitions
    GroundedToJump,
    GroundedToInAir,
    GroundedToWallGrab,
    GroundedToDash,

    AbilityToCrouchIdle,
    AbilityToIdle,
    AbilityToInAir,

    TouchingWallToIdle,
    TouchingWallToInAir,
    TouchingWallToLedgeClimb,

    // other state transitions
    IdleToMove,
    IdleToCrouchIdle,

    MoveToIdle,
    MoveToCrouchMove,
    MoveToRoll,

    InAirToLand,
    InAirToLedgeClimb,
    InAirToJump,
    InAirToWallGrab,
    InAirToWallSlide,
    InAirToDash,

    LandToIdle,
    LandToMove,

    WallSlideToWallGrab,
    WallSlideToWallJump,
    WallSlideToInAir,

    WallGrabToWallClimb,
    WallGrabToWallSlide,

    WallClimbToWallGrab,

    LedgeClimbToWallSlide,
    LedgeClimbToWallJump,

    CrouchIdleToCrouchMove,
    CrouchIdleToIdle,

    CrouchMoveToCrouchIdle,
    CrouchMoveToMove,

    Count
}
