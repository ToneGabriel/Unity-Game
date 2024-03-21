using UnityEngine;

public abstract class Entity : MonoBehaviour, ISaveable, IDamageble
{
    #region Components & Data
    [SerializeField] protected EntityObjectComponents   _objectComponents;
    protected EntityStatusComponents                    _statusComponents;
    protected FiniteStateMachine                        _stateMachine;
    protected Vector2                                   _workspaceVector2;
    #endregion

    #region Getters
    public EntityObjectComponents ObjectComponents { get { return _objectComponents; } }
    public EntityStatusComponents StatusComponents { get { return _statusComponents; } }
    #endregion

    #region Unity functions
    protected virtual void Awake()
    {
        _objectComponents.Animator          = GetComponent<Animator>();
        _objectComponents.Rigidbody         = GetComponent<Rigidbody2D>();
        _objectComponents.BoxCollider       = GetComponent<BoxCollider2D>();
        _objectComponents.HealthBar.SetMaxHealth(_objectComponents.DataEntity.MaxHealth);
        _statusComponents.FacingDirection   = 1;
        _statusComponents.CurrentHealth     = _objectComponents.DataEntity.MaxHealth;
    }

    protected virtual void OnEnable()
    {
        _objectComponents.HealthBar.SetHealthBar(_statusComponents.CurrentHealth);
        _statusComponents.IsDead    = false;
        _statusComponents.IsStuned  = false;
    }

    protected virtual void Start()
    {
        ObjectPoolManager.Instance.RequestPool<HitParticleController>();
    }

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
        if (Time.time >= _statusComponents.LastDamageTime + _objectComponents.DataEntity.StunRecoveryTime)
            ResetStunResistnce();
    }

    protected virtual void PhysicsUpdate() { }
    #endregion

    #region Setters
    public void ChangeState(int stateID)
    {
        _stateMachine.ChangeState(stateID);
    }

    public void SetVelocityZero()
    {
        _objectComponents.Rigidbody.velocity = Vector2.zero;
    }

    public void SetVelocityX(float velocity)
    {
        _workspaceVector2.Set(velocity, _objectComponents.Rigidbody.velocity.y);
        _objectComponents.Rigidbody.velocity = _workspaceVector2;
    }

    public void SetVelocityY(float velocity)
    {
        _workspaceVector2.Set(_objectComponents.Rigidbody.velocity.x, velocity);
        _objectComponents.Rigidbody.velocity = _workspaceVector2;
    }

    public void SetVelocity(float velocity, Vector2 direction)
    {
        _workspaceVector2 = direction * velocity;
        _objectComponents.Rigidbody.velocity = _workspaceVector2;
    }

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        _workspaceVector2.Set(angle.x * velocity * direction, angle.y * velocity);
        _objectComponents.Rigidbody.velocity = _workspaceVector2;
    }

    public void SetVelocity(float velocity)                                     // Set velocity towards facing direction
    {
        _workspaceVector2.Set(_statusComponents.FacingDirection * velocity, _objectComponents.Rigidbody.velocity.y);
        _objectComponents.Rigidbody.velocity = _workspaceVector2;
    }

    public void Flip()
    {
        _statusComponents.FacingDirection *= -1;
        transform.Rotate(0f, -180f, 0f);
    }
    #endregion

    #region Checkers
    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(_objectComponents.GroundCheck.transform.position, _objectComponents.DataEntity.GroundCheckRadius, _objectComponents.DataEntity.WhatIsGround);
    }

    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(_objectComponents.EnvironmentCheck.transform.position, transform.right, _objectComponents.DataEntity.EnvironmentCheckDistance, _objectComponents.DataEntity.WhatIsGround);
    }

    public bool CheckIfTouchingLedge(Vector3 direction)
    {
        return Physics2D.Raycast(_objectComponents.LedgeCheck.transform.position, direction, _objectComponents.DataEntity.EnvironmentCheckDistance, _objectComponents.DataEntity.WhatIsGround);
    }

    public bool CheckIfTouchingCeiling()
    {
        return Physics2D.OverlapCircle(_objectComponents.LedgeCheck.transform.position, _objectComponents.DataEntity.GroundCheckRadius, _objectComponents.DataEntity.WhatIsGround);
    }
    #endregion

    #region Damage
    public virtual void Damage(AttackDetails attackDetails) 
    {
        if (attackDetails.Position.x < transform.position.x)
            _statusComponents.LastDamageDirection = -1;
        else
            _statusComponents.LastDamageDirection = 1;

        if(CanTakeDamage())
        {
            AdditionalDamageActions(attackDetails);

            _statusComponents.CurrentHealth -= attackDetails.DamageAmount;
            _objectComponents.HealthBar.SetHealthBar(_statusComponents.CurrentHealth);
            ObjectPoolManager.Instance.GetFromPool<HitParticleController>(transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

            CheckStatus();
        }
    }

    public virtual bool CanTakeDamage() => true;

    public virtual void AdditionalDamageActions(AttackDetails attackDetails)
    {
        //DamageHop(_dataEntity.DamageHopDirection, _dataEntity.DamageHopSpeed);
        if (_statusComponents.LastDamageDirection != _statusComponents.FacingDirection)
            Flip();
    }

    public virtual void CheckStatus()
    {
        if (_statusComponents.CurrentHealth <= 0)
            _statusComponents.IsDead = true;
    }

    public virtual void ResetStunResistnce()
    {
        _statusComponents.IsStuned = false;
        _statusComponents.CurrentStunResistance = _objectComponents.DataEntity.StunResistance;
    }

    public void DamageHop(Vector2 direction, float velocity)
    {
        if (_objectComponents.Rigidbody.bodyType == RigidbodyType2D.Dynamic)
        {
            direction.Set(direction.x * _statusComponents.LastDamageDirection * (-1), direction.y);
            _objectComponents.Rigidbody.velocity = direction * velocity;
        }
    }
    #endregion

    #region Save Functions
    public virtual object CaptureState() => null;

    public virtual void RestoreState(ref object state) { }
    #endregion
}
