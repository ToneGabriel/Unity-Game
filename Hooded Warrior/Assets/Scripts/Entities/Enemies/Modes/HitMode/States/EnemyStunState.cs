using UnityEngine;

public class EnemyStunState : EntityModeState<EnemyHitMode.StateID>
{
    private readonly EnemyHitMode       _enemyHitMode;
    private readonly EnemyHitModeData   _enemyHitModeData;

    protected bool _isStunTimeOver;
    protected bool _isGrounded;
    protected bool _isMovementStopped;
    protected bool _isPlayerInMeleeRange;
    protected bool _isPlayerInMinAgroRange;

    public EnemyStunState(EnemyHitMode mode, EnemyHitModeData data, string animBoolName)
        : base(mode, animBoolName)
    {
        _enemyHitMode = mode;
        _enemyHitModeData = data;
    }

    public override void Enter()
    {
        base.Enter();

        _isStunTimeOver = false;
        _isMovementStopped = false;
        //_enemy.DamageHop(_enemyHitModeData.StunKnockBackDirection, _enemyHitModeData.StunKnockBackSpeed);     // knock back  
    }

    public override void Exit()
    {
        base.Exit();

        //_enemy.ResetStunResistnce();
        //_enemy.RigidbodyType = RigidbodyType2D.Dynamic;
    }

    public override void Update()
    {
        base.Update();

        if (Time.time >= _stateStartTime + _enemyHitModeData.StunTime)                                                        // Counts stun time
            _isStunTimeOver = true;

        if (_isGrounded && Time.time >= _stateStartTime + _enemyHitModeData.StunKnockBackTime && !_isMovementStopped)         // sets velocity to 0 while stunned
        {
            _isMovementStopped      = true;
            //_enemy.RigidbodyType    = RigidbodyType2D.Static;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        //_isGrounded             = _enemy.IsGrounded();
        //_isPlayerInMeleeRange   = _enemy.CheckPlayerInMeleeRange();
        //_isPlayerInMinAgroRange = _enemy.CheckPlayerInMinAgroRange();
    }
}
