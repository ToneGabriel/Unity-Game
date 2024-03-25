using UnityEngine;

public class Bull : Enemy
{
    #region States and Data
    [SerializeField] private Data_Idle _idleStateData;
    [SerializeField] private Data_Move _moveStateData;
    [SerializeField] private Data_PlayerDetected _playerDetectedStateData;
    [SerializeField] private Data_Charge _chargeStateData;
    [SerializeField] private Data_LookForPlayer _lookForPlayerStateData;
    [SerializeField] private Data_MeleeAttack _meleeAttackStateData;
    [SerializeField] private Data_Stun _stunStateData;
    [SerializeField] private Data_Dead _deadStateData;
    #endregion

    #region Components
    public GameObject MeleeAttackPosition;
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

        _stateMachine.InitializeState(_states[(int)BullStateID.Move]);
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
    protected void InitializeStates()
    {
        _stateMachine   = new FiniteStateMachine();
        _states         = new State[(int)BullStateID.Count];

        _states[(int)BullStateID.Idle]              = new BullIdleState(this, "idle", _idleStateData);
        _states[(int)BullStateID.Move]              = new BullMoveState(this, "walk", _moveStateData);
        _states[(int)BullStateID.PlayerDetected]    = new BullPlayerDetectedState(this, "playerDetected", _playerDetectedStateData);
        _states[(int)BullStateID.LookForPlayer]     = new BullLookForPlayerState(this, "lookForPlayer", _lookForPlayerStateData);
        _states[(int)BullStateID.Charge]            = new BullChargeState(this, "charge", _chargeStateData);
        _states[(int)BullStateID.MeleeAttack]       = new BullMeleeAttackState(this, "meleeAttack", _meleeAttackStateData);
        _states[(int)BullStateID.Stun]              = new BullStunState(this, "stun", _stunStateData);
        _states[(int)BullStateID.Dead]              = new BullDeadState(this, "dead", _deadStateData);
    }

    public override void Damage(AttackDetails attackdetails)
    {
        base.Damage(attackdetails);

        if (_statusComponents.IsDead)
            ChangeState((int)BullStateID.Dead);
        else if (_statusComponents.IsStuned && _stateMachine.CurrentState != _states[(int)BullStateID.Stun])
            ChangeState((int)BullStateID.Stun);
        else if (!_statusComponents.IsStuned && _objectComponents.Rigidbody.velocity.x != 0)
            ChangeState((int)BullStateID.LookForPlayer);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        //Gizmos.DrawWireSphere(meleeAttackPosition.transform.position, meleeAttackStateData.attackRadius);
    }
    #endregion
}
