using UnityEngine;

[CreateAssetMenu(fileName ="newWeaponData",menuName ="Data/Weapon Data/Weapon")]
public class WeaponData : ScriptableObject
{
    public float[] MovementSpeed;
    public float AttackRadius = 3f;
    public float AttackResetTime = 0.5f;
    public float Damage;
    public float StunDamage;

    public LayerMask WhatIsEnemy;
}
