using System;

[Serializable]
public struct EnemySaveData
{
    public bool IsDead;

    public EnemySaveData(Enemy enemy)
    {
        IsDead = enemy.IsDead;
    }
}
