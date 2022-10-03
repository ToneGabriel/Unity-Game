using UnityEngine;

public class BringerOfDeath : Enemy
{
    #region States and Data
    public BringerOfDeath_IdleState IdleState { get; private set; }
    public BringerOfDeath_MoveState MoveState { get; private set; }
    public BringerOfDeath_ChargeState ChargeState { get; private set; }
    public BringerOfDeath_PlayerDetectedState PlayerDetectedState { get; private set; }
    public BringerOfDeath_MeleeAttackState MeleeAttackState { get; private set; }
    public BringerOfDeath_LookForPlayerState LookForPlayerState { get; private set; }
    public BringerOfDeath_DeadState DeadState { get; private set; }
    public BringerOfDeath_RangedAttackState PortalRangedAttackState { get; private set; }
    public BringerOfDeath_RangedAttackState OrbRangedAttackState { get; private set; }

    [SerializeField] private Data_Idle _idleStateData;
    [SerializeField] private Data_Move _moveStateData;
    [SerializeField] private Data_Charge _chargeStateData;
    [SerializeField] private Data_PlayerDetected _playerDetectedStateData;
    [SerializeField] private Data_MeleeAttack _meleeAttackStateData;
    [SerializeField] private Data_LookForPlayer _lookForPlayerStateData;
    [SerializeField] private Data_Dead _deadStateData;
    [SerializeField] private Data_RangedAttack _portalRangedAttackStateData;
    [SerializeField] private Data_RangedAttack _orbRangedAttackStateData;
    #endregion

    #region Components
    public GameObject MeleeAttackPosition;
    public GameObject PortalRangedAttackPosition;
    public GameObject OrbRangedAttackPosition;
    #endregion

    #region Unity functions
    protected override void OnEnable()
    {
        base.OnEnable();

        StateMachine.Initialize(MoveState);
    }

    protected override void LogicUpdate()
    {
        base.LogicUpdate();

        //if (GameManager.Instance.IsLoadingData)     // TODO: boss spawn
            //gameObject.SetActive(false);
    }

    protected override void PhysicsUpdate() => base.PhysicsUpdate();
    #endregion

    #region Triggers
    public void TriggerMeleeAttack()
    {
        MeleeAttackState.TriggerMeleeAttack();
    }

    public void TriggerPortalRangedAttack()
    {
        PortalRangedAttackState.TriggerPortalRangedAttack();
    }

    public void TriggerOrbRangedAttack()
    {
        OrbRangedAttackState.TriggerOrbRangedAttack();
    }

    public void FinishMeleeAttack()
    {
        MeleeAttackState.FinishMeleeAttack();
    }

    public void FinishPortalRangedAttack()
    {
        PortalRangedAttackState.FinishPortalRangedAttack();
    }

    public void FinishOrbRangedAttack()
    {
        OrbRangedAttackState.FinishOrbRangedAttack();
    }

    public void FinishDeathAnimation()
    {
        DeadState.FinishDeathAnimation();
    }
    #endregion

    #region Other Functions
    protected override void InitializeStates()
    {
        base.InitializeStates();

        IdleState = new BringerOfDeath_IdleState(this, StateMachine, "idle", _idleStateData);
        MoveState = new BringerOfDeath_MoveState(this, StateMachine, "walk", _moveStateData);
        ChargeState = new BringerOfDeath_ChargeState(this, StateMachine, "charge", _chargeStateData);
        PlayerDetectedState = new BringerOfDeath_PlayerDetectedState(this, StateMachine, "playerDetected", _playerDetectedStateData);
        LookForPlayerState = new BringerOfDeath_LookForPlayerState(this, StateMachine, "lookForPlayer", _lookForPlayerStateData);
        DeadState = new BringerOfDeath_DeadState(this, StateMachine, "dead", _deadStateData);

        MeleeAttackState = new BringerOfDeath_MeleeAttackState(this, StateMachine, "meleeAttack", _meleeAttackStateData);
        PortalRangedAttackState = new BringerOfDeath_RangedAttackState(this, StateMachine, "portalRangedAttack", _portalRangedAttackStateData);
        OrbRangedAttackState = new BringerOfDeath_RangedAttackState(this, StateMachine, "orbRangedAttack", _orbRangedAttackStateData);
    }

    public override void Damage(AttackDetails attackdetails)
    {
        base.Damage(attackdetails);

        if (IsDead)
            StateMachine.ChangeState(DeadState);
        else if (Rigidbody.velocity.x != 0)
            StateMachine.ChangeState(LookForPlayerState);
    }
    
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(MeleeAttackPosition.transform.position, _meleeAttackStateData.AttackRadius);
    }
    #endregion
}
