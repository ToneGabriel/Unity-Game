
public class BringerOfDeathDeadState : EnemyDeadState
{
    private BringerOfDeath _bod;

    public BringerOfDeathDeadState(BringerOfDeath bod, string animBoolName, Data_Dead stateData)
        : base(bod, animBoolName, stateData)
    {
        _bod = bod;
    }

}
