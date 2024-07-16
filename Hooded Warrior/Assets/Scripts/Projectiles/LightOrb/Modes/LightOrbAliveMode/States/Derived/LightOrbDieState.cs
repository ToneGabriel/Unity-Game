
public sealed class LightOrbDieState : LightOrbBaseState
{
    public LightOrbDieState(LightOrbAliveMode mode, LightOrbAliveModeData data)
        : base(mode, data) { /*Empty*/ }

    public override void Update()
    {
        base.Update();

        if (_lightOrbAliveMode.Target_OuterLightInnerRadius > 0f)
        {
            // gradually decrease outer radius
            _lightOrbAliveMode.Target_OuterLightInnerRadius -= _lightOrbAliveModeData.OuterLightInnerRadiusChangeRatio;
            _lightOrbAliveMode.Target_OuterLightOuterRadius -= _lightOrbAliveModeData.OuterLightOuterRadiusChangeRatio;
        }
        else if (_lightOrbAliveMode.Target_InnerLightInnerRadius > 0f)
        {
            // gradually decrease inner radius
            _lightOrbAliveMode.Target_InnerLightInnerRadius -= _lightOrbAliveModeData.InnerLightInnerRadiusChangeRatio;
            _lightOrbAliveMode.Target_InnerLightOuterRadius -= _lightOrbAliveModeData.InnerLightOuterRadiusChangeRatio;
        }
        else
            _lightOrbAliveMode.Target_Die();
    }
}
