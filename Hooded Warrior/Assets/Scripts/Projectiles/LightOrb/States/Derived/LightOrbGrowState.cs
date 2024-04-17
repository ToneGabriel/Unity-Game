
public class LightOrbGrowState : LightOrbBaseState
{
    public LightOrbGrowState(LightOrb lightOrb)
        : base(lightOrb) { }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_lightOrb.IsOrbLightAtMaxRadius())
            _lightOrb.ChangeState((int)LightOrbStateID.Live);
        else
            _lightOrb.IncreaseLightRadius();
            //_lightOrb.IncreaseLightRadius(0.04f, 0.11f);
    }
}
