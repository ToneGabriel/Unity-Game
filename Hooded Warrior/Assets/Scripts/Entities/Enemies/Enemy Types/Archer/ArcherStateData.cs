using UnityEngine;

[CreateAssetMenu(fileName = "newArcherStateData", menuName = "Data/Enemy Data/State Data/Archer State Data")]
public class ArcherStateData : ScriptableObject
{
    // Editor ==========================================

    [Header("Dodge State")]
    public float DodgeSpeed = 10f;
    public float DodgeTime = 0.2f;
    public float DodgeCooldown = 5f;
    public Vector2 DodgeAngle;

    [Header("MeleeAttack State")]
    public float MeleeAttackRadius = 0.5f;
    public float MeleeAttackDamage = 10f;
    public float MeleeAttackCooldown = 0.6f;
    public LayerMask WhatIsPlayer;

    [Header("Ranged Attack State")]
    public float RangedAttackCooldown = 5f;

    // Getters ==========================================


}
