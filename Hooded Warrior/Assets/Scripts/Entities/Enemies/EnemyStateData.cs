using UnityEngine;

public class EnemyStateData : ScriptableObject
{
    // Editor ==========================================

    [Header("Idle State")]
    public float MinIdleTime = 1f;
    public float MaxIdleTime = 2f;

    [Header("Move State")]
    public float MovementSpeed = 3f;

    [Header("PlayerDetected State")]
    public float LookTime = 0.5f;

    [Header("LookForPlayer State")]
    public int AmountOfTurns = 2;
    public float TimeBetweenTurns = 0.75f;

    [Header("Stun State")]
    public float StunTime = 3f;
    public float StunKnockBackTime = 0.2f;
    public float StunKnockBackSpeed = 20f;
    public Vector2 StunKnockBackDirection;

    [Header("Dead State")]
    public GameObject DeathChunkParticle;
    public GameObject DeathBloodParticle;

    // Getters ==========================================

}
