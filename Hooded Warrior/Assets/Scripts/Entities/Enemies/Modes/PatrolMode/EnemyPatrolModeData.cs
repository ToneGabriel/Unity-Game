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

    [Header("LookForPlayer State")]
    [SerializeField] private int _amountOfTurns                 = 2;
    [SerializeField] private float _timeBetweenTurns            = 0.75f;

    // Getters ==========================================

    public float MinIdleTime                { get { return _minIdleTime; } }
    public float MaxIdleTime                { get { return _maxIdleTime; } }

    public float MovementSpeed              { get { return _movementSpeed; } }

    public float AmountOfTurns              { get { return _amountOfTurns; } }
    public float TimeBetweenTurns           { get { return _timeBetweenTurns; } }
}
