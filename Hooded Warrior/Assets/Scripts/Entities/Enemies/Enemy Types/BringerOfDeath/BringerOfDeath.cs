using UnityEngine;

public sealed class BringerOfDeath : Enemy
{
    #region States and Data
    [SerializeField] private Data_Idle _idleStateData;
    [SerializeField] private Data_Move _moveStateData;
    [SerializeField] private Data_Charge _chargeStateData;
    [SerializeField] private Data_PlayerDetected _playerDetectedStateData;
    [SerializeField] private Data_LookForPlayer _lookForPlayerStateData;
    [SerializeField] private Data_Dead _deadStateData;
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
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Start()
    {
        base.Start();

        ObjectPoolManager.Instance.RequestPool<DeathPortal>();
        ObjectPoolManager.Instance.RequestPool<DeathOrb>();
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
    public override void Damage(AttackDetails attackdetails)
    {
        base.Damage(attackdetails);

        //if (EntityInternComponents.IsDead)
        //    ChangeState((int)BringerOfDeathStateID.Dead);
        //else if (VelocityX != 0)
        //    ChangeState((int)BringerOfDeathStateID.LookForPlayer);
    }
    
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        //Gizmos.DrawWireSphere(MeleeAttackPosition.transform.position, _meleeAttackStateData.AttackRadius);
    }

    protected override void FSMInitializeModes()
    {
        throw new System.NotImplementedException();
        //AddNewState((int)BringerOfDeathStateID.Idle,                new BringerOfDeathIdleState(this, "idle", _idleStateData));
        //AddNewState((int)BringerOfDeathStateID.Move,                new BringerOfDeathMoveState(this, "walk", _moveStateData));
        //AddNewState((int)BringerOfDeathStateID.PlayerDetected,      new BringerOfDeathPlayerDetectedState(this, "playerDetected", _playerDetectedStateData));
        //AddNewState((int)BringerOfDeathStateID.LookForPlayer,       new BringerOfDeathLookForPlayerState(this, "lookForPlayer", _lookForPlayerStateData));
        //AddNewState((int)BringerOfDeathStateID.Charge,              new BringerOfDeathChargeState(this, "charge", _chargeStateData));
        //AddNewState((int)BringerOfDeathStateID.Dead,                new BringerOfDeathDeadState(this, "dead", _deadStateData));
        //AddNewState((int)BringerOfDeathStateID.MeleeAttack,         new BringerOfDeathMeleeAttackState(this, "meleeAttack", _meleeAttackStateData));
        //AddNewState((int)BringerOfDeathStateID.PortalRangedAttack,  new BringerOfDeathRangedAttackState(this, "portalRangedAttack", _portalRangedAttackStateData));
        //AddNewState((int)BringerOfDeathStateID.OrbRangedAttack,     new BringerOfDeathRangedAttackState(this, "orbRangedAttack", _orbRangedAttackStateData));
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
