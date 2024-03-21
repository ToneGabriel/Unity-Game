using UnityEngine;

public sealed class EntityObjectComponents
{
    [Header("Entity Object Components")]
    public HealthBar        HealthBar;
    public GameObject       GroundCheck;
    public GameObject       EnvironmentCheck;
    public GameObject       LedgeCheck;

    public Rigidbody2D      Rigidbody;
    public Animator         Animator;
    public BoxCollider2D    BoxCollider;

    public Data_Entity      DataEntity;
}
