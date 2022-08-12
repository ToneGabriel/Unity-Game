using UnityEngine;

public abstract class StunState : EnemyState
{
    protected Data_Stun _stateData;
    protected bool _isStunTimeOver;
    protected bool _isGrounded;
    protected bool _isMovementStopped;
    protected bool _isPlayerInMeleeRange;
    protected bool _isPlayerInMinAgroRange;

    public StunState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_Stun stateData) 
        : base(enemy, stateMachine, animBoolName)
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
        _enemy.Rigidbody.bodyType = RigidbodyType2D.Dynamic;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= StartTime + _stateData.StunTime)                                                        // Counts stun time
            _isStunTimeOver = true;

        if (_isGrounded && Time.time >= StartTime + _stateData.StunKnockBackTime && !_isMovementStopped)         // sets velocity to 0 while stunned
        {
            _isMovementStopped = true;
            _enemy.Rigidbody.bodyType = RigidbodyType2D.Static;
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isGrounded = _enemy.CheckIfGrounded();
        _isPlayerInMeleeRange = _enemy.CheckPlayerInMeleeRange();
        _isPlayerInMinAgroRange = _enemy.CheckPlayerInMinAgroRange();
    }
}
