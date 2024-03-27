using UnityEngine;

public abstract class Enemy : Entity                // Base Enemy class
{
    [Header("Enemy Basics")]
    protected DataEnemy _enemyData;                // Reference to base enemy data

    #region Unity Functions
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        _entityIntStatusComponents.CurrentStunResistance = _entityData.StunResistance;
        _entityIntStatusComponents.CurrentHealth = _entityData.MaxHealth;
        
        base.OnEnable();
    }

    protected override void Update()
    {
        base.Update();

        _entityIntObjComponents.Animator.SetFloat("velocityY", _entityIntObjComponents.Rigidbody.velocity.y);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    #endregion

    #region Checkers
    public virtual bool CheckPlayerInMinAgroRange()                                     // Raycast to check agro enter range
    {
        return Physics2D.Raycast(   _entityExtObjComponents.EnvironmentCheck.transform.position,
                                    _entityExtObjComponents.EnvironmentCheck.transform.right,
                                    _enemyData.MinAgroDistance, _enemyData.WhatIsPlayer);
    }

    public virtual bool CheckPlayerInMaxAgroRange()                                     // Raycast to check agro exit range
    {
        return Physics2D.Raycast(   _entityExtObjComponents.EnvironmentCheck.transform.position,
                                    _entityExtObjComponents.EnvironmentCheck.transform.right,
                                    _enemyData.MaxAgroDistance, _enemyData.WhatIsPlayer);
    }

    public virtual bool CheckPlayerInMeleeRange()                                       // Raycast to check melee range
    {
        return Physics2D.Raycast(   _entityExtObjComponents.EnvironmentCheck.transform.position,
                                    _entityExtObjComponents.EnvironmentCheck.transform.right,
                                    _enemyData.CloseRangeActionDistance, _enemyData.WhatIsPlayer);
    }
    #endregion

    #region Damage Functions
    public override void Damage(AttackDetails attackDetails) => base.Damage(attackDetails);   // Called when taking damage (message sent from attacker)

    public override bool CanTakeDamage() => base.CanTakeDamage();

    public override void AdditionalDamageActions(AttackDetails attackDetails)
    {
        base.AdditionalDamageActions(attackDetails);

        _entityIntStatusComponents.LastDamageTime = Time.time;
        _entityIntStatusComponents.CurrentStunResistance -= attackDetails.StunDamageAmmount;
    }

    public override void CheckStatus()
    {
        base.CheckStatus();

        if (_entityIntStatusComponents.CurrentStunResistance <= 0)
            _entityIntStatusComponents.IsStuned = true;
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

        _entityIntStatusComponents.IsDead = data.IsDead;
    }
    #endregion

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(_entityExtObjComponents.EnvironmentCheck.transform.position,
                        _entityExtObjComponents.EnvironmentCheck.transform.position + (Vector3)(Vector2.right * _entityIntStatusComponents.FacingDirection * _entityData.EnvironmentCheckDistance));
        Gizmos.DrawLine(_entityExtObjComponents.EnvironmentCheck.transform.position,
                        _entityExtObjComponents.EnvironmentCheck.transform.position + (Vector3)(Vector2.down * _entityIntStatusComponents.FacingDirection * _entityData.EnvironmentCheckDistance));
        Gizmos.DrawLine(_entityExtObjComponents.EnvironmentCheck.transform.position,
                        _entityExtObjComponents.EnvironmentCheck.transform.position + (Vector3)(Vector2.right * _entityIntStatusComponents.FacingDirection * _enemyData.CloseRangeActionDistance));
    }
}
