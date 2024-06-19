using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHitModeData : ScriptableObject
{
    // Editor ==========================================================

    [Header("Stun State")]
    [SerializeField] private float _stunTime                    = 3f;
    [SerializeField] private float _stunKnockBackTime           = 0.2f;
    [SerializeField] private float _stunKnockBackSpeed          = 20f;
    [SerializeField] private Vector2 _stunKnockBackDirection;

    [Header("Dead State")]
    [SerializeField] private GameObject _deathChunkParticle;
    [SerializeField] private GameObject _deathBloodParticle;

    // Getters ========================================================

    public float StunTime                   { get { return _stunTime; } }
    public float StunKnockBackTime          { get { return _stunKnockBackTime; } }
    public float StunKnockBackSpeed         { get { return _stunKnockBackSpeed; } }
    public Vector2 StunKnockBackDirection   { get { return _stunKnockBackDirection; } }

    public GameObject DeathChunkParticle    { get { return _deathChunkParticle; } }
    public GameObject DeathBloodParticle    { get { return _deathBloodParticle; } }
}
