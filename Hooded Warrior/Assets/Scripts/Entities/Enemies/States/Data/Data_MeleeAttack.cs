using UnityEngine;

[CreateAssetMenu(fileName = "newMeleeAttackStateData", menuName = "Data/State Data/Melee Attack State")]
public class Data_MeleeAttack : ScriptableObject
{
    public float AttackRadius = 0.5f;
    public float AttackDamage = 10f;
    public float AttackCooldown = 0.6f;
    public LayerMask WhatIsPlayer;
}
