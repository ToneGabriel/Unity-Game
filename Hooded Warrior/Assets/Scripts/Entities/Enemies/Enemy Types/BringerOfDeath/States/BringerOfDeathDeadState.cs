
public class BringerOfDeathDeadState : EnemyDeadState
{
    private BringerOfDeath _bod;

    public BringerOfDeathDeadState(BringerOfDeath bod, string animBoolName)
        : base(bod, animBoolName)
    {
        _bod = bod;
    }

}
