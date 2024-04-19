
public sealed class LightOrbDieState : LightOrbBaseState
{
    public LightOrbDieState(LightOrb lightOrb)
        : base(lightOrb) { }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_lightOrb.OuterLightInnerRadius > 0f)
        {
            // gradually decrease outer radius
            _lightOrb.OuterLightInnerRadius -= _lightOrb.Data.OuterLightInnerRadiusChangeRatio;
            _lightOrb.OuterLightOuterRadius -= _lightOrb.Data.OuterLightOuterRadiusChangeRatio;
        }
        else if (_lightOrb.InnerLightInnerRadius > 0f)
        {
            // gradually decrease inner radius
            _lightOrb.InnerLightInnerRadius -= _lightOrb.Data.InnerLightInnerRadiusChangeRatio;
            _lightOrb.InnerLightOuterRadius -= _lightOrb.Data.InnerLightOuterRadiusChangeRatio;
        }
        else
            _lightOrb.Die();
    }
}
