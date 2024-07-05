using System.Collections.Generic;
using UnityEngine;

public sealed class Archer : Enemy
{
    private enum StateID
    {
        Patrol,
        Aggro,
        Hit
    }

    private FiniteStateMachine<StateID> _archerController = null;

    #region Components
    public GameObject MeleeAttackPosition;
    public GameObject RangedAttackPosition;
    #endregion

    #region Unity Functions
    protected override void Awake()
    {
        base.Awake();

        _archerController = new FiniteStateMachine<StateID>();

        _archerController.InitializeStates
        (
            new KeyValuePair<StateID, State>(StateID.Patrol,    new EnemyPatrolMode(this, null)),
            new KeyValuePair<StateID, State>(StateID.Aggro,     new ArcherAggroMode(this, null)),
            new KeyValuePair<StateID, State>(StateID.Hit,       new EnemyHitMode(this, null))
        );

        _archerController.SetDefaultState(StateID.Patrol);
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

    #region Controller Interface
    protected override State GetControlState()
    {
        return _archerController;
    }

    protected override bool UpdateConditions()
    {
        return !GameManager.Instance.IsGamePaused;
    }

    protected override bool FixedUpdateConditions()
    {
        return !GameManager.Instance.IsGamePaused;
    }

    public override void ChangeState()
    {
        if (_archerController.IsStateActive(StateID.Patrol))
            _archerController.ChangeState(StateID.Aggro);
        else if (_archerController.IsStateActive(StateID.Aggro))
            _archerController.ChangeState(StateID.Patrol);
        else if (_archerController.IsStateActive(StateID.Hit))
            _archerController.ChangeState(StateID.Aggro);
    }
    #endregion Controller Interface

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

        //if (_entityActionComponents.IsDead)
        //    ChangeState((int)ArcherAggroModeStateID.Dead);
        //else if (_entityActionComponents.IsStuned && !IsStateActive((int)ArcherAggroModeStateID.Stun))
        //    ChangeState((int)ArcherAggroModeStateID.Stun);
        //else if (!_entityActionComponents.IsStuned && _entityActionComponents.Rigidbody.velocity.x != 0)
        //    ChangeState((int)ArcherAggroModeStateID.LookForPlayer);
        //else if (!_entityActionComponents.IsStuned && CheckPlayerInMinAgroRange())
        //    ChangeState((int)ArcherAggroModeStateID.RangedAttack);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        //Gizmos.DrawWireSphere(MeleeAttackPosition.transform.position, _archerAggroModeData.MeleeAttackRadius);
    }

    //protected override void FSMInitializeTransitions()
    //{
    //    // from Idle...
    //    AddNewTransition((int)ArcherStateID.Idle, (int)ArcherStateID.Move,                      () => { return false; }); // is idle time over
    //    AddNewTransition((int)ArcherStateID.Idle, (int)ArcherStateID.PlayerDetected,            () => { return CheckPlayerInMinAgroRange(); });

    //    // from Move...
    //    AddNewTransition((int)ArcherStateID.Move, (int)ArcherStateID.Idle,                      () => { return IsTouchingWall() || IsTouchingLedge(-transform.up); });
    //    AddNewTransition((int)ArcherStateID.Move, (int)ArcherStateID.PlayerDetected,            () => { return CheckPlayerInMinAgroRange(); });

    //    // from PlayerDetected...
    //    AddNewTransition((int)ArcherStateID.PlayerDetected, (int)ArcherStateID.Dodge,           () => { return CheckPlayerInMeleeRange(); });
    //    AddNewTransition((int)ArcherStateID.PlayerDetected, (int)ArcherStateID.MeleeAttack,     () => { return false; });
    //    AddNewTransition((int)ArcherStateID.PlayerDetected, (int)ArcherStateID.RangedAttack,    () => { return false; });
    //    AddNewTransition((int)ArcherStateID.PlayerDetected, (int)ArcherStateID.LookForPlayer,   () => { return !CheckPlayerInMaxAgroRange(); });

    //    // from LookForPlayer...
    //    AddNewTransition((int)ArcherStateID.LookForPlayer, (int)ArcherStateID.PlayerDetected,   () => { return CheckPlayerInMinAgroRange(); });
    //    AddNewTransition((int)ArcherStateID.LookForPlayer, (int)ArcherStateID.Move,             () => { return false; });   // _isAllTurnsTimeDone

    //    // from Stun...
    //    AddNewTransition((int)ArcherStateID.Stun, (int)ArcherStateID.MeleeAttack,               () => { return CheckPlayerInMeleeRange(); });
    //    AddNewTransition((int)ArcherStateID.Stun, (int)ArcherStateID.PlayerDetected,            () => { return CheckPlayerInMinAgroRange(); });
    //    AddNewTransition((int)ArcherStateID.Stun, (int)ArcherStateID.LookForPlayer,             () => { return !CheckPlayerInMeleeRange() && !CheckPlayerInMinAgroRange(); });

    //    // from Dead...
    //    // None

    //    // from Dodge...
    //    AddNewTransition((int)ArcherStateID.Dodge, (int)ArcherStateID.MeleeAttack,              () => { return CheckPlayerInMeleeRange(); });
    //    AddNewTransition((int)ArcherStateID.Dodge, (int)ArcherStateID.RangedAttack,             () => { return CheckPlayerInMaxAgroRange() && !CheckPlayerInMeleeRange(); });
    //    AddNewTransition((int)ArcherStateID.Dodge, (int)ArcherStateID.LookForPlayer,            () => { return !CheckPlayerInMaxAgroRange(); });

    //    // from MeleeAttack...
    //    AddNewTransition((int)ArcherStateID.MeleeAttack, (int)ArcherStateID.PlayerDetected,     () => { return CheckPlayerInMinAgroRange(); });
    //    AddNewTransition((int)ArcherStateID.MeleeAttack, (int)ArcherStateID.LookForPlayer,      () => { return !CheckPlayerInMinAgroRange(); });

    //    // from RangedAttack...
    //    AddNewTransition((int)ArcherStateID.RangedAttack, (int)ArcherStateID.PlayerDetected,    () => { return CheckPlayerInMinAgroRange(); });
    //    AddNewTransition((int)ArcherStateID.RangedAttack, (int)ArcherStateID.LookForPlayer,     () => { return !CheckPlayerInMinAgroRange(); });
    //}
    #endregion
}
