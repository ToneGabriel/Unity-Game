using UnityEngine;

public class BringerOfDeath : Enemy
{
    #region States and Data
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
    protected override void Awake()
    {
        base.Awake();

        InitializeStates();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _stateMachine.InitializeState(_states[(int)BringerOfDeathStateID.Move]);
    }
    #endregion

    #region Triggers
    public void TriggerMeleeAttack()
    {
        //MeleeAttackState.TriggerMeleeAttack();
    }

    public void TriggerPortalRangedAttack()
    {
        //PortalRangedAttackState.TriggerPortalRangedAttack();
    }

    public void TriggerOrbRangedAttack()
    {
        //OrbRangedAttackState.TriggerOrbRangedAttack();
    }

    public void FinishMeleeAttack()
    {
        //MeleeAttackState.FinishMeleeAttack();
    }

    public void FinishPortalRangedAttack()
    {
        //PortalRangedAttackState.FinishPortalRangedAttack();
    }

    public void FinishOrbRangedAttack()
    {
        //OrbRangedAttackState.FinishOrbRangedAttack();
    }

    public void FinishDeathAnimation()
    {
        //DeadState.FinishDeathAnimation();
    }
    #endregion

    #region Other Functions
    protected void InitializeStates()
    {
        _stateMachine   = new FiniteStateMachine();
        _states         = new State[(int)BringerOfDeathStateID.Count];

        _states[(int)BringerOfDeathStateID.Idle]                = new BringerOfDeathIdleState(this, "idle", _idleStateData);
        _states[(int)BringerOfDeathStateID.Move]                = new BringerOfDeathMoveState(this, "walk", _moveStateData);
        _states[(int)BringerOfDeathStateID.PlayerDetected]      = new BringerOfDeathPlayerDetectedState(this, "playerDetected", _playerDetectedStateData);
        _states[(int)BringerOfDeathStateID.LookForPlayer]       = new BringerOfDeathLookForPlayerState(this, "lookForPlayer", _lookForPlayerStateData);
        _states[(int)BringerOfDeathStateID.Charge]              = new BringerOfDeathChargeState(this, "charge", _chargeStateData);
        _states[(int)BringerOfDeathStateID.Dead]                = new BringerOfDeathDeadState(this, "dead", _deadStateData);

        _states[(int)BringerOfDeathStateID.MeleeAttack]         = new BringerOfDeathMeleeAttackState(this, "meleeAttack", _meleeAttackStateData);
        _states[(int)BringerOfDeathStateID.PortalRangedAttack]  = new BringerOfDeathRangedAttackState(this, "portalRangedAttack", _portalRangedAttackStateData);
        _states[(int)BringerOfDeathStateID.OrbRangedAttack]     = new BringerOfDeathRangedAttackState(this, "orbRangedAttack", _orbRangedAttackStateData);
    }

    public override void Damage(AttackDetails attackdetails)
    {
        base.Damage(attackdetails);

        if (_statusComponents.IsDead)
            ChangeState((int)BringerOfDeathStateID.Dead);
        else if (_objectComponents.Rigidbody.velocity.x != 0)
            ChangeState((int)BringerOfDeathStateID.LookForPlayer);
    }
    
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(MeleeAttackPosition.transform.position, _meleeAttackStateData.AttackRadius);
    }
    #endregion
}
