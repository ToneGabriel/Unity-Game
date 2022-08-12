using UnityEngine;

[CreateAssetMenu(fileName = "newDeathOrbData", menuName = "Data/Boss1 Data/Death Orb")]
public class DeathOrbData : ScriptableObject
{
    public float LifeTime = 5f;
    public float DetectionRadius = 10f;

    public float MaxScale = 3f;
    public float ScaleUnit = 0.1f;
    public WaitForSeconds TimeToScale = new WaitForSeconds(0.01f);

    public float TimeBeforeAttack = 1f;
    public int MaxNumberOfProjectiles = 3;
    public float MinCastAngleZ = 30f;
    public float MaxCastAngleZ = 160f;

    public LayerMask WhatIsPlayer;
}
