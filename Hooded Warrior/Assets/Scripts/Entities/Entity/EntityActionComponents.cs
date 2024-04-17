using UnityEngine;

public struct EntityActionComponents
{
    public Rigidbody2D      Rigidbody;
    public Animator         Animator;
    public BoxCollider2D    BoxCollider;

    public int              FacingDirection;
    public int              LastDamageDirection;
    public float            LastDamageTime;
    public float            CurrentHealth;
    public float            CurrentStunResistance;
    public bool             IsDead;
    public bool             IsStuned;
}
