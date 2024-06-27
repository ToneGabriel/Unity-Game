using UnityEngine;

public sealed class Bull : Enemy
{
    //#region States and Data
    //[SerializeField] private Data_Idle _idleStateData;
    //[SerializeField] private Data_Move _moveStateData;
    //[SerializeField] private Data_PlayerDetected _playerDetectedStateData;
    //[SerializeField] private Data_Charge _chargeStateData;
    //[SerializeField] private Data_LookForPlayer _lookForPlayerStateData;
    //[SerializeField] private Data_Stun _stunStateData;
    //[SerializeField] private Data_Dead _deadStateData;
    //#endregion

    #region Components
    public GameObject MeleeAttackPosition;
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
    #endregion

    #region Triggers
    // get acces to Trigger and Finish attack in Bull_MeleeAttackState class from Bull class
    // set on attack animation
    public void TriggerAttack()
    {
        //MeleeAttackState.TriggerMeleeAttack();
    }

    public void FinishAttack()
    {
        //MeleeAttackState.FinishMeleeAttack();
    }

    public void FinishDeathAnimation()
    {
        //DeadState.FinishDeathAnimation();
    }
    #endregion

    #region Other functions
    public override void Damage(AttackDetails attackdetails)
    {
        base.Damage(attackdetails);

        //if (EntityInternComponents.IsDead)
        //    ChangeState((int)BullStateID.Dead);
        //else if (EntityInternComponents.IsStuned && !IsStateActive((int)BullStateID.Stun))
        //    ChangeState((int)BullStateID.Stun);
        //else if (!EntityInternComponents.IsStuned && VelocityX != 0)
        //    ChangeState((int)BullStateID.LookForPlayer);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        //Gizmos.DrawWireSphere(meleeAttackPosition.transform.position, meleeAttackStateData.attackRadius);
    }

    //protected override void FSMInitializeModes()
    //{
    //    //AddNewState((int)BullStateID.Idle,              new BullIdleState(this, "idle", _idleStateData));
    //    //AddNewState((int)BullStateID.Move,              new BullMoveState(this, "walk", _moveStateData));
    //    //AddNewState((int)BullStateID.PlayerDetected,    new BullPlayerDetectedState(this, "playerDetected", _playerDetectedStateData));
    //    //AddNewState((int)BullStateID.LookForPlayer,     new BullLookForPlayerState(this, "lookForPlayer", _lookForPlayerStateData));
    //    //AddNewState((int)BullStateID.Charge,            new BullChargeState(this, "charge", _chargeStateData));
    //    //AddNewState((int)BullStateID.MeleeAttack,       new BullMeleeAttackState(this, "meleeAttack", _meleeAttackStateData));
    //    //AddNewState((int)BullStateID.Stun,              new BullStunState(this, "stun", _stunStateData));
    //    //AddNewState((int)BullStateID.Dead,              new BullDeadState(this, "dead", _deadStateData));
    //}

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
