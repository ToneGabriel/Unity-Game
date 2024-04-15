using System;
using UnityEngine;

public abstract class Entity : FSMMonoBehaviour, ISaveable, IDamageble
{
    #region Components & Data
    [SerializeField]
    protected EntityExternalObjectComponents    _entityExtObjComponents;
    protected EntityInternalObjectComponents    _entityIntObjComponents;
    protected EntityInternalStatusComponents    _entityIntStatusComponents;

    [SerializeField]
    protected EntityData                        _entityData;
    protected Vector2                           _workspaceVector2;
    #endregion

    #region Component Getters & Setters
    public float                                VelocityX       { get { return _entityIntObjComponents.Rigidbody.velocity.x; } }
    public float                                VelocityY       { get { return _entityIntObjComponents.Rigidbody.velocity.y; } }
    public float                                Drag            { set { _entityIntObjComponents.Rigidbody.drag = value; } }
    public RigidbodyType2D                      RigidbodyType   { set { _entityIntObjComponents.Rigidbody.bodyType = value; } }
    public EntityInternalStatusComponents       GeneralStatus
    {
        get
        {
            GetGeneralStatus(out var ret);
            return ret;
        }
    }

    private EntityInternalStatusComponents GetGeneralStatus(out EntityInternalStatusComponents val)
    {
        val = _entityIntStatusComponents;
        return val;
    }
    #endregion

    #region Unity functions
    protected override void Awake()
    {
        base.Awake();

        //_entityExtObjComponents.HealthBar.SetMaxHealth(_entityData.MaxHealth);

        _entityIntObjComponents.Rigidbody           = GetComponent<Rigidbody2D>();
        _entityIntObjComponents.Animator            = GetComponent<Animator>();
        _entityIntObjComponents.BoxCollider         = GetComponent<BoxCollider2D>();

        _entityIntStatusComponents.FacingDirection  = 1;
        _entityIntStatusComponents.CurrentHealth    = _entityData.MaxHealth;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        //_entityExtObjComponents.HealthBar.SetHealthBar(_entityIntStatusComponents.CurrentHealth);

        _entityIntStatusComponents.IsDead    = false;
        _entityIntStatusComponents.IsStuned  = false;
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
        base.Update();
    }
    #endregion

    #region Setters
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
        _entityIntObjComponents.Rigidbody.velocity = Vector2.zero;
    }

    public void SetVelocityX(float velocityX)
    {
        //_workspaceVector2.Set(velocity, VelocityY);
        //_entityIntObjComponents.Rigidbody.velocity = _workspaceVector2;

        _entityIntObjComponents.Rigidbody.velocity.Set(velocityX, VelocityY);
    }

    public void SetVelocityY(float velocityY)
    {
        //_workspaceVector2.Set(VelocityX, velocity);
        //_entityIntObjComponents.Rigidbody.velocity = _workspaceVector2;

        _entityIntObjComponents.Rigidbody.velocity.Set(VelocityX, velocityY);
    }

    public void SetVelocity(float velocityX, float velocityY)
    {
        //_workspaceVector2.Set(x, y);
        //_entityIntObjComponents.Rigidbody.velocity = _workspaceVector2;

        _entityIntObjComponents.Rigidbody.velocity.Set(velocityX, velocityY);
    }

    public void SetVelocity(Vector2 velocity)
    {
        _entityIntObjComponents.Rigidbody.velocity = velocity;
    }

    public void SetVelocity(float velocity, Vector2 direction)
    {
        //_workspaceVector2 = direction * velocity;
        //_entityIntObjComponents.Rigidbody.velocity = _workspaceVector2;

        _entityIntObjComponents.Rigidbody.velocity = direction * velocity;
    }

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        //_workspaceVector2.Set(angle.x * velocity * direction, angle.y * velocity);
        //_entityIntObjComponents.Rigidbody.velocity = _workspaceVector2;

        _entityIntObjComponents.Rigidbody.velocity.Set(angle.x * velocity * direction, angle.y * velocity);
    }

    public void SetVelocity(float velocity)                                     // Set velocity towards facing direction
    {
        //_workspaceVector2.Set(_entityIntStatusComponents.FacingDirection * velocity, VelocityY);
        //_entityIntObjComponents.Rigidbody.velocity = _workspaceVector2;

        _entityIntObjComponents.Rigidbody.velocity.Set(_entityIntStatusComponents.FacingDirection * velocity, VelocityY);
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
            //_entityExtObjComponents.HealthBar.SetHealthBar(_entityIntStatusComponents.CurrentHealth);
            ObjectPoolManager.Instance.GetFromPool<HitParticleController>(transform.position, Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0f, 360f)));

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
            _entityIntObjComponents.Rigidbody.velocity = direction * velocity;
        }
    }
    #endregion

    #region Save Functions
    public virtual object CaptureState() => null;

    public virtual void RestoreState(ref object state) { }
    #endregion
}
