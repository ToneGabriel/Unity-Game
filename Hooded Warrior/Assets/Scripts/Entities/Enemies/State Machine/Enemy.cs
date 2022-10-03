using UnityEngine;

public abstract class Enemy : Entity                                                     // Base Enemy class
{
    protected Data_Enemy _dataEnemy;                                            // Reference to base enemy data

    #region Unity Functions
    protected override void Awake()
    {
        _dataEnemy = (Data_Enemy)_dataEntity;

        base.Awake();
    }

    protected override void OnEnable()
    {
        CurrentStunResistance = _dataEnemy.StunResistance;
        CurrentHealth = _dataEnemy.MaxHealth;
        
        base.OnEnable();
    }

    protected override void LogicUpdate()
    {
        base.LogicUpdate();

        StateMachine.CurrentState.LogicUpdate();
        Animator.SetFloat("velocityY", Rigidbody.velocity.y);
    }

    protected override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        StateMachine.CurrentState.PhysicsUpdate();                                      // FixedUpdate for current state
    }
    #endregion

    #region Checkers
    public virtual bool CheckPlayerInMinAgroRange()                                     // Raycast to check agro enter range
    {
        return Physics2D.Raycast(_environmentCheck.transform.position, _environmentCheck.transform.right, _dataEnemy.MinAgroDistance, _dataEnemy.WhatIsPlayer);
    }

    public virtual bool CheckPlayerInMaxAgroRange()                                     // Raycast to check agro exit range
    {
        return Physics2D.Raycast(_environmentCheck.transform.position, _environmentCheck.transform.right, _dataEnemy.MaxAgroDistance, _dataEnemy.WhatIsPlayer);
    }

    public virtual bool CheckPlayerInMeleeRange()                                       // Raycast to check melee range
    {
        return Physics2D.Raycast(_environmentCheck.transform.position, _environmentCheck.transform.right, _dataEnemy.CloseRangeActionDistance, _dataEnemy.WhatIsPlayer);
    }
    #endregion

    #region Damage Functions
    public override void Damage(AttackDetails attackDetails) => base.Damage(attackDetails);   // Called when taking damage (message sent from attacker)

    public override bool CanTakeDamage() => base.CanTakeDamage();

    public override void AdditionalDamageActions(AttackDetails attackDetails)
    {
        base.AdditionalDamageActions(attackDetails);

        LastDamageTime = Time.time;
        CurrentStunResistance -= attackDetails.StunDamageAmmount;
    }

    public override void CheckStatus()
    {
        base.CheckStatus();

        if (CurrentStunResistance <= 0)
            IsStuned = true;
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

        IsDead = data.IsDead;
    }
    #endregion

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(_environmentCheck.transform.position, _environmentCheck.transform.position + (Vector3)(Vector2.right * FacingDirection * _dataEnemy.EnvironmentCheckDistance));
        Gizmos.DrawLine(_environmentCheck.transform.position, _environmentCheck.transform.position + (Vector3)(Vector2.down * FacingDirection * _dataEnemy.EnvironmentCheckDistance));
        Gizmos.DrawLine(_environmentCheck.transform.position, _environmentCheck.transform.position + (Vector3)(Vector2.right * FacingDirection * _dataEnemy.CloseRangeActionDistance));
    }
}
