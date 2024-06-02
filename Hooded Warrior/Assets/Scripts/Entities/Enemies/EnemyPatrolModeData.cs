using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyPatrolModeData", menuName = "Data/Enemy Data/State Data/Enemy State Data")]
public class EnemyPatrolModeData : ScriptableObject
{
    // Editor ==========================================

    [Header("Idle State")]
    [SerializeField] private float _minIdleTime                 = 1f;
    [SerializeField] private float _maxIdleTime                 = 2f;

    [Header("Move State")]
    [SerializeField] private float _movementSpeed               = 3f;

    [Header("PlayerDetected State")]
    [SerializeField] private float _lookTime                    = 0.5f;

    [Header("LookForPlayer State")]
    [SerializeField] private int _amountOfTurns                 = 2;
    [SerializeField] private float _timeBetweenTurns            = 0.75f;

    [Header("Stun State")]
    [SerializeField] private float _stunTime                    = 3f;
    [SerializeField] private float _stunKnockBackTime           = 0.2f;
    [SerializeField] private float _stunKnockBackSpeed          = 20f;
    [SerializeField] private Vector2 _stunKnockBackDirection;

    [Header("Dead State")]
    [SerializeField] private GameObject _deathChunkParticle;
    [SerializeField] private GameObject _deathBloodParticle;

    // Getters ==========================================

    public float MinIdleTime                { get { return _minIdleTime; } }
    public float MaxIdleTime                { get { return _maxIdleTime; } }

    public float MovementSpeed              { get { return _movementSpeed; } }

    public float LookTime                   { get { return _lookTime; } }

    public float AmountOfTurns              { get { return _amountOfTurns; } }
    public float TimeBetweenTurns           { get { return _timeBetweenTurns; } }

    public float StunTime                   { get { return _stunTime; } }
    public float StunKnockBackTime          { get { return _stunKnockBackTime; } }
    public float StunKnockBackSpeed         { get { return _stunKnockBackSpeed; } }
    public Vector2 StunKnockBackDirection   { get { return _stunKnockBackDirection; } }

    public GameObject DeathChunkParticle    { get { return _deathChunkParticle; } }
    public GameObject DeathBloodParticle    { get { return _deathBloodParticle; } }
}
