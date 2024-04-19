
public sealed class LightOrbBornState : LightOrbBaseState
{
    public LightOrbBornState(LightOrb lightOrb)
        : base(lightOrb) { }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_lightOrb.InnerLightInnerRadius < _lightOrb.Data.InnerLightMaxInnerRadius)
        {
            // gradually grow inner light radius
            _lightOrb.InnerLightInnerRadius += _lightOrb.Data.InnerLightInnerRadiusChangeRatio;
            _lightOrb.InnerLightOuterRadius += _lightOrb.Data.InnerLightOuterRadiusChangeRatio;
        }
        else if (_lightOrb.OuterLightInnerRadius < _lightOrb.Data.OuterLightMaxInnerRadius)
        {
            // gradually grow outer light radius
            _lightOrb.OuterLightInnerRadius += _lightOrb.Data.OuterLightInnerRadiusChangeRatio;
            _lightOrb.OuterLightOuterRadius += _lightOrb.Data.OuterLightOuterRadiusChangeRatio;
        }
        else
            _lightOrb.ChangeState((int)LightOrbStateID.Live);
    }
}
