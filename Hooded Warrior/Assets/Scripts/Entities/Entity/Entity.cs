using System;
using UnityEngine;

public abstract class Entity : FSMMonoBehaviour, ISaveable, IDamageble
{
    #region Components & Data
    [SerializeField] protected EntityExternComponents   _entityExternComponents;
    [SerializeField] protected EntityData               _entityData;

    protected EntityActionComponents                    _entityActionComponents;
    protected Vector2                                   _workspaceVector2;
    #endregion

    #region Component Getters & Setters
    public float            VelocityX       { get { return _entityActionComponents.Rigidbody.velocity.x; } }
    public float            VelocityY       { get { return _entityActionComponents.Rigidbody.velocity.y; } }
    public int              FacingDirection { get { return _entityActionComponents.FacingDirection; } }
    public bool             IsDead          { get { return _entityActionComponents.IsDead; } }
    public float            Drag            { set { _entityActionComponents.Rigidbody.drag = value; } }
    public RigidbodyType2D  RigidbodyType   { set { _entityActionComponents.Rigidbody.bodyType = value; } }
    #endregion

    #region Unity functions
    protected override void Awake()
    {
        base.Awake();

        //_entityExtObjComponents.HealthBar.SetMaxHealth(_entityData.MaxHealth);

        _entityActionComponents.Rigidbody           = GetComponent<Rigidbody2D>();
        _entityActionComponents.Animator            = GetComponent<Animator>();
        _entityActionComponents.BoxCollider         = GetComponent<BoxCollider2D>();
        _entityActionComponents.FacingDirection     = 1;
        _entityActionComponents.CurrentHealth       = _entityData.MaxHealth;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        //_entityExtObjComponents.HealthBar.SetHealthBar(_entityIntStatusComponents.CurrentHealth);

        _entityActionComponents.IsDead    = false;
        _entityActionComponents.IsStuned  = false;
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
        _entityActionComponents.Animator.SetBool(animBoolName, value);
    }

    public void SetAnimatorFloatParam(string animFloatName, float value)
    {
        _entityActionComponents.Animator.SetFloat(animFloatName, value);
    }

    public void SetVelocityZero()
    {
        _entityActionComponents.Rigidbody.velocity = Vector2.zero;
    }

    public void SetVelocityX(float velocityX)
    {
        _workspaceVector2.Set(velocityX, VelocityY);
        _entityActionComponents.Rigidbody.velocity = _workspaceVector2;
    }

    public void SetVelocityY(float velocityY)
    {
        _workspaceVector2.Set(VelocityX, velocityY);
        _entityActionComponents.Rigidbody.velocity = _workspaceVector2;
    }

    public void SetVelocity(float velocityX, float velocityY)
    {
        _workspaceVector2.Set(velocityX, velocityY);
        _entityActionComponents.Rigidbody.velocity = _workspaceVector2;
    }

    public void SetVelocity(Vector2 velocity)
    {
        _entityActionComponents.Rigidbody.velocity = velocity;
    }

    public void SetVelocity(float velocity, Vector2 direction)
    {
        _workspaceVector2 = direction * velocity;
        _entityActionComponents.Rigidbody.velocity = _workspaceVector2;
    }

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        _workspaceVector2.Set(angle.x * velocity * direction, angle.y * velocity);
        _entityActionComponents.Rigidbody.velocity = _workspaceVector2;
    }

    public void SetVelocity(float velocity)     // Set velocity towards facing direction
    {
        _workspaceVector2.Set(_entityActionComponents.FacingDirection * velocity, VelocityY);
        _entityActionComponents.Rigidbody.velocity = _workspaceVector2;
    }

    public void Flip()
    {
        _entityActionComponents.FacingDirection *= -1;
        transform.Rotate(0f, -180f, 0f);
    }
    #endregion

    #region Checkers
    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle( _entityExternComponents.GroundCheck.transform.position,
                                        _entityData.GroundCheckRadius,
                                        _entityData.WhatIsGround);
    }

    public bool IsTouchingCeiling()
    {
        return Physics2D.OverlapCircle( _entityExternComponents.LedgeCheck.transform.position,
                                        _entityData.GroundCheckRadius,
                                        _entityData.WhatIsGround);
    }

    public bool IsTouchingWall()
    {
        return Physics2D.Raycast(   _entityExternComponents.EnvironmentCheck.transform.position,
                                    transform.right,
                                    _entityData.EnvironmentCheckDistance,
                                    _entityData.WhatIsGround);
    }

    public bool IsTouchingLedge(Vector3 direction)
    {
        return Physics2D.Raycast(   _entityExternComponents.LedgeCheck.transform.position,
                                    direction,
                                    _entityData.EnvironmentCheckDistance,
                                    _entityData.WhatIsGround);
    }
    #endregion

    #region Damage
    public virtual void Damage(AttackDetails attackDetails) 
    {
        if (attackDetails.Position.x < transform.position.x)
            _entityActionComponents.LastDamageDirection = -1;
        else
            _entityActionComponents.LastDamageDirection = 1;

        if(CanTakeDamage())
        {
            AdditionalDamageActions(attackDetails);

            _entityActionComponents.CurrentHealth -= attackDetails.DamageAmount;
            //_entityExtObjComponents.HealthBar.SetHealthBar(_entityIntStatusComponents.CurrentHealth);
            ObjectPoolManager.Instance.GetFromPool<HitParticleController>(transform.position, Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0f, 360f)));

            CheckStatus();
        }
    }

    public virtual bool CanTakeDamage() => true;

    public virtual void AdditionalDamageActions(AttackDetails attackDetails)
    {
        //DamageHop(_dataEntity.DamageHopDirection, _dataEntity.DamageHopSpeed);
        if (_entityActionComponents.LastDamageDirection != _entityActionComponents.FacingDirection)
            Flip();
    }

    public virtual void CheckStatus()
    {
        if (_entityActionComponents.CurrentHealth <= 0)
            _entityActionComponents.IsDead = true;
    }

    public virtual void ResetStunResistnce()
    {
        _entityActionComponents.IsStuned = false;
        _entityActionComponents.CurrentStunResistance = _entityData.StunResistance;
    }

    public void DamageHop(Vector2 direction, float velocity)
    {
        if (_entityActionComponents.Rigidbody.bodyType == RigidbodyType2D.Dynamic)
        {
            direction.Set(direction.x * _entityActionComponents.LastDamageDirection * (-1), direction.y);
            _entityActionComponents.Rigidbody.velocity = direction * velocity;
        }
    }
    #endregion

    #region Save Functions
    public virtual object CaptureState() => null;

    public virtual void RestoreState(ref object state) { }
    #endregion
}
