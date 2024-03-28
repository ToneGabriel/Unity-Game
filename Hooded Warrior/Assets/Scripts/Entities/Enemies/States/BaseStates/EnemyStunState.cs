using UnityEngine;

public abstract class EnemyStunState : EnemyState
{
    protected Data_Stun _stateData;
    protected bool _isStunTimeOver;
    protected bool _isGrounded;
    protected bool _isMovementStopped;
    protected bool _isPlayerInMeleeRange;
    protected bool _isPlayerInMinAgroRange;

    public EnemyStunState(Enemy enemy, string animBoolName, Data_Stun stateData) 
        : base(enemy, animBoolName)
    {
        _stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        _isStunTimeOver = false;
        _isMovementStopped = false;
        _enemy.DamageHop(_stateData.StunKnockBackDirection, _stateData.StunKnockBackSpeed);     // knock back  
    }

    public override void Exit()
    {
        base.Exit();

        _enemy.ResetStunResistnce();
        _enemy.RBBodyType = RigidbodyType2D.Dynamic;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= _stateStartTime + _stateData.StunTime)                                                        // Counts stun time
            _isStunTimeOver = true;

        if (_isGrounded && Time.time >= _stateStartTime + _stateData.StunKnockBackTime && !_isMovementStopped)         // sets velocity to 0 while stunned
        {
            _isMovementStopped = true;
            _enemy.RBBodyType = RigidbodyType2D.Static;
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isGrounded = _enemy.IsGrounded();
        _isPlayerInMeleeRange = _enemy.CheckPlayerInMeleeRange();
        _isPlayerInMinAgroRange = _enemy.CheckPlayerInMinAgroRange();
    }
}
