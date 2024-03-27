using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/Enemy Data/Base Data")]
public class DataEnemy : ScriptableObject
{
    public float MaxAgroDistance = 4f;
    public float MinAgroDistance = 3f;
    public float CloseRangeActionDistance = 1f;

    public LayerMask WhatIsPlayer;
}
