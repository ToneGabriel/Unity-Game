using UnityEngine;

[CreateAssetMenu(fileName = "newDodgeStateData", menuName = "Data/State Data/Dodge Data")]
public class Data_Dodge : ScriptableObject
{
    public float DodgeSpeed = 10f;
    public float DodgeTime = 0.2f;
    public float DodgeCooldown = 5f;
    public Vector2 DodgeAngle;

    
}
