using UnityEngine;

[CreateAssetMenu(fileName = "newArrowData", menuName = "Data/Arrow Data/Arrow")]
public class ArrowData : ScriptableObject
{
    public float ArrowDamage = 10f;
    public float DamageRadius = 0.2f;
    public float ArrowSpeed = 10f;
    public float ArrowRotateSpeed = 200f;
    public float ArrowMaxTravelDistance = 8f;
    public float ArrowGroundedTime = 5f;

    public LayerMask WhatIsGround;
    public LayerMask WhatIsPlayer;

}
