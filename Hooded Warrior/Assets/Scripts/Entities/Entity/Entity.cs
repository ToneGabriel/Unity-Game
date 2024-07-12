using System;
using UnityEngine;

public abstract class Entity : MonoBehaviourController//, ISaveable, IDamageble
{
    #region Components & Data
    [SerializeField] private EntitySensors  _sensors;
    [SerializeField] private EntityData     _data;

    public ref readonly EntitySensors   Sensors { get { return ref _sensors; } }
    public EntityData                   BaseData { get { return _data; } }

    public Rigidbody2D                  Rigidbody { get; private set; }
    public Animator                     Animator { get; private set; }
    public BoxCollider2D                BoxCollider { get; private set; }

    public int                          FacingDirection { get; set; }
    public int                          LastDamageDirection { get; set; }
    public float                        LastDamageTime { get; set; }
    public float                        CurrentHealth { get; set; }
    public float                        CurrentStunResistance { get; set; }
    public bool                         IsDead { get; set; }
    public bool                         IsStuned { get; set; }
    #endregion

    #region Unity functions
    protected override void Awake()
    {
        base.Awake();

        //_entityExtObjComponents.HealthBar.SetMaxHealth(_entityData.MaxHealth);

        Rigidbody   = GetComponent<Rigidbody2D>();
        Animator    = GetComponent<Animator>();
        BoxCollider = GetComponent<BoxCollider2D>();

        FacingDirection = 1;
        //CurrentHealth   = _data.MaxHealth;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        //_entityExtObjComponents.HealthBar.SetHealthBar(_entityIntStatusComponents.CurrentHealth);

        IsDead    = false;
        IsStuned  = false;
    }

    protected override void Start()
    {
        base.Start();

        //ObjectPoolManager.Instance.RequestPool<HitParticleController>();
    }

    protected override void Update()
    {
        base.Update();

        //if (Time.time >= _entityIntStatusComponents.LastDamageTime + _entityData.StunRecoveryTime)
        //    ResetStunResistnce();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    #endregion

    #region Damage
    public virtual void Damage(AttackDetails attackDetails)
    {
        if (attackDetails.Position.x < transform.position.x)
            LastDamageDirection = -1;
        else
            LastDamageDirection = 1;

        if (CanTakeDamage())
        {
            AdditionalDamageActions(attackDetails);

            CurrentHealth -= attackDetails.DamageAmount;
            //_entityExtObjComponents.HealthBar.SetHealthBar(_entityIntStatusComponents.CurrentHealth);
            ObjectPoolManager.Instance.GetFromPool<HitParticleController>(transform.position, Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0f, 360f)));

            CheckStatus();
        }
    }

    public virtual bool CanTakeDamage() => true;

    public virtual void AdditionalDamageActions(AttackDetails attackDetails)
    {
        //DamageHop(_dataEntity.DamageHopDirection, _dataEntity.DamageHopSpeed);
        //if (LastDamageDirection != FacingDirection)
        //    Flip();
    }

    public virtual void CheckStatus()
    {
        if (CurrentHealth <= 0)
            IsDead = true;
    }

    public virtual void ResetStunResistnce()
    {
        IsStuned = false;
        //CurrentStunResistance = _EntityData.StunResistance;
    }

    public void DamageHop(Vector2 direction, float velocity)
    {
        //if (EntityInternComponents.Rigidbody.bodyType == RigidbodyType2D.Dynamic)
        //{
        //    direction.Set(direction.x * LastDamageDirection * (-1), direction.y);
        //    EntityInternComponents.Rigidbody.velocity = direction * velocity;
        //}
    }
    #endregion

    #region Save Functions
    public virtual object CaptureState() => null;

    public virtual void RestoreState(ref object state) { }
    #endregion
}
