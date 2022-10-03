using UnityEngine;

public class Archer : Enemy
{
    #region States and Data
    public Archer_IdleState IdleState { get; private set; }
    public Archer_MoveState MoveState { get; private set; }
    public Archer_PlayerDetectedState PlayerDetectedState { get; private set; }
    public Archer_LookForPlayerState LookForPlayerState { get; private set; }
    public Archer_StunState StunState { get; private set; }
    public Archer_DeadState DeadState { get; private set; }
    public Archer_DodgeState DodgeState { get; private set; }
    public Archer_MeleeAttackState MeleeAttackState { get; private set; }
    public Archer_RangedAttackState RangedAttackState { get; private set; }

    [SerializeField] private Data_Idle _idleStateData;
    [SerializeField] private Data_Move _moveStateData;
    [SerializeField] private Data_PlayerDetected _playerDetectedStateData;
    [SerializeField] private Data_LookForPlayer _lookForPlayerStateData;
    [SerializeField] private Data_Stun _stunStateData;
    [SerializeField] private Data_Dead _deadStateData;
    [SerializeField] private Data_Dodge _dodgeStateData;
    [SerializeField] private Data_MeleeAttack _meleeAttackStateData;
    [SerializeField] private Data_RangedAttack _rangedAttackStateData;
    #endregion

    #region Components
    public GameObject MeleeAttackPosition;
    public GameObject RangedAttackPosition;
    #endregion

    #region Unity Functions
    protected override void OnEnable()
    {
        base.OnEnable();

        StateMachine.Initialize(MoveState);
    }
    #endregion

    #region Triggers
    public void TriggerMeleeAttack()
    {
        MeleeAttackState.TriggerMeleeAttack();
    }

    public void TriggerRangedAttack()
    {
        RangedAttackState.TriggerRangedAttack();
    }

    public void FinishMeleeAttack()
    {
        MeleeAttackState.FinishMeleeAttack();
    }

    public void FinishRangedAttack()
    {
        RangedAttackState.FinishRangedAttack();
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

        IdleState = new Archer_IdleState(this, StateMachine, "idle", _idleStateData);
        MoveState = new Archer_MoveState(this, StateMachine, "walk", _moveStateData);
        PlayerDetectedState = new Archer_PlayerDetectedState(this, StateMachine, "playerDetected", _playerDetectedStateData);
        LookForPlayerState = new Archer_LookForPlayerState(this, StateMachine, "lookForPlayer", _lookForPlayerStateData);
        StunState = new Archer_StunState(this, StateMachine, "stun", _stunStateData);
        DeadState = new Archer_DeadState(this, StateMachine, "dead", _deadStateData);
        DodgeState = new Archer_DodgeState(this, StateMachine, "dodge", _dodgeStateData);

        MeleeAttackState = new Archer_MeleeAttackState(this, StateMachine, "meleeAttack", _meleeAttackStateData);
        RangedAttackState = new Archer_RangedAttackState(this, StateMachine, "rangedAttack", _rangedAttackStateData);
    }

    public override void Damage(AttackDetails attackdetails)                        // Called when taking damage (message sent from attacker)
    {
        base.Damage(attackdetails);

        if (IsDead)
            StateMachine.ChangeState(DeadState);
        else if (IsStuned && StateMachine.CurrentState != StunState)
            StateMachine.ChangeState(StunState);
        else if (!IsStuned && Rigidbody.velocity.x != 0)
            StateMachine.ChangeState(LookForPlayerState);
        else if (!IsStuned && CheckPlayerInMinAgroRange())
            StateMachine.ChangeState(RangedAttackState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(MeleeAttackPosition.transform.position, _meleeAttackStateData.AttackRadius);
    }
    #endregion
}
