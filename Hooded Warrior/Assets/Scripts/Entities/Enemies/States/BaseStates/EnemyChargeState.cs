using UnityEngine;

public abstract class EnemyChargeState : EnemyState
{
    protected Data_Charge _stateData;
    protected bool _isPlayerInMinAgroRange;
    protected bool _isDetectingLedge;
    protected bool _isDetectingWall;
    protected bool _isChargeTimeOver;
    protected bool _isPlayerInMeleeRange;

    public EnemyChargeState(Enemy enemy, string animBoolName, Data_Charge stateData)
        : base(enemy, animBoolName)
    {
        _stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        _enemy.SetVelocity(_stateData.ChargeSpeed);
        _isChargeTimeOver = false;
    }

    public override void LogicUpdate()                                                  
    {
        base.LogicUpdate();

        if (Time.time >= _enemy.EntityIntStatusComponents.StateStartTime + _stateData.ChargeTime)                              // Counts charge time
            _isChargeTimeOver = true;
    }

    public override void DoChecks()                                                     // Check ranges
    {
        base.DoChecks();

        _isPlayerInMinAgroRange = _enemy.CheckPlayerInMinAgroRange();
        _isDetectingLedge = _enemy.CheckIfTouchingLedge(-_enemy.transform.up);
        _isDetectingWall = _enemy.CheckIfTouchingWall();
        _isPlayerInMeleeRange = _enemy.CheckPlayerInMeleeRange();
    }

}
