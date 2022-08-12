
public class BringerOfDeath_PlayerDetectedState : PlayerDetectedState
{
    private BringerOfDeath _bod;

    public BringerOfDeath_PlayerDetectedState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_PlayerDetected stateData)
        : base(enemy, stateMachine, animBoolName, stateData)
    {
        _bod = (BringerOfDeath)enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!_bod.MeleeAttackState.IsOnCooldown && _isPlayerInMeleeRange && _canMove)
            _stateMachine.ChangeState(_bod.MeleeAttackState);
        else if (!_bod.PortalRangedAttackState.IsOnCooldown && _canMove)
            _stateMachine.ChangeState(_bod.PortalRangedAttackState);
        else if (!_bod.OrbRangedAttackState.IsOnCooldown && _canMove)
            _stateMachine.ChangeState(_bod.OrbRangedAttackState);
        else if (_isPLayerInMinAgroRange && _canMove)
            _stateMachine.ChangeState(_bod.ChargeState);
        else if (!_isPLayerInMaxAgroRange)
            _stateMachine.ChangeState(_bod.LookForPlayerState);
    }
}
