using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class DataPlayer : ScriptableObject
{
    [Header("Move State")]
    public float MovementVelocity           = 10f;

    [Header("Jump State")]
    public float JumpVelocity               = 15f;
    public int AmountOfJumps                = 2;

    [Header("In Air State")]
    public float JumpHeightMultiplier       = 0.5f;
    public float MaxVelocityY               = 50f;

    [Header("Wall Touching State")]
    public float WallSlideVelocity          = 1f;
    public float WallClimbVelocity          = 3f;
    public float WallJumpVelocity           = 20f;
    public float WallJumpTime               = 0.25f;
    public Vector2 WallJumpAngle            = new Vector2(1, 2);

    [Header("Ledge Climb State")]
    public Vector2 StartOffset              = new Vector2(0.4f, 1f);
    public Vector2 StopOffset               = new Vector2(0.4f, 0.7f);

    [Header("Dash State")]
    public float DashCooldown               = 0.5f;
    public float MaxHoldTime                = 1f;
    public float HoldTimeScale              = 0f;
    public float DashTime                   = 0.2f;
    public float DashVelocity               = 30f;
    public float Drag                       = 10f;
    public float DashEndYMultiplier         = 0.2f;
    public float DistanceBetweenAfterimages = 1f;

    [Header("Crouch State")]
    public float CrouchMovementVelocity     = 5f;
    public float CrouchColliderHeight       = 0.8f;
    public float StandColliderHeight        = 1.6f;
    public Vector2 CrouchLightOrbPosition   = new Vector2(-1f, 0f);
    public Vector2 StandLightOrbPosition    = new Vector2(-1f, 1.5f);

    [Header("Roll State")]
    public float RollVelocity               = 15f;

    [Header("Dead State")]
    public GameObject DeathBloodParticle;
    public GameObject DeathChunkParticle;
}
