
public class BringerOfDeathPlayerDetectedState : EnemyPlayerDetectedState
{
    private BringerOfDeath _bod;

    public BringerOfDeathPlayerDetectedState(BringerOfDeath bod, string animBoolName, Data_PlayerDetected stateData)
        : base(bod, animBoolName, stateData)
    {
        _bod = bod;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //if (!_bod.MeleeAttackState.IsOnCooldown && _isPlayerInMeleeRange && _canMove)
        //    _bod.ChangeState((int)BringerOfDeathStateID.MeleeAttack);
        //else if (!_bod.PortalRangedAttackState.IsOnCooldown && _canMove)
        //    _bod.ChangeState((int)BringerOfDeathStateID.PortalRangedAttack);
        //else if (!_bod.OrbRangedAttackState.IsOnCooldown && _canMove)
        //    _bod.ChangeState((int)BringerOfDeathStateID.OrbRangedAttack);
        //else if (_isPLayerInMinAgroRange && _canMove)
        //    _bod.ChangeState((int)BringerOfDeathStateID.Charge);
        //else if (!_isPLayerInMaxAgroRange)
        //    _bod.ChangeState((int)BringerOfDeathStateID.LookForPlayer);
    }
}
