using UnityEngine;

public class Data_Entity : ScriptableObject
{
    [Header("Basics")]
    public float MaxHealth = 1000f;
    public float DamageHopSpeed = 5f;
    public Vector2 DamageHopDirection = new Vector2(3f, 2f);
    public float StunResistance = 3f;
    public float StunRecoveryTime = 2f;

    [Header("Check variables")]
    public float GroundCheckRadius = 0.3f;
    public float EnvironmentCheckDistance = 0.5f;
    public float LedgeCheckRadius = 0.3f;
    public LayerMask WhatIsGround;
}
