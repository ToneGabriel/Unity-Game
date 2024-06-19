using UnityEngine;

public abstract class Enemy : Entity                // Base Enemy class
{
    public EnemyData EnemyBaseData;

    #region Unity Functions
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        //EntityInternComponents.CurrentStunResistance   = _data.StunResistance;
        //EntityInternComponents.CurrentHealth           = _data.MaxHealth;
        
        base.OnEnable();
    }

    protected override void Update()
    {
        base.Update();

        //EntityInternComponents.Animator.SetFloat("velocityY", EntityInternComponents.Rigidbody.velocity.y);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    #endregion

    #region Damage Functions
    public override void Damage(AttackDetails attackDetails) => base.Damage(attackDetails);   // Called when taking damage (message sent from attacker)

    public override bool CanTakeDamage() => base.CanTakeDamage();

    public override void AdditionalDamageActions(AttackDetails attackDetails)
    {
        base.AdditionalDamageActions(attackDetails);

        //EntityInternComponents.LastDamageTime = Time.time;
        //EntityInternComponents.CurrentStunResistance -= attackDetails.StunDamageAmmount;
    }

    public override void CheckStatus()
    {
        base.CheckStatus();

        //if (EntityInternComponents.CurrentStunResistance <= 0)
        //    EntityInternComponents.IsStuned = true;
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

        //EntityInternComponents.IsDead = data.IsDead;
    }
    #endregion

    #region Other Functions
    public virtual void OnDrawGizmos()
    {
    //    Gizmos.DrawLine(_sensors.EnvironmentCheck.transform.position,
    //                    _sensors.EnvironmentCheck.transform.position + (Vector3)(_data.EnvironmentCheckDistance * EntityInternComponents.FacingDirection * Vector2.right));
    //    Gizmos.DrawLine(_sensors.EnvironmentCheck.transform.position,
    //                    _sensors.EnvironmentCheck.transform.position + (Vector3)(_data.EnvironmentCheckDistance * EntityInternComponents.FacingDirection * Vector2.down));
    //    Gizmos.DrawLine(_sensors.EnvironmentCheck.transform.position,
    //                    _sensors.EnvironmentCheck.transform.position + (Vector3)(EnemyBaseData.CloseRangeActionDistance * EntityInternComponents.FacingDirection * Vector2.right));
    }
    #endregion
}
