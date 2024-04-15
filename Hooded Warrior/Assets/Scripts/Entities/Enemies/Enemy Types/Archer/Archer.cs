using UnityEngine;

public sealed class Archer : Enemy
{
    #region States and Data
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

        if (_entityIntStatusComponents.IsDead)
            ChangeState((int)ArcherStateID.Dead);
        else if (_entityIntStatusComponents.IsStuned && !IsStateActive((int)ArcherStateID.Stun))
            ChangeState((int)ArcherStateID.Stun);
        else if (!_entityIntStatusComponents.IsStuned && _entityIntObjComponents.Rigidbody.velocity.x != 0)
            ChangeState((int)ArcherStateID.LookForPlayer);
        else if (!_entityIntStatusComponents.IsStuned && CheckPlayerInMinAgroRange())
            ChangeState((int)ArcherStateID.RangedAttack);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(MeleeAttackPosition.transform.position, _meleeAttackStateData.AttackRadius);
    }

    protected override void InitializeStates()
    {
        AddNewState((int)ArcherStateID.Idle,            new ArcherIdleState(this, "idle", _idleStateData));
        AddNewState((int)ArcherStateID.Move,            new ArcherMoveState(this, "walk", _moveStateData));
        AddNewState((int)ArcherStateID.PlayerDetected,  new ArcherPlayerDetectedState(this, "playerDetected", _playerDetectedStateData));
        AddNewState((int)ArcherStateID.LookForPlayer,   new ArcherLookForPlayerState(this, "lookForPlayer", _lookForPlayerStateData));
        AddNewState((int)ArcherStateID.Stun,            new ArcherStunState(this, "stun", _stunStateData));
        AddNewState((int)ArcherStateID.Dead,            new ArcherDeadState(this, "dead", _deadStateData));
        AddNewState((int)ArcherStateID.Dodge,           new ArcherDodgeState(this, "dodge", _dodgeStateData));
        AddNewState((int)ArcherStateID.MeleeAttack,     new ArcherMeleeAttackState(this, "meleeAttack", _meleeAttackStateData));
        AddNewState((int)ArcherStateID.RangedAttack,    new ArcherRangedAttackState(this, "rangedAttack", _rangedAttackStateData));
    }
    #endregion
}
