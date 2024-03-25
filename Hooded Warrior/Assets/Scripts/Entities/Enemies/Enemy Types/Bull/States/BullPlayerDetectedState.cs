
public class BullPlayerDetectedState : EnemyPlayerDetectedState
{
    private Bull _bull;

    public BullPlayerDetectedState(Bull bull, string animBoolName, Data_PlayerDetected stateData)
        : base(bull, animBoolName, stateData)
    {
        _bull = bull;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //if (_bull.MeleeAttackState.IsOnCooldown && _isPlayerInMeleeRange && _canMove)
        //    _bull.ChangeState((int)BullStateID.MeleeAttack);
        //else if (_isPLayerInMinAgroRange && _canMove)
        //    _bull.ChangeState((int)BullStateID.Charge);
        //else if (!_isPLayerInMaxAgroRange)
        //    _bull.ChangeState((int)BullStateID.LookForPlayer);
    }
}
