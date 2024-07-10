using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlModeData : ScriptableObject
{
    // Editor ==========================================

    [Header("Move State")]
    [SerializeField] private float _movementVelocity = 10f;

    [Header("Jump State")]
    [SerializeField] private float _jumpVelocity = 15f;
    [SerializeField] private int _maxAmountOfJumps = 2;

    [Header("In Air State")]
    [SerializeField] private float _jumpHeightMultiplier = 0.5f;
    [SerializeField] private float _maxVelocityY = 50f;

    [Header("Wall Touching State")]
    [SerializeField] private float _wallSlideVelocity = 1f;
    [SerializeField] private float _wallClimbVelocity = 3f;
    [SerializeField] private float _wallJumpVelocity = 20f;
    [SerializeField] private float _wallJumpTime = 0.25f;
    [SerializeField] private Vector2 _wallJumpAngle = new Vector2(1, 2);

    [Header("Ledge Climb State")]
    [SerializeField] private Vector2 _startOffset = new Vector2(0.4f, 1f);
    [SerializeField] private Vector2 _stopOffset = new Vector2(0.4f, 0.7f);

    [Header("Dash State")]
    [SerializeField] private float _dashCooldown = 0.5f;
    [SerializeField] private float _maxHoldTime = 1f;
    [SerializeField] private float _dashTime = 0.2f;
    [SerializeField] private float _dashVelocity = 30f;
    [SerializeField] private float _dashDrag = 10f;
    [SerializeField] private float _dashEndYMultiplier = 0.2f;
    [SerializeField] private float _distanceBetweenAfterimages = 1f;

    [Header("Crouch State")]
    [SerializeField] private float _crouchMovementVelocity = 5f;
    [SerializeField] private float _crouchColliderHeight = 0.8f;
    [SerializeField] private float _standColliderHeight = 1.6f;
    [SerializeField] private Vector2 _crouchLightOrbPosition = new Vector2(-1f, 0f);
    [SerializeField] private Vector2 _standLightOrbPosition = new Vector2(-1f, 1.5f);

    [Header("Roll State")]
    [SerializeField] private float _rollVelocity = 15f;

    [Header("Dead State")]
    [SerializeField] private GameObject _deathBloodParticle;
    [SerializeField] private GameObject _deathChunkParticle;


    // Getters ==========================================

    public float MovementVelocity { get { return _movementVelocity; } }

    public float JumpVelocity { get { return _jumpVelocity; } }
    public int MaxAmountOfJumps { get { return _maxAmountOfJumps; } }

    public float JumpHeightMultiplier { get { return _jumpHeightMultiplier; } }
    public float MaxVelocityY { get { return _maxVelocityY; } }

    public float WallSlideVelocity { get { return _wallSlideVelocity; } }
    public float WallClimbVelocity { get { return _wallClimbVelocity; } }
    public float WallJumpVelocity { get { return _wallJumpVelocity; } }
    public float WallJumpTime { get { return _wallJumpTime; } }
    public Vector2 WallJumpAngle { get { return _wallJumpAngle; } }

    public Vector2 StartOffset { get { return _startOffset; } }
    public Vector2 StopOffset { get { return _stopOffset; } }

    public float DashCooldown { get { return _dashCooldown; } }
    public float MaxHoldTime { get { return _maxHoldTime; } }
    public float DashTime { get { return _dashTime; } }
    public float DashVelocity { get { return _dashVelocity; } }
    public float DashDrag { get { return _dashDrag; } }
    public float DashEndYMultiplier { get { return _dashEndYMultiplier; } }
    public float DistanceBetweenAfterimages { get { return _distanceBetweenAfterimages; } }

    public float CrouchMovementVelocity { get { return _crouchMovementVelocity; } }
    public float CrouchColliderHeight { get { return _crouchColliderHeight; } }
    public float StandColliderHeight { get { return _standColliderHeight; } }
    public Vector2 CrouchLightOrbPosition { get { return _crouchLightOrbPosition; } }
    public Vector2 StandLightOrbPosition { get { return _standLightOrbPosition; } }

    public float RollVelocity { get { return _rollVelocity; } }

    public GameObject DeathBloodParticle { get { return _deathBloodParticle; } }
    public GameObject DeathChunkParticle { get { return _deathChunkParticle; } }
}
