using UnityEngine;

[CreateAssetMenu(fileName = "newOrbSpellData", menuName = "Data/Spell Data/Orb Spell")]
public class OrbSpellData : ScriptableObject
{
    public float SpellDamage = 10f;
    public float DamageRadius = 0.2f;
    public float DetectionRadius = 10f;

    public float SpellSpeed = 10f;
    public float SpellRotateSpeed = 200f;
    public float SpellLifeTime = 5f;

    public LayerMask WhatIsGround;
    public LayerMask WhatIsEnemy;
}
