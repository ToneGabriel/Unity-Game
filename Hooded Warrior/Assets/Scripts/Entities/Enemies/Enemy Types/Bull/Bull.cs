using UnityEngine;

public class Bull : Enemy
{
    #region States and Data
    public Bull_IdleState IdleState { get; private set; }
    public Bull_MoveState MoveState { get; private set; }
    public Bull_PlayerDetectedState PlayerDetectedState { get; private set; }
    public Bull_ChargeState ChargeState { get; private set; }
    public Bull_LookForPlayerState LookForPlayerState { get; private set; }
    public Bull_MeleeAttackState MeleeAttackState { get; private set; }
    public Bull_StunState StunState { get; private set; }
    public Bull_DeadState DeadState { get; private set; }

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
    protected override void OnEnable()
    {
        base.OnEnable();

        _stateMachine.Initialize(MoveState);
    }
    #endregion

    #region Triggers
    // get acces to Trigger and Finish attack in Bull_MeleeAttackState class from Bull class
    // set on attack animation
    public void TriggerAttack()
    {
        MeleeAttackState.TriggerMeleeAttack();
    }

    public void FinishAttack()
    {
        MeleeAttackState.FinishMeleeAttack();
    }

    public void FinishDeathAnimation()
    {
        DeadState.FinishDeathAnimation();
    }
    #endregion

    #region Other functions
    protected void InitializeStates()
    {
        IdleState = new Bull_IdleState(this, _stateMachine, "idle", _idleStateData);
        MoveState = new Bull_MoveState(this, _stateMachine, "walk", _moveStateData);
        PlayerDetectedState = new Bull_PlayerDetectedState(this, _stateMachine, "playerDetected", _playerDetectedStateData);
        ChargeState = new Bull_ChargeState(this, _stateMachine, "charge", _chargeStateData);
        LookForPlayerState = new Bull_LookForPlayerState(this, _stateMachine, "lookForPlayer", _lookForPlayerStateData);
        MeleeAttackState = new Bull_MeleeAttackState(this, _stateMachine, "meleeAttack", _meleeAttackStateData);
        StunState = new Bull_StunState(this, _stateMachine, "stun", _stunStateData);
        DeadState = new Bull_DeadState(this, _stateMachine, "dead", _deadStateData);
    }

    public override void Damage(AttackDetails attackdetails)
    {
        base.Damage(attackdetails);

        if (IsDead)
            _stateMachine.ChangeState(DeadState);
        else if (IsStuned && _stateMachine.CurrentState != StunState)
            _stateMachine.ChangeState(StunState);
        else if (!IsStuned && _rigidbody.velocity.x != 0)
            _stateMachine.ChangeState(LookForPlayerState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        //Gizmos.DrawWireSphere(meleeAttackPosition.transform.position, meleeAttackStateData.attackRadius);
    }
    #endregion
}
