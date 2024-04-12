
public class LightOrbLiveState : LightOrbBaseState
{
    public LightOrbLiveState(LightOrb lightOrb)
        : base(lightOrb) { }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_lightOrb.IsReadyToDie())
            _lightOrb.ChangeState((int)LightOrbStateID.Die);
    }
}
