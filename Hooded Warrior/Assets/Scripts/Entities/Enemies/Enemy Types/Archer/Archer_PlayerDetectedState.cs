using UnityEngine;

public class Archer_PlayerDetectedState : PlayerDetectedState
{
    private Archer _archer;

    public Archer_PlayerDetectedState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_PlayerDetected stateData) 
        : base(enemy, stateMachine, animBoolName, stateData)
    {
        _archer = (Archer)enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!_archer.MeleeAttackState.IsOnCooldown && _isPlayerInMeleeRange)
        {
            if (!_archer.DodgeState.IsOnCooldown)
                _stateMachine.ChangeState(_archer.DodgeState);
            else
                _stateMachine.ChangeState(_archer.MeleeAttackState);
        }
        else if (!_archer.RangedAttackState.IsOnCooldown)
            _stateMachine.ChangeState(_archer.RangedAttackState);
        else if (!_isPLayerInMaxAgroRange)
            _stateMachine.ChangeState(_archer.LookForPlayerState);
    }
}
