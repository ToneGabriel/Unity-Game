using UnityEngine;

public class EnemyStunState : EnemyState
{
    protected bool _isStunTimeOver;
    protected bool _isGrounded;
    protected bool _isMovementStopped;
    protected bool _isPlayerInMeleeRange;
    protected bool _isPlayerInMinAgroRange;

    public EnemyStunState(Enemy enemy, string animBoolName) 
        : base(enemy, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _isStunTimeOver = false;
        _isMovementStopped = false;
        _enemy.DamageHop(_enemy.PatrolData.StunKnockBackDirection, _enemy.PatrolData.StunKnockBackSpeed);     // knock back  
    }

    public override void Exit()
    {
        base.Exit();

        _enemy.ResetStunResistnce();
        _enemy.RigidbodyType = RigidbodyType2D.Dynamic;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= _stateStartTime + _enemy.PatrolData.StunTime)                                                        // Counts stun time
            _isStunTimeOver = true;

        if (_isGrounded && Time.time >= _stateStartTime + _enemy.PatrolData.StunKnockBackTime && !_isMovementStopped)         // sets velocity to 0 while stunned
        {
            _isMovementStopped      = true;
            _enemy.RigidbodyType    = RigidbodyType2D.Static;
        }
    }

    protected override void DoChecks()
    {
        base.DoChecks();

        _isGrounded             = _enemy.IsGrounded();
        _isPlayerInMeleeRange   = _enemy.CheckPlayerInMeleeRange();
        _isPlayerInMinAgroRange = _enemy.CheckPlayerInMinAgroRange();
    }
}
