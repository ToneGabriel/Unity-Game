using UnityEngine;

[CreateAssetMenu(fileName = "newStunStateData", menuName = "Data/State Data/Stun State")]
public class Data_Stun : ScriptableObject
{
    public float StunTime = 3f;
    public float StunKnockBackTime = 0.2f;
    public float StunKnockBackSpeed = 20f;
    public Vector2 StunKnockBackDirection;
}
