using UnityEngine;

[CreateAssetMenu(fileName = "newShieldData", menuName = "Data/Shield Data/Shield")]
public class ShieldData : ScriptableObject
{
    public float ShieldHealth = 30f;
    public float ShieldRegenerationTime = 6f;
    public float ShieldCooldownTime = 3f;
}
