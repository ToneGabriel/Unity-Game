using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class EntityData : ScriptableObject
{
    // Editor =================================================

    [Header("Basics")]
    [SerializeField] private float _maxHealth                  = 1000f;
    [SerializeField] private float _damageHopSpeed             = 5f;
    [SerializeField] private Vector2 _damageHopDirection       = new Vector2(3f, 2f);
    [SerializeField] private float _stunResistance             = 3f;
    [SerializeField] private float _stunRecoveryTime           = 2f;

    [Header("Check variables")]
    [SerializeField] private float _groundCheckRadius          = 0.3f;
    [SerializeField] private float _environmentCheckDistance   = 0.5f;
    [SerializeField] private float _ledgeCheckRadius           = 0.3f;
    [SerializeField] private LayerMask _whatIsGround;

    // Getters =================================================

    public float MaxHealth                  { get { return _maxHealth; } }
    public float DamageHopSpeed             { get { return _damageHopSpeed; } }
    public Vector2 DamageHopDirection       { get { return _damageHopDirection; } }
    public float StunResistance             { get { return _stunResistance; } }
    public float StunRecoveryTime           { get { return _stunRecoveryTime; } }

    public float GroundCheckRadius          { get { return _groundCheckRadius; } }
    public float EnvironmentCheckDistance   { get { return _environmentCheckDistance; } }
    public float LedgeCheckRadius           { get { return _ledgeCheckRadius; } }
    public LayerMask WhatIsGround           { get { return _whatIsGround; } }
}
