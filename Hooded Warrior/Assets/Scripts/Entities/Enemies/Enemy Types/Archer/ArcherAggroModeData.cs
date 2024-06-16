using UnityEngine;

[CreateAssetMenu(fileName = "newArcherAggroModeData", menuName = "Data/Enemy Data/State Data/Archer State Data")]
public class ArcherAggroModeData : ScriptableObject
{
    // Editor ==========================================

    [Header("Dodge State")]
    [SerializeField] private float _dodgeSpeed              = 10f;
    [SerializeField] private float _dodgeTime               = 0.2f;
    [SerializeField] private float _dodgeCooldown           = 5f;
    [SerializeField] private Vector2 _dodgeAngle;

    [Header("MeleeAttack State")]
    [SerializeField] private float _meleeAttackRadius       = 0.5f;
    [SerializeField] private float _meleeAttackDamage       = 10f;
    [SerializeField] private float _meleeAttackCooldown     = 0.6f;
    [SerializeField] private LayerMask _whatIsPlayer;

    [Header("Ranged Attack State")]
    [SerializeField] private float _rangedAttackCooldown    = 5f;

    // Getters ==========================================

    public float DodgeSpeed             { get { return _dodgeSpeed; } }
    public float DodgeTime              { get { return _dodgeTime; } }
    public float DodgeCooldown          { get { return _dodgeCooldown; } }
    public Vector2 DodgeAngle           { get { return _dodgeAngle; } }

    public float MeleeAttackRadius      { get { return _meleeAttackRadius; } }
    public float MeleeAttackDamage      { get { return _meleeAttackDamage; } }
    public float MeleeAttackCooldown    { get { return _meleeAttackCooldown; } }
    public LayerMask WhatIsPlayer       { get { return _whatIsPlayer; } }

    public float RangedAttackCooldown   { get { return _rangedAttackCooldown; } }
}
