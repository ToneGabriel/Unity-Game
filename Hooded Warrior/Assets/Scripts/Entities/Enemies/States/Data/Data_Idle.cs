using UnityEngine;

[CreateAssetMenu(fileName = "newIdleStateData", menuName = "Data/State Data/Idle State")]
public class Data_Idle : ScriptableObject
{
    public float MinIdleTime = 1f;
    public float MaxIdleTime = 2f;
}
