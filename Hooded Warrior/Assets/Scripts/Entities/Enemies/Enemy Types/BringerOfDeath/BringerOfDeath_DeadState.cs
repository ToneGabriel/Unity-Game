
public class BringerOfDeath_DeadState : DeadState
{
    private BringerOfDeath _bod;

    public BringerOfDeath_DeadState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_Dead stateData)
        : base(enemy, stateMachine, animBoolName, stateData)
    {
        _bod = (BringerOfDeath)enemy;
    }

}
