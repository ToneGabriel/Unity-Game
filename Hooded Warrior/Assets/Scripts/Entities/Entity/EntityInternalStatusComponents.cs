using UnityEngine;

public sealed class EntityInternalStatusComponents
{
    public int              FacingDirection;
    public int              LastDamageDirection;
    public float            LastDamageTime;
    public float            CurrentHealth;
    public float            CurrentStunResistance;
    public bool             IsDead;
    public bool             IsStuned;

    public float            Drag;
    public Vector2          Velocity;
    public RigidbodyType2D  RigidbodyType;
}
