using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/Enemy Data/Base Data")]
public class EnemyData : ScriptableObject
{
    // Editor ==========================================
    [Header("Basics")]
    [SerializeField] private float _maxAgroDistance          = 4f;
    [SerializeField] private float _minAgroDistance          = 3f;
    [SerializeField] private float _closeRangeActionDistance = 1f;

    [Header("Check variables")]
    [SerializeField] private LayerMask _whatIsPlayer;

    // Getters ==========================================

    public float MaxAgroDistance            { get { return _maxAgroDistance; } }
    public float MinAgroDistance            { get { return _minAgroDistance; } }
    public float CloseRangeActionDistance   { get { return _closeRangeActionDistance; } }

    public LayerMask WhatIsPlayer           { get { return _whatIsPlayer; } }
}
