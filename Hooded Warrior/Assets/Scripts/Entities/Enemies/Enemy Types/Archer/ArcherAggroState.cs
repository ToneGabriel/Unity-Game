
public class ArcherAggroState : EntityState
{
    protected ArcherAggroMode       _archerAggroMode;
    protected Archer                _archer;
    protected ArcherAggroModeData   _archerAggroModeData;

    public ArcherAggroState(Archer archer, ArcherAggroModeData data, string animBoolName)
        : base(archer, animBoolName)
    {
        _archer                 = archer;
        _archerAggroModeData    = data;
    }
}
