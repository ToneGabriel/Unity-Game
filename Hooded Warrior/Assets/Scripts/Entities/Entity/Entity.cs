using UnityEngine;

public abstract class Entity : MonoBehaviour, ISaveable, IDamageble
{
    #region Components & Data
    [Header("Entity External Components")][SerializeField]
    protected EntityExternalObjectComponents    _entityExtObjComponents;

    [Header("Entity Data")][SerializeField]
    protected DataEntity                        _entityData;

    protected EntityInternalObjectComponents    _entityIntObjComponents;
    protected EntityInternalStatusComponents    _entityIntStatusComponents;
    protected FiniteStateMachine                _stateMachine;      // initialized in derived classes
    protected State[]                           _states;            // initialized in derived classes
    protected Vector2                           _workspaceVector2;
    #endregion

    #region Component Getters
    public EntityInternalStatusComponents       EntityIntStatusComponents   { get { return _entityIntStatusComponents; } }
    #endregion

    #region Getters
    public Vector2 RBVelocity
    {
        get { return _entityIntObjComponents.Rigidbody.velocity; }
        set { _entityIntObjComponents.Rigidbody.velocity = value; }
    }

    public RigidbodyType2D RBBodyType
    {
        get { return _entityIntObjComponents.Rigidbody.bodyType; }
        set { _entityIntObjComponents.Rigidbody.bodyType = value; }
    }

    public float RBDrag
    {
        get { return _entityIntObjComponents.Rigidbody.drag; }
        set { _entityIntObjComponents.Rigidbody.drag = value; }
    }
    #endregion

    #region Unity functions
    protected virtual void Awake()
    {
        _entityExtObjComponents.HealthBar.SetMaxHealth(_entityData.MaxHealth);

        _entityIntObjComponents.Animator            = GetComponent<Animator>();
        _entityIntObjComponents.Rigidbody           = GetComponent<Rigidbody2D>();
        _entityIntObjComponents.BoxCollider         = GetComponent<BoxCollider2D>();

        _entityIntStatusComponents.FacingDirection  = 1;
        _entityIntStatusComponents.CurrentHealth    = _entityData.MaxHealth;
    }

    protected virtual void OnEnable()
    {
        _entityExtObjComponents.HealthBar.SetHealthBar(_entityIntStatusComponents.CurrentHealth);

        _entityIntStatusComponents.IsDead    = false;
        _entityIntStatusComponents.IsStuned  = false;
    }

    protected virtual void Start()
    {
        ObjectPoolManager.Instance.RequestPool<HitParticleController>();
    }

    protected virtual void Update()
    {
        if (GameManager.Instance.IsGamePaused)
            return;

        if (Time.time >= _entityIntStatusComponents.LastDamageTime + _entityData.StunRecoveryTime)
            ResetStunResistnce();

        _stateMachine.CurrentState.LogicUpdate();
    }

    protected virtual void FixedUpdate()
    {
        if (GameManager.Instance.IsGamePaused)
            return;

        _stateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Setters
    public void ChangeState(int stateID)
    {
        _stateMachine.ChangeState(_states[stateID]);
    }

    public void SetAnimatorBoolParam(string animBoolName, bool value)
    {
        _entityIntObjComponents.Animator.SetBool(animBoolName, value);
    }

    public void SetAnimatorFloatParam(string animFloatName, float value)
    {
        _entityIntObjComponents.Animator.SetFloat(animFloatName, value);
    }

    public void SetVelocityZero()
    {
        RBVelocity = Vector2.zero;
    }

    public void SetVelocityX(float velocity)
    {
        _workspaceVector2.Set(velocity, RBVelocity.y);
        RBVelocity = _workspaceVector2;
    }

    public void SetVelocityY(float velocity)
    {
        _workspaceVector2.Set(RBVelocity.x, velocity);
        RBVelocity = _workspaceVector2;
    }

    public void SetVelocity(float velocity, Vector2 direction)
    {
        _workspaceVector2 = direction * velocity;
        RBVelocity = _workspaceVector2;
    }

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        _workspaceVector2.Set(angle.x * velocity * direction, angle.y * velocity);
        RBVelocity = _workspaceVector2;
    }

    public void SetVelocity(float velocity)                                     // Set velocity towards facing direction
    {
        _workspaceVector2.Set(_entityIntStatusComponents.FacingDirection * velocity, RBVelocity.y);
        RBVelocity = _workspaceVector2;
    }

    public void Flip()
    {
        _entityIntStatusComponents.FacingDirection *= -1;
        transform.Rotate(0f, -180f, 0f);
    }
    #endregion

    #region Checkers
    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle( _entityExtObjComponents.GroundCheck.transform.position,
                                        _entityData.GroundCheckRadius,
                                        _entityData.WhatIsGround);
    }

    public bool IsTouchingCeiling()
    {
        return Physics2D.OverlapCircle( _entityExtObjComponents.LedgeCheck.transform.position,
                                        _entityData.GroundCheckRadius,
                                        _entityData.WhatIsGround);
    }

    public bool IsTouchingWall()
    {
        return Physics2D.Raycast(   _entityExtObjComponents.EnvironmentCheck.transform.position,
                                    transform.right,
                                    _entityData.EnvironmentCheckDistance,
                                    _entityData.WhatIsGround);
    }

    public bool IsTouchingLedge(Vector3 direction)
    {
        return Physics2D.Raycast(   _entityExtObjComponents.LedgeCheck.transform.position,
                                    direction,
                                    _entityData.EnvironmentCheckDistance,
                                    _entityData.WhatIsGround);
    }
    #endregion

    #region Damage
    public virtual void Damage(AttackDetails attackDetails) 
    {
        if (attackDetails.Position.x < transform.position.x)
            _entityIntStatusComponents.LastDamageDirection = -1;
        else
            _entityIntStatusComponents.LastDamageDirection = 1;

        if(CanTakeDamage())
        {
            AdditionalDamageActions(attackDetails);

            _entityIntStatusComponents.CurrentHealth -= attackDetails.DamageAmount;
            _entityExtObjComponents.HealthBar.SetHealthBar(_entityIntStatusComponents.CurrentHealth);
            ObjectPoolManager.Instance.GetFromPool<HitParticleController>(transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

            CheckStatus();
        }
    }

    public virtual bool CanTakeDamage() => true;

    public virtual void AdditionalDamageActions(AttackDetails attackDetails)
    {
        //DamageHop(_dataEntity.DamageHopDirection, _dataEntity.DamageHopSpeed);
        if (_entityIntStatusComponents.LastDamageDirection != _entityIntStatusComponents.FacingDirection)
            Flip();
    }

    public virtual void CheckStatus()
    {
        if (_entityIntStatusComponents.CurrentHealth <= 0)
            _entityIntStatusComponents.IsDead = true;
    }

    public virtual void ResetStunResistnce()
    {
        _entityIntStatusComponents.IsStuned = false;
        _entityIntStatusComponents.CurrentStunResistance = _entityData.StunResistance;
    }

    public void DamageHop(Vector2 direction, float velocity)
    {
        if (_entityIntObjComponents.Rigidbody.bodyType == RigidbodyType2D.Dynamic)
        {
            direction.Set(direction.x * _entityIntStatusComponents.LastDamageDirection * (-1), direction.y);
            RBVelocity = direction * velocity;
        }
    }
    #endregion

    #region Save Functions
    public virtual object CaptureState() => null;

    public virtual void RestoreState(ref object state) { }
    #endregion
}
