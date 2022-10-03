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

        StateMachine.Initialize(MoveState);
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
    protected override void InitializeStates()
    {
        base.InitializeStates();

        IdleState = new Bull_IdleState(this, StateMachine, "idle", _idleStateData);
        MoveState = new Bull_MoveState(this, StateMachine, "walk", _moveStateData);
        PlayerDetectedState = new Bull_PlayerDetectedState(this, StateMachine, "playerDetected", _playerDetectedStateData);
        ChargeState = new Bull_ChargeState(this, StateMachine, "charge", _chargeStateData);
        LookForPlayerState = new Bull_LookForPlayerState(this, StateMachine, "lookForPlayer", _lookForPlayerStateData);
        MeleeAttackState = new Bull_MeleeAttackState(this, StateMachine, "meleeAttack", _meleeAttackStateData);
        StunState = new Bull_StunState(this, StateMachine, "stun", _stunStateData);
        DeadState = new Bull_DeadState(this, StateMachine, "dead", _deadStateData);
    }

    public override void Damage(AttackDetails attackdetails)
    {
        base.Damage(attackdetails);

        if (IsDead)
            StateMachine.ChangeState(DeadState);
        else if (IsStuned && StateMachine.CurrentState != StunState)
            StateMachine.ChangeState(StunState);
        else if (!IsStuned && Rigidbody.velocity.x != 0)
            StateMachine.ChangeState(LookForPlayerState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        //Gizmos.DrawWireSphere(meleeAttackPosition.transform.position, meleeAttackStateData.attackRadius);
    }
    #endregion
}
