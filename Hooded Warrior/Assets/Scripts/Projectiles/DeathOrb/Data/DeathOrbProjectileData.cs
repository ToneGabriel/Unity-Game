using UnityEngine;

[CreateAssetMenu(fileName = "newDeathOrbProjectileData", menuName = "Data/Boss1 Data/Death Orb Projectile")]
public class DeathOrbProjectileData : ScriptableObject
{
    public float SpellDamage = 30f;
    public float SpellLifeTime = 5f;
    public float DamageRadius = 0.2f;

    public float SpellSpeed = 5f;
    public float SpellRotateSpeed = 200f;

    public LayerMask WhatIsGround;
    public LayerMask WhatIsPlayer;
}
