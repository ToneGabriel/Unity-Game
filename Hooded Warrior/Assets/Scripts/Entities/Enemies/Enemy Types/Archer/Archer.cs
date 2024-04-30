using UnityEngine;

public sealed class Archer : Enemy
{
    #region States and Data
    [SerializeField] private ArcherStateData _archerStateData;
    #endregion

    #region Components
    public GameObject MeleeAttackPosition;
    public GameObject RangedAttackPosition;
    #endregion

    #region Unity Functions
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Start()
    {
        base.Start();

        ObjectPoolManager.Instance.RequestPool<Arrow>();
    }
    #endregion

    #region Triggers
    public void TriggerMeleeAttack()
    {
        //MeleeAttackState.TriggerMeleeAttack();
    }

    public void TriggerRangedAttack()
    {
        //RangedAttackState.TriggerRangedAttack();
    }

    public void FinishMeleeAttack()
    {
        //MeleeAttackState.FinishMeleeAttack();
    }

    public void FinishRangedAttack()
    {
        //RangedAttackState.FinishRangedAttack();
    }

    public void FinishDeathAnimation()
    {
        //DeadState.FinishDeathAnimation();
    }
    #endregion

    #region Other Functions
    public override void Damage(AttackDetails attackdetails)    // Called when taking damage (message sent from attacker)
    {
        base.Damage(attackdetails);

        if (_entityActionComponents.IsDead)
            ChangeState((int)ArcherStateID.Dead);
        else if (_entityActionComponents.IsStuned && !IsStateActive((int)ArcherStateID.Stun))
            ChangeState((int)ArcherStateID.Stun);
        else if (!_entityActionComponents.IsStuned && _entityActionComponents.Rigidbody.velocity.x != 0)
            ChangeState((int)ArcherStateID.LookForPlayer);
        else if (!_entityActionComponents.IsStuned && CheckPlayerInMinAgroRange())
            ChangeState((int)ArcherStateID.RangedAttack);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(MeleeAttackPosition.transform.position, _archerStateData.MeleeAttackRadius);
    }

    protected override void FSMInitializeStates()
    {
        AddNewState((int)ArcherStateID.Idle,            new EnemyIdleState(this, "idle", _archerStateData));
        AddNewState((int)ArcherStateID.Move,            new EnemyMoveState(this, "walk", _archerStateData));
        AddNewState((int)ArcherStateID.PlayerDetected,  new EnemyPlayerDetectedState(this, "playerDetected", _archerStateData));
        AddNewState((int)ArcherStateID.LookForPlayer,   new EnemyLookForPlayerState(this, "lookForPlayer", _archerStateData));
        AddNewState((int)ArcherStateID.Stun,            new EnemyStunState(this, "stun", _archerStateData));
        AddNewState((int)ArcherStateID.Dead,            new EnemyDeadState(this, "dead", _archerStateData));
        
        AddNewState((int)ArcherStateID.Dodge,           new ArcherDodgeState(this, "dodge", _archerStateData));
        AddNewState((int)ArcherStateID.MeleeAttack,     new ArcherMeleeAttackState(this, "meleeAttack", _archerStateData));
        AddNewState((int)ArcherStateID.RangedAttack,    new ArcherRangedAttackState(this, "rangedAttack", _archerStateData));
    }

    protected override void FSMInitializeTransitions()
    {
        // from Idle...
        AddNewTransition((int)ArcherStateID.Idle, (int)ArcherStateID.Move,                      () => { return false; }); // is idle time over
        AddNewTransition((int)ArcherStateID.Idle, (int)ArcherStateID.PlayerDetected,            () => { return CheckPlayerInMinAgroRange(); });

        // from Move...
        AddNewTransition((int)ArcherStateID.Move, (int)ArcherStateID.Idle,                      () => { return IsTouchingWall() || IsTouchingLedge(-transform.up); });
        AddNewTransition((int)ArcherStateID.Move, (int)ArcherStateID.PlayerDetected,            () => { return CheckPlayerInMinAgroRange(); });

        // from PlayerDetected...
        AddNewTransition((int)ArcherStateID.PlayerDetected, (int)ArcherStateID.Dodge,           () => { return CheckPlayerInMeleeRange(); });
        AddNewTransition((int)ArcherStateID.PlayerDetected, (int)ArcherStateID.MeleeAttack,     () => { return false; });
        AddNewTransition((int)ArcherStateID.PlayerDetected, (int)ArcherStateID.RangedAttack,    () => { return false; });
        AddNewTransition((int)ArcherStateID.PlayerDetected, (int)ArcherStateID.LookForPlayer,   () => { return !CheckPlayerInMaxAgroRange(); });

        // from LookForPlayer...
        AddNewTransition((int)ArcherStateID.LookForPlayer, (int)ArcherStateID.PlayerDetected,   () => { return CheckPlayerInMinAgroRange(); });
        AddNewTransition((int)ArcherStateID.LookForPlayer, (int)ArcherStateID.Move,             () => { return false; });   // _isAllTurnsTimeDone

        // from Stun...
        AddNewTransition((int)ArcherStateID.Stun, (int)ArcherStateID.MeleeAttack,               () => { return CheckPlayerInMeleeRange(); });
        AddNewTransition((int)ArcherStateID.Stun, (int)ArcherStateID.PlayerDetected,            () => { return CheckPlayerInMinAgroRange(); });
        AddNewTransition((int)ArcherStateID.Stun, (int)ArcherStateID.LookForPlayer,             () => { return !CheckPlayerInMeleeRange() && !CheckPlayerInMinAgroRange(); });

        // from Dead...
        // None

        // from Dodge...
        AddNewTransition((int)ArcherStateID.Dodge, (int)ArcherStateID.MeleeAttack,              () => { return CheckPlayerInMeleeRange(); });
        AddNewTransition((int)ArcherStateID.Dodge, (int)ArcherStateID.RangedAttack,             () => { return CheckPlayerInMaxAgroRange() && !CheckPlayerInMeleeRange(); });
        AddNewTransition((int)ArcherStateID.Dodge, (int)ArcherStateID.LookForPlayer,            () => { return !CheckPlayerInMaxAgroRange(); });

        // from MeleeAttack...
        AddNewTransition((int)ArcherStateID.MeleeAttack, (int)ArcherStateID.PlayerDetected,     () => { return CheckPlayerInMinAgroRange(); });
        AddNewTransition((int)ArcherStateID.MeleeAttack, (int)ArcherStateID.LookForPlayer,      () => { return !CheckPlayerInMinAgroRange(); });

        // from RangedAttack...
        AddNewTransition((int)ArcherStateID.RangedAttack, (int)ArcherStateID.PlayerDetected,    () => { return CheckPlayerInMinAgroRange(); });
        AddNewTransition((int)ArcherStateID.RangedAttack, (int)ArcherStateID.LookForPlayer,     () => { return !CheckPlayerInMinAgroRange(); });
    }

    protected override bool FSMUpdateConditions()
    {
        return !GameManager.Instance.IsGamePaused;
    }

    protected override bool FSMFixedUpdateConditions()
    {
        return !GameManager.Instance.IsGamePaused;
    }
    #endregion
}
