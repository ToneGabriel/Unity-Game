using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/Enemy Data/Base Data")]
public class Data_Enemy : DataEntity
{
    [Header("Enemy Basics")]
    public float MaxAgroDistance = 4f;
    public float MinAgroDistance = 3f;
    public float CloseRangeActionDistance = 1f;

    public LayerMask WhatIsPlayer;
}
