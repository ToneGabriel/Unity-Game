
public class Archer_DodgeState : DodgeState
{
    private Archer _archer;

    public Archer_DodgeState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_Dodge stateData)
        : base(enemy, stateMachine, animBoolName, stateData)
    {
        _archer = (Archer)enemy;
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(_isDodgeOver)
        {
            if (_isPlayerInMeleeRange)
                _stateMachine.ChangeState(_archer.MeleeAttackState);
            else if (_isPlayerInMaxAgroRange && !_isPlayerInMeleeRange)
                _stateMachine.ChangeState(_archer.RangedAttackState);
            else if (!_isPlayerInMaxAgroRange)
                _stateMachine.ChangeState(_archer.LookForPlayerState);
        }
    }
}
