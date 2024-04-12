
public class LightOrbBornState : LightOrbBaseState
{
    public LightOrbBornState(LightOrb lightOrb)
        : base(lightOrb) { }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_lightOrb.CanGrow())
            _lightOrb.ChangeState((int)LightOrbStateID.Grow);
    }
}
