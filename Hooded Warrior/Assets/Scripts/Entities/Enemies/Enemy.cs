using UnityEngine;

public abstract class Enemy : Entity                // Base Enemy class
{
    [SerializeField] protected EnemyData            _enemyData;
    [SerializeField] protected EnemyPatrolModeData  _enemyPatrolModeData;

    public EnemyPatrolModeData PatrolData { get { return _enemyPatrolModeData; } }


    #region Unity Functions
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        _entityActionComponents.CurrentStunResistance   = _entityData.StunResistance;
        _entityActionComponents.CurrentHealth           = _entityData.MaxHealth;
        
        base.OnEnable();
    }

    protected override void Update()
    {
        base.Update();

        _entityActionComponents.Animator.SetFloat("velocityY", _entityActionComponents.Rigidbody.velocity.y);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    #endregion

    #region Checkers
    public virtual bool CheckPlayerInMinAgroRange()                                     // Raycast to check agro enter range
    {
        return Physics2D.Raycast(   _entityExternComponents.EnvironmentCheck.transform.position,
                                    _entityExternComponents.EnvironmentCheck.transform.right,
                                    _enemyData.MinAgroDistance, _enemyData.WhatIsPlayer);
    }

    public virtual bool CheckPlayerInMaxAgroRange()                                     // Raycast to check agro exit range
    {
        return Physics2D.Raycast(   _entityExternComponents.EnvironmentCheck.transform.position,
                                    _entityExternComponents.EnvironmentCheck.transform.right,
                                    _enemyData.MaxAgroDistance, _enemyData.WhatIsPlayer);
    }

    public virtual bool CheckPlayerInMeleeRange()                                       // Raycast to check melee range
    {
        return Physics2D.Raycast(   _entityExternComponents.EnvironmentCheck.transform.position,
                                    _entityExternComponents.EnvironmentCheck.transform.right,
                                    _enemyData.CloseRangeActionDistance, _enemyData.WhatIsPlayer);
    }
    #endregion

    #region Damage Functions
    public override void Damage(AttackDetails attackDetails) => base.Damage(attackDetails);   // Called when taking damage (message sent from attacker)

    public override bool CanTakeDamage() => base.CanTakeDamage();

    public override void AdditionalDamageActions(AttackDetails attackDetails)
    {
        base.AdditionalDamageActions(attackDetails);

        _entityActionComponents.LastDamageTime = Time.time;
        _entityActionComponents.CurrentStunResistance -= attackDetails.StunDamageAmmount;
    }

    public override void CheckStatus()
    {
        base.CheckStatus();

        if (_entityActionComponents.CurrentStunResistance <= 0)
            _entityActionComponents.IsStuned = true;
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

        _entityActionComponents.IsDead = data.IsDead;
    }
    #endregion

    #region Other Functions
    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(_entityExternComponents.EnvironmentCheck.transform.position,
                        _entityExternComponents.EnvironmentCheck.transform.position + (Vector3)(_entityData.EnvironmentCheckDistance * _entityActionComponents.FacingDirection * Vector2.right));
        Gizmos.DrawLine(_entityExternComponents.EnvironmentCheck.transform.position,
                        _entityExternComponents.EnvironmentCheck.transform.position + (Vector3)(_entityData.EnvironmentCheckDistance * _entityActionComponents.FacingDirection * Vector2.down));
        Gizmos.DrawLine(_entityExternComponents.EnvironmentCheck.transform.position,
                        _entityExternComponents.EnvironmentCheck.transform.position + (Vector3)(_enemyData.CloseRangeActionDistance * _entityActionComponents.FacingDirection * Vector2.right));
    }
    #endregion
}
