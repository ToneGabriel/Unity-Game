using UnityEngine;

public class Archer_StunState : StunState
{
    private Archer _archer;

    public Archer_StunState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_Stun stateData) 
        : base(enemy, stateMachine, animBoolName, stateData)
    {
        _archer = (Archer)enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if (_isStunTimeOver)
        {
            if (_isPlayerInMeleeRange)
                _stateMachine.ChangeState(_archer.MeleeAttackState);
            else if (_isPlayerInMinAgroRange)
                _stateMachine.ChangeState(_archer.PlayerDetectedState);
            else
            {
                _archer.LookForPlayerState.SetTurnImmediately(true);
                _stateMachine.ChangeState(_archer.LookForPlayerState);
            }
        }
    }
}
