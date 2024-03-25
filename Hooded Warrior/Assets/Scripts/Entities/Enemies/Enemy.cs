using UnityEngine;

public abstract class Enemy : Entity                // Base Enemy class
{
    protected Data_Enemy _dataEnemy;                // Reference to base enemy data

    #region Unity Functions
    protected override void Awake()
    {
        _dataEnemy = (Data_Enemy)_dataEntity;

        base.Awake();
    }

    protected override void OnEnable()
    {
        _statusComponents.CurrentStunResistance = _dataEnemy.StunResistance;
        _statusComponents.CurrentHealth = _dataEnemy.MaxHealth;
        
        base.OnEnable();
    }

    protected override void Update()
    {
        base.Update();

        _objectComponents.Animator.SetFloat("velocityY", _objectComponents.Rigidbody.velocity.y);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    #endregion

    #region Checkers
    public virtual bool CheckPlayerInMinAgroRange()                                     // Raycast to check agro enter range
    {
        return Physics2D.Raycast(   _objectComponents.EnvironmentCheck.transform.position,
                                    _objectComponents.EnvironmentCheck.transform.right,
                                    _dataEnemy.MinAgroDistance, _dataEnemy.WhatIsPlayer);
    }

    public virtual bool CheckPlayerInMaxAgroRange()                                     // Raycast to check agro exit range
    {
        return Physics2D.Raycast(   _objectComponents.EnvironmentCheck.transform.position,
                                    _objectComponents.EnvironmentCheck.transform.right,
                                    _dataEnemy.MaxAgroDistance, _dataEnemy.WhatIsPlayer);
    }

    public virtual bool CheckPlayerInMeleeRange()                                       // Raycast to check melee range
    {
        return Physics2D.Raycast(   _objectComponents.EnvironmentCheck.transform.position,
                                    _objectComponents.EnvironmentCheck.transform.right,
                                    _dataEnemy.CloseRangeActionDistance, _dataEnemy.WhatIsPlayer);
    }
    #endregion

    #region Damage Functions
    public override void Damage(AttackDetails attackDetails) => base.Damage(attackDetails);   // Called when taking damage (message sent from attacker)

    public override bool CanTakeDamage() => base.CanTakeDamage();

    public override void AdditionalDamageActions(AttackDetails attackDetails)
    {
        base.AdditionalDamageActions(attackDetails);

        _statusComponents.LastDamageTime = Time.time;
        _statusComponents.CurrentStunResistance -= attackDetails.StunDamageAmmount;
    }

    public override void CheckStatus()
    {
        base.CheckStatus();

        if (_statusComponents.CurrentStunResistance <= 0)
            _statusComponents.IsStuned = true;
    }
    #endregion

    #region Save Functions
    public override object CaptureState()
    {
        return new EnemySaveData(this);
    }

    public override void RestoreState(ref object state)
    {
        var data = (EnemySaveData)state;

        _statusComponents.IsDead = data.IsDead;
    }
    #endregion

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(_objectComponents.EnvironmentCheck.transform.position,
                        _objectComponents.EnvironmentCheck.transform.position + (Vector3)(Vector2.right * _statusComponents.FacingDirection * _dataEnemy.EnvironmentCheckDistance));
        Gizmos.DrawLine(_objectComponents.EnvironmentCheck.transform.position,
                        _objectComponents.EnvironmentCheck.transform.position + (Vector3)(Vector2.down * _statusComponents.FacingDirection * _dataEnemy.EnvironmentCheckDistance));
        Gizmos.DrawLine(_objectComponents.EnvironmentCheck.transform.position,
                        _objectComponents.EnvironmentCheck.transform.position + (Vector3)(Vector2.right * _statusComponents.FacingDirection * _dataEnemy.CloseRangeActionDistance));
    }
}
