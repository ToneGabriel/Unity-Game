using UnityEngine;

public abstract class Entity : MonoBehaviour, ISaveable, IDamageble
{
    #region Components & Data
    public Rigidbody2D Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public BoxCollider2D BoxCollider { get; private set; }
    public FiniteStateMachine StateMachine { get; private set; }

    [SerializeField] protected HealthBar _healthBar;
    [SerializeField] protected GameObject _groundCheck;
    [SerializeField] protected GameObject _environmentCheck;
    [SerializeField] protected GameObject _ledgeCheck;
    [SerializeField] protected Data_Entity _dataEntity;
    #endregion

    #region Other Variables
    public int FacingDirection { get; protected set; }
    public int LastDamageDirection { get; protected set; }
    public float LastDamageTime { get; protected set; }
    public float CurrentHealth { get; protected set; }
    public float CurrentStunResistance { get; protected set; }
    public bool IsDead { get; protected set; }
    public bool IsStuned { get; protected set; }
    protected Vector2 _workspaceVector2;
    #endregion

    #region Unity functions
    protected virtual void Awake()
    {
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
        BoxCollider = GetComponent<BoxCollider2D>();
        FacingDirection = 1;
        CurrentHealth = _dataEntity.MaxHealth;
        _healthBar.SetMaxHealth(_dataEntity.MaxHealth);

        InitializeStates();
    }

    protected virtual void OnEnable()
    {
        _healthBar.SetHealthBar(CurrentHealth);
        IsDead = false;
        IsStuned = false;
    }

    protected virtual void Start() { }

    protected virtual void Update()
    {
        if (!GameManager.Instance.IsGamePaused)
            LogicUpdate();
    }

    protected virtual void FixedUpdate()
    {
        if (!GameManager.Instance.IsGamePaused)
            PhysicsUpdate();
    }

    protected virtual void LogicUpdate() 
    {
        if (Time.time >= LastDamageTime + _dataEntity.StunRecoveryTime)
            ResetStunResistnce();
    }

    protected virtual void PhysicsUpdate() { }
    #endregion

    #region Setters
    protected virtual void InitializeStates()
    {
        StateMachine = new FiniteStateMachine();
    }

    public void SetVelocityZero()
    {
        Rigidbody.velocity = Vector2.zero;
    }

    public void SetVelocityX(float velocity)
    {
        _workspaceVector2.Set(velocity, Rigidbody.velocity.y);
        Rigidbody.velocity = _workspaceVector2;
    }

    public void SetVelocityY(float velocity)
    {
        _workspaceVector2.Set(Rigidbody.velocity.x, velocity);
        Rigidbody.velocity = _workspaceVector2;
    }

    public void SetVelocity(float velocity, Vector2 direction)
    {
        _workspaceVector2 = direction * velocity;
        Rigidbody.velocity = _workspaceVector2;
    }

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        _workspaceVector2.Set(angle.x * velocity * direction, angle.y * velocity);
        Rigidbody.velocity = _workspaceVector2;
    }

    public void SetVelocity(float velocity)                                     // Set velocity towards facing direction
    {
        _workspaceVector2.Set(FacingDirection * velocity, Rigidbody.velocity.y);
        Rigidbody.velocity = _workspaceVector2;
    }

    public void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0f, -180f, 0f);
    }
    #endregion

    #region Checkers
    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.transform.position, _dataEntity.GroundCheckRadius, _dataEntity.WhatIsGround);
    }

    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(_environmentCheck.transform.position, transform.right, _dataEntity.EnvironmentCheckDistance, _dataEntity.WhatIsGround);
    }

    public bool CheckIfTouchingLedge(Vector3 direction)
    {
        return Physics2D.Raycast(_ledgeCheck.transform.position, direction, _dataEntity.EnvironmentCheckDistance, _dataEntity.WhatIsGround);
    }

    public bool CheckIfTouchingCeiling()
    {
        return Physics2D.OverlapCircle(_ledgeCheck.transform.position, _dataEntity.GroundCheckRadius, _dataEntity.WhatIsGround);
    }
    #endregion

    #region Damage
    public virtual void Damage(AttackDetails attackDetails) 
    {
        if (attackDetails.Position.x < transform.position.x)
            LastDamageDirection = -1;
        else
            LastDamageDirection = 1;

        if(CanTakeDamage())
        {
            AdditionalDamageActions(attackDetails);

            CurrentHealth -= attackDetails.DamageAmount;
            _healthBar.SetHealthBar(CurrentHealth);
            ObjectPoolManager.Instance.GetFromPool<HitParticleController>(transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

            CheckStatus();
        }
    }

    public virtual bool CanTakeDamage() => true;

    public virtual void AdditionalDamageActions(AttackDetails attackDetails)
    {
        //DamageHop(_dataEntity.DamageHopDirection, _dataEntity.DamageHopSpeed);
        if (LastDamageDirection != FacingDirection)
            Flip();
    }

    public virtual void CheckStatus()
    {
        if (CurrentHealth <= 0)
            IsDead = true;
    }

    public virtual void ResetStunResistnce()
    {
        IsStuned = false;
        CurrentStunResistance = _dataEntity.StunResistance;
    }

    public void DamageHop(Vector2 direction, float velocity)
    {
        if (Rigidbody.bodyType == RigidbodyType2D.Dynamic)
        {
            direction.Set(direction.x * LastDamageDirection * (-1), direction.y);
            Rigidbody.velocity = direction * velocity;
        }
    }
    #endregion

    #region Save Functions
    public virtual object CaptureState() => null;

    public virtual void RestoreState(ref object state) { }
    #endregion
}
